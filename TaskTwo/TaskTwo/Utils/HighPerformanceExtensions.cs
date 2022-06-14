using System;
using System.ComponentModel;
using System.Threading.Tasks;

using Microsoft.Toolkit.HighPerformance;
using Microsoft.Toolkit.HighPerformance.Enumerables;

using static System.Linq.Enumerable;

namespace TaskTwo.Utils;

internal static class Helper
{
    private static ReadOnlySpanEnumerable<T> EnumerateRow<T>(in this ReadOnlyMemory2D<T> memory2D, in int row) =>
        memory2D.Span.GetRowSpan(row).Enumerate();

    private static void Deconstruct<T>(in this ReadOnlySpanEnumerable<T>.Item item, out int index, out T value)
    {
        index = item.Index;
        value = item.Value;
    }
        
    /// <summary>Converts a 2D <seealso cref="ReadOnlyMemory2D{T}"/> memory view to a two-dimensional <typeparamref name="TResult"/> array.</summary>
    /// <param name="memory">The view to be converted.</param>
    /// <param name="convert">The function to convert a <typeparamref name="T"/> object to a <typeparamref name="TResult"/> instance.</param>
    /// <typeparam name="T">The <paramref name="memory"/> view type parameter.</typeparam>
    /// <typeparam name="TResult">The outcome type parameter.</typeparam>
    /// <returns>A two-dimensional array of the <typeparamref name="TResult"/> type.</returns>
    /// <exception cref="ArgumentException">There is no function available to convert a <typeparamref name="T"/> object to a <typeparamref name="TResult"/> instance.</exception>
    internal static TResult?[,] Convert<T, TResult>(this ReadOnlyMemory2D<T?> memory,
                                                    Func<T?, TResult?>? convert = null)
    {
        if (convert is null)
        {
            var (tIn, tOut) = (typeof(T), typeof(TResult));
            var converter = TypeDescriptor.GetConverter(tIn);
            if (!converter.CanConvertTo(tOut))
                throw new ArgumentException($@"A convert function from '{tIn}' to '{tOut}' must be provided.",
                                            nameof(convert));

            convert = value => (TResult?)converter.ConvertTo(value, tOut);
        }

        var result = new TResult?[memory.Height, memory.Width];
        Parallel.ForEach(Range(0, memory.Height), i => {
            foreach (var (j, value) in memory.EnumerateRow(i))
                result[i, j] = convert(value);
        });

        return result;
    }
}