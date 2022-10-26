using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCore.Extensions
{
    /// <summary>
    /// Array comparator, a metadata comparator that is provided when mapping an array to a database field
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayComparer<T> : ValueComparer<T[]>
    {
        /// <summary>
        ///     <para>
        ///         Creates a new <see cref="T:Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer`1" /> with the given comparison and
        ///         snapshotting expressions.
        ///     </para>
        ///     <para>
        ///         Snapshotting is the process of creating a copy of the value into a snapshot so it can
        ///         later be compared to determine if it has changed. For some types, such as collections,
        ///         this needs to be a deep copy of the collection rather than just a shallow copy of the
        ///         reference.
        ///     </para>
        /// </summary>
        public ArrayComparer() :
            base((c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => v != null ? HashCode.Combine(a, v.GetHashCode()) : 0),
                c => c.ToArray())
        {
        }
    }
}
