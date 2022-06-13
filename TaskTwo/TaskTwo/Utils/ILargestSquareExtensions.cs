using System;

using Microsoft.Toolkit.HighPerformance;

namespace TaskTwo.Utils
{
    using Contracts;
    using Utils;
    
    public static class LargestSquareExtensions
    {
        /// <summary>
        /// Given a two-dimensional matrix of <seealso cref="uint"/> values, returns the area of the largest square
        /// that contains only unities.
        /// </summary>
        /// <param name="object">The object being extended.</param>
        /// <param name="matrix">A two-dimensional array of binary values.</param>
        /// <returns>The area of the largest square that contains only unities.</returns>
        public static uint GetLargestSquareArea(this ILargestSquare @object, in ReadOnlyMemory2D<uint> matrix) =>
            @object.GetLargestSquareArea(matrix.Convert(Convert.ToBoolean));
        
        /// <summary>
        /// Given a two-dimensional matrix of <seealso cref="int"/> values, returns the area of the largest square
        /// that contains only ones.
        /// </summary>
        /// <param name="object">The object being extended.</param>
        /// <param name="matrix">A two-dimensional array of binary values.</param>
        /// <returns>The area of the largest square that contains only ones.</returns>
        public static uint GetLargestSquareArea(this ILargestSquare @object, in ReadOnlyMemory2D<int> matrix) =>
            @object.GetLargestSquareArea(matrix.Convert<int, bool>());
    }
}