using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskOne.Utils;

internal static class Helper
{
    // ReSharper disable once MemberCanBePrivate.Global
    public static (T1, T2) Identity<T1, T2>(T1 arg1, T2 arg2) => (arg1, arg2);

    /// <summary>
    /// Copies the elements of the <paramref name="source"/> to a new array.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to create an array from.</param>
    /// <typeparam name="T">The type of the source elements.</typeparam>
    /// <returns>An array that contains elements copied from the <paramref name="source"/>.</returns>
    public static T[] ToArraySmart<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);
        T[] array;
        
        switch (source)
        {
            case T[] arrayCollection:
                array = new T[arrayCollection.Length];
                arrayCollection.CopyTo(array, 0);
                break;
            case List<T> listCollection:
                array = listCollection.ToArray();
                break;
            case ICollection<T> sizedCollection:
                array = new T[sizedCollection.Count];
                foreach (var (item, i) in sizedCollection.Select(Identity))
                    array[i] = item;
                break;
            default:
                array = source.ToArray();
                break;
        }

        return array;
    }
}