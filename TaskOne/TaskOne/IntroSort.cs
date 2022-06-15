using System;
using System.Collections.Generic;

namespace TaskOne;
using Utils;

/// <summary>
/// A class implementing the <see cref="Contracts.ISort{T}"/> interface with the introspective sort as provided by <see cref="Array"/>::Sort method.
/// </summary>
/// <typeparam name="T">The type of objects to sort.</typeparam>
/// <remarks>https://docs.microsoft.com/en-us/dotnet/api/system.array.sort?view=net-6.0</remarks>
public sealed class IntroSort<T> : CompareSort<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntroSort{T}"/> class to specified comparers.
    /// </summary>
    /// <inheritdoc />
    public IntroSort(IComparer<T>? comparer = null, IComparer<T>? reverseComparer = null)
        : base(comparer, reverseComparer) { }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
    public override T[] Sort(IEnumerable<T> collection, IComparer<T>? comparer)
    {
        var array = collection.ToArraySmart();
        Array.Sort(array, comparer ?? Comparer<T>.Default);
        return array;
    }
}