using System.Collections.Generic;

using static System.Linq.Enumerable;

namespace TaskOne;
using Utils;

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
    public PriorityQueueSort(Comparer<T>? comparer = null, ReverseComparer<T>? reverseComparer = null)
        : base(comparer, reverseComparer) { }

    public override void Sort(IList<T> collection, Comparer<T> comparer)
    {
        var minHeap = new PriorityQueue<T, T>(collection.Count, comparer);
        minHeap.EnqueueRange(from item in collection
                             select (item, item));
        
        foreach (var i in Range(0, minHeap.Count))
            collection[i] = minHeap.Dequeue();
    }
}