using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskOne;

using Utils;

public class HeapSort<T> : CompareSort<T>
{
    private static void Heapify(Span<T> span, int index, IComparer<T> comparer)
    {
        var maxIndex = index;
        
        while (true)
        {
            var maxValue = span[index]; 
            if (span.TryLeftChild(index, out var value) is {} leftIndex && comparer.Compare(value, maxValue) > 0)
            {
                maxIndex = leftIndex;
                maxValue = value;
            }
            if (span.TryRightChild(index, out value) is {} rightIndex && comparer.Compare(value, maxValue) > 0)
            {
                maxIndex = rightIndex;
                maxValue = value;
            }

            if (maxIndex == index)
                break;

            span[maxIndex] = span[index];
            span[index] = maxValue!;
            index = maxIndex;
        }
    }
    
    public HeapSort(IComparer<T>? comparer = null, IComparer<T>? reverseComparer = null)
        : base(comparer, reverseComparer) { }

    public override T[] Sort(IEnumerable<T> collection, IComparer<T>? comparer)
    {
        ArgumentNullException.ThrowIfNull(collection);

        comparer ??= Comparer<T>.Default;
        // TODO: arrays, list, ICollection, itp.
        var array = collection.ToArray();
        Console.WriteLine(string.Join(@", ", array));

        for (var i = array.Length / 2 - 1; i >= 0; i--)
        {
            Heapify(array, i, comparer);
            Console.WriteLine(string.Join(@", ", array));
        }
        
        return array;
    }
    
}