using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCore.Extensions;

/// <summary>
/// Responsible for converting entity value objects to Json string format for storage in the database
/// </summary>
public class StringJsonConverter<TModel> : ValueConverter<TModel, string>
{
    public StringJsonConverter(ConverterMappingHints? mappingHints = null)
        : base(v => JsonSerializer.Serialize(v, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            }),
            v => JsonSerializer.Deserialize<TModel>(v, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            })!, mappingHints)
    {
    }
}
