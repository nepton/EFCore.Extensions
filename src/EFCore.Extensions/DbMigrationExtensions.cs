using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EFCore.Extensions;

/// <summary>
/// EF Core 执行数据库 Migration 操作
/// </summary>
public static class DbMigrationExtensions
{
    /// <summary>
    /// 初始化database方法
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="services"></param>
    /// <param name="seedAction"></param>
    /// <returns></returns>
    public static IServiceProvider MigrateDbContext<TContext>(this IServiceProvider services, Action<TContext, IServiceProvider>? seedAction = null)
        where TContext : DbContext
    {
        //创建数据库实例在本区域有效
        using var scope   = services.CreateScope();
        var       logger  = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
        var       context = scope.ServiceProvider.GetService<TContext>();

        if (context == null)
            throw new InvalidOperationException($"Cannot find context {typeof(TContext).Name}, please check if the context is registered in the DI container.");

        try
        {
            context.Database.Migrate(); //初始化database
            seedAction?.Invoke(context, services);
            logger.LogInformation("DbContext {DbContext} Initialized", typeof(TContext).Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "An error occurred while migrating the database used on {DbContext} ({ConnectionString})",
                typeof(TContext).Name,
                context.Database.GetDbConnection());
        }

        return services;
    }
}
