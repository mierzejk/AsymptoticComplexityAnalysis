using System.Collections.Generic;
using System.Linq;

namespace TaskOne;

using static Utils.Helper;

/// <summary>
/// max-heap
/// </summary>
/// <typeparam name="T"></typeparam>
public class HeapSort<T> : CompareSort<T>
{
    /// <summary>
    /// max-heap, heapify down, "from the bottom up" Cormen
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <param name="comparer"></param>
    // ReSharper disable once SuggestBaseTypeForParameter
    private static void Heapify(T[] segment, int index, int length, IComparer<T> comparer)
    {
        ref T RefGreaterChild(int parentIndex, int offset, ref T maxValue, out int refIndex)
        {
            var childIndex = (parentIndex << 1) + offset;
            if (childIndex < length && comparer.Compare(segment[childIndex], maxValue) > 0)
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
            (segment[index], maxValue) = (maxValue, segment[index]); // swap node value with a greater child 
        } while (refIndices.Any(i => i > index));
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="HeapSort{T}"/> class to specified comparers.
    /// </summary>
    /// <inheritdoc />
    public HeapSort(IComparer<T>? comparer = null, IComparer<T>? reverseComparer = null)
        : base(comparer, reverseComparer) { }

    public override T[] Sort(IEnumerable<T> collection, IComparer<T>? comparer)
    {
        var array = collection.ToArraySmart();
        comparer ??= Comparer<T>.Default;

        // turn the array into a max-heap
        for (var i = array.Length / 2; i > 0;)
            Heapify(array, --i, array.Length, comparer);
        
        // swap root (max value) with the last leaf and heapify to restore the max-heap properties; repeat with an array segment
        for (var i = array.Length - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            Heapify(array, 0, i, comparer);
        }
        
        return array;
    }
    
}