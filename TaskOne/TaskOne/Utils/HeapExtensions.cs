using System;

namespace TaskOne.Utils;

internal static class HeapExtensions
{
    private static int? TryChild<T>(in ReadOnlySpan<T> span, int nodeIndex, int offset, out T? childValue)
    {
        childValue = default;
        var childIndex = (nodeIndex << 1) + offset;
        if (childIndex >= span.Length) return null;
        childValue = span[childIndex];
        return childIndex;
    }
    
    /// <summary>
    /// Given the contiguous <paramref name="span"/> layout of a binary heap, tries to get the left child of a node at
    /// the <paramref name="nodeIndex"/> position.
    /// </summary>
    /// <param name="span">The array representation of a binary heap.</param>
    /// <param name="nodeIndex">The parent node index.</param>
    /// <param name="childValue">The left child value if the child node exists; otherwise <c>default(T)</c>.</param>
    /// <typeparam name="T">The type of the binary heap nodes.</typeparam>
    /// <returns>The left child node index if the child exists; otherwise <c>null</c>.</returns>
    public static int? TryLeftChild<T>(this in ReadOnlySpan<T> span, int nodeIndex, out T? childValue) =>
        TryChild(span, nodeIndex, 1, out childValue);
    
    /// <summary>
    /// Given the contiguous <paramref name="span"/> layout of a binary heap, tries to get the left child of a node at
    /// the <paramref name="nodeIndex"/> position.
    /// </summary>
    /// <param name="span">The array representation of a binary heap.</param>
    /// <param name="nodeIndex">The parent node index.</param>
    /// <param name="childValue">The left child value if the child node exists; otherwise <c>default(T)</c>.</param>
    /// <typeparam name="T">The type of the binary heap nodes.</typeparam>
    /// <returns>The left child node index if the child exists; otherwise <c>null</c>.</returns>
    public static int? TryLeftChild<T>(this in Span<T> span, int nodeIndex, out T? childValue) =>
        TryChild(span, nodeIndex, 1, out childValue);
    
    /// <summary>
    /// Given the contiguous <paramref name="span"/> layout of a binary heap, tries to get the right child of a node at
    /// the <paramref name="nodeIndex"/> position.
    /// </summary>
    /// <param name="span">The array representation of a binary heap.</param>
    /// <param name="nodeIndex">The parent node index.</param>
    /// <param name="childValue">The right child value if the child node exists; otherwise <c>default(T)</c>.</param>
    /// <typeparam name="T">The type of the binary heap nodes.</typeparam>
    /// <returns>The right child node index if the child exists; otherwise <c>null</c>.</returns>
    public static int? TryRightChild<T>(this in ReadOnlySpan<T> span, int nodeIndex, out T? childValue) =>
        TryChild(span, nodeIndex, 2, out childValue);
    
    /// <summary>
    /// Given the contiguous <paramref name="span"/> layout of a binary heap, tries to get the right child of a node at
    /// the <paramref name="nodeIndex"/> position.
    /// </summary>
    /// <param name="span">The array representation of a binary heap.</param>
    /// <param name="nodeIndex">The parent node index.</param>
    /// <param name="childValue">The right child value if the child node exists; otherwise <c>default(T)</c>.</param>
    /// <typeparam name="T">The type of the binary heap nodes.</typeparam>
    /// <returns>The right child node index if the child exists; otherwise <c>null</c>.</returns>
    public static int? TryRightChild<T>(this in Span<T> span, int nodeIndex, out T? childValue) =>
        TryChild(span, nodeIndex, 2, out childValue);
}