using System;
using System.Collections.Generic;
using System.Linq;

using static System.Linq.Enumerable;

namespace TaskOne;

using Utils;

public static class Program
{
    // The heapify process takes log n time and the swapping takes O(n) time. Thus the overall time complexity of heapsort is O(n logn).

    public static void Main(string[] args)
    {
        var array = new[] {1.0F, -0.0F, 0F, 2.5F, 123, 100, 44, 55, 44, -3.123};
        whateverInPlace(array, reverse: false);
        Console.WriteLine(string.Join(@", ", array));
    }

    private static void whateverInPlace<T>(IList<T> collection, Comparer<T>? comparer = null, bool reverse = false)
    {
        var length = collection.Count;
        comparer ??= Comparer<T>.Default;
        if (reverse)
            comparer = new ReverseComparer<T>(comparer);
            
        var minHeap = new PriorityQueue<T, T>(length, comparer);
        minHeap.EnqueueRange(from item in collection
                             select (item, item));
        
        foreach (var i in Range(0, length))
            collection[i] = minHeap.Dequeue();
    }
}