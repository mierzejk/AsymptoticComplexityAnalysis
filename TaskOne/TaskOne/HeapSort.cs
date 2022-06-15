using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskOne;

using Utils;

public class HeapSort<T> : CompareSort<T>
{
    private static void Heapify(T[] segment, int index, IComparer<T> comparer)
    {
        ref T RefGreaterChild(int parentIndex, int offset, ref T maxValue, out int refIndex)
        {
            var childIndex = (parentIndex << 1) + offset;
            if (childIndex < segment.Length && comparer.Compare(segment[childIndex], maxValue) > 0)
            {
                refIndex = childIndex;
                return ref segment[childIndex];
            }

            refIndex = parentIndex;
            return ref maxValue;
        }

        ref var maxValue = ref segment[index];
        var refIndices = new[]{index, index};
        do
        {
            index = refIndices.Max();
            maxValue = ref RefGreaterChild(index, 1, ref maxValue, out refIndices[0])!; // left
            maxValue = ref RefGreaterChild(index, 2, ref maxValue, out refIndices[1])!; // right
            (segment[index], maxValue) = (maxValue, segment[index]); // swap
        } while (refIndices.Any(i => i > index));
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