using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TaskOne;
using Utils;

/// <summary>
/// A class implementing the <see cref="Contracts.ISort{T}"/> interface with an array-backed, binary max-heap.
/// </summary>
/// <typeparam name="T">The type of objects to sort.</typeparam>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class HeapSort<T> : CompareSort<T>
{
    /// <summary>
    /// Rearranges the <paramref name="array"/> elements by swapping respective parent-child nodes, so the data
    /// structure satisfies the max-heap property.
    /// </summary>
    /// <param name="array">The array representation of a binary heap.</param>
    /// <param name="index">The index of a node to start the "heapifying down" algorithm with.</param>
    /// <param name="length">Limits the number of <paramref name="array"/> cells considered the binary heap nodes.</param>
    /// <param name="comparer">Provides means of comparing <paramref name="array"/> elements.</param>
    /// <remarks>The method implements the "heapify down" in-place approach (or "from the bottom up" according to
    /// Cormen's "Algorithms Unlocked" book), so its time complexity is O(n). The opposite "up" approach ("starting with
    /// an empty binary heap") would require O(n⋅log(n)) time to complete.</remarks>
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

    /// <inheritdoc />
    /// <exception cref="System.ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
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