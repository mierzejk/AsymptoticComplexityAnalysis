using System.Collections.Generic;
using TaskOne.Contracts;

namespace TaskOne.Utils;

/// <summary>
/// Provides a base class for implementations of the <see cref="ISort{T}"/> generic interface.
/// </summary>
/// <typeparam name="T">The type of objects to sort.</typeparam>
public abstract class CompareSort<T> : ISort<T>
{
    /// <summary>
    /// An object to compare collection elements when sorting in default order.
    /// </summary>
    private readonly IComparer<T> _comparer;
    
    /// <summary>
    /// An object to compare collection elements when sorting in reversed order.
    /// </summary>
    private readonly IComparer<T> _reverseComparer;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompareSort{T}"/> class to specified comparers.
    /// </summary>
    /// <param name="comparer">If provided, the object to compare collection elements when sorting in default order; an ascending order comparer by default.</param>
    /// <param name="reverseComparer">If provided, the object to compare collection elements when sorting in reversed order; a reverse of <paramref name="comparer"/> by default.</param>
    protected CompareSort(IComparer<T>? comparer = null, IComparer<T>? reverseComparer = null)
    {
        this._comparer = comparer ?? Comparer<T>.Default;
        this._reverseComparer = reverseComparer ?? new ReverseComparer<T>(this._comparer);
    }

    /// <summary>
    /// Sorts the <paramref name="collection"/> elements.
    /// </summary>
    /// <param name="collection">The collection to be sorted.</param>
    /// <param name="reverse">True to sort in reversed order; false for default.</param>
    public T[] Sort(IEnumerable<T> collection, bool reverse = false) =>
        this.Sort(collection, reverse ? this._reverseComparer : this._comparer);

    public abstract T[] Sort(IEnumerable<T> collection, IComparer<T>? comparer);
}