using System;
using System.Linq;

using Microsoft.Toolkit.HighPerformance;

namespace TaskTwo
{
    using Contracts;
    
    /// <summary>Dynamic programming implementation of the <see cref="ILargestSquare"/> contract.</summary>
    public sealed class DynamicProgramming : ILargestSquare
    { // TODO: comments.
        public uint GetLargestSquareArea(in ReadOnlySpan2D<bool> matrix)
        {
            var (m, n) = (matrix.Height, matrix.Width);  // rows, columns
            var squareSides = new uint[m + n];
            
            // iterate over rows
            for (var i = 0; i < m; i++)
            {
                var prevRow = new ArraySegment<uint>(squareSides, m-i-1, n+1);
                var prevSide = new uint[1];
                
                // iterate over columns (row cells)
                for (var j = 0; j < n; j++)
                    prevRow[j] = prevSide[0] = matrix[i, j] switch {
                        true => 1 + prevSide.Concat(prevRow[j..(j+2)]).Min(),
                        false => 0U
                    };
            }

            var maxSide = matrix.IsEmpty? 0U : squareSides.Max();
            return checked(maxSide * maxSide);
        }
    }
}