using System;
using System.Collections.Generic;

using static System.Linq.Enumerable;

namespace TaskOne;

/// <summary>
/// A class implementing the <see cref="Contracts.ISort{T}"/> with an array-backed, quaternary min-heap,
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

    public override T[] Sort(IEnumerable<T> collection, IComparer<T>? comparer)
    {
        ArgumentNullException.ThrowIfNull(collection);
        
        PriorityQueue<T, T> minHeap;
        if (collection is ICollection<T> sized)
            minHeap = new PriorityQueue<T, T>(sized.Count, comparer);
        else
            minHeap = new PriorityQueue<T, T>(comparer);
        
        minHeap.EnqueueRange(from item in collection
                             select (item, item));

        var result = new T[minHeap.Count];
        foreach (var i in Range(0, result.Length))
            result[i] = minHeap.Dequeue();

        return result;
    }
}