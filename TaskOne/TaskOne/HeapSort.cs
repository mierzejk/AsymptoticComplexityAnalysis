using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TaskOne;
using Utils;

/// <summary>
/// max-heap
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class HeapSort<T> : CompareSort<T>
{
    /// <summary>
    /// max-heap, heapify down, "from the bottom up" Cormen
    /// </summary>
    /// <param name="array"></param>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <param name="comparer"></param>
    // ReSharper disable once SuggestBaseTypeForParameter
    protected virtual void Heapify(T[] array, int index, int length, IComparer<T> comparer)
    {
        ref T RefGreaterChild(int offset, ref T maxValue, out int refIndex)
        {
            var childIndex = (index << 1) + offset;
            if (childIndex < length && comparer.Compare(array[childIndex], maxValue) > 0)
            {
                refIndex = childIndex;
                return ref array[childIndex];
            }

            refIndex = index;
            return ref maxValue;
        }

        ref var maxValue = ref array[index];
        var refIndices = new[] {index, index};
        do
        {
            index = refIndices.Max();
            maxValue = ref RefGreaterChild(1, ref maxValue, out refIndices[0])!; // left
            maxValue = ref RefGreaterChild(2, ref maxValue, out refIndices[1])!; // right
            (array[index], maxValue) = (maxValue, array[index]); // swap node value with a greater child 
        } while (refIndices.Any(i => i > index));  // nodes swapped?
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
        var arrayLength = array.Length;
        comparer ??= Comparer<T>.Default;

        // turn the array into a max-heap by heapifying subtrees.
        for (var i = arrayLength / 2; i > 0;)
            this.Heapify(array, --i, arrayLength, comparer);
        
        // swap root (max value) with the last leaf and heapify to restore the max-heap properties; repeat with an array segment
        for (var i = arrayLength - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            this.Heapify(array, 0, i, comparer);
        }
        
        return array;
    }
    
}