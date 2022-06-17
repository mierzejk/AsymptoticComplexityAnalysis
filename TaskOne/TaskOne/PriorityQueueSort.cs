using System;
using System.Collections.Generic;

using static System.Linq.Enumerable;

namespace TaskOne;
using Utils;

/// <summary>
/// A class implementing the <see cref="Contracts.ISort{T}"/> interface with an array-backed, quaternary min-heap,
/// based on the <see cref="PriorityQueue{TElement,TPriority}"/> class.
/// </summary>
/// <typeparam name="T">The type of objects to sort.</typeparam>
public sealed class PriorityQueueSort<T> : CompareSort<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueueSort{T}"/> class to specified comparers.
    /// </summary>
    /// <inheritdoc />
    public PriorityQueueSort(IComparer<T>? comparer = null, IComparer<T>? reverseComparer = null)
        : base(comparer, reverseComparer) { }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
    public override T[] Sort(IEnumerable<T> collection, IComparer<T>? comparer)
    {
        ArgumentNullException.ThrowIfNull(collection);
        
        var minHeap = collection is ICollection<T> sized
            ? new PriorityQueue<T, T>(sized.Count, comparer)
            : new PriorityQueue<T, T>(comparer);
        
        minHeap.EnqueueRange(from item in collection
                             select (item, item));

        var result = new T[minHeap.Count];
        foreach (var i in Range(0, result.Length))
            result[i] = minHeap.Dequeue();

        return result;
    }
}