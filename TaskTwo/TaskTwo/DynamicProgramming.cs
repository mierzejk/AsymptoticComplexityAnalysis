using System;
using System.Linq;

using Microsoft.Toolkit.HighPerformance;

namespace TaskTwo;
using Contracts;
    
/// <summary>
/// Dynamic programming implementation of the <see cref="ILargestSquare"/> contract.
/// </summary>
public sealed class DynamicProgramming : ILargestSquare
{
    public uint GetLargestSquareArea(in ReadOnlySpan2D<bool> matrix)
    {
        var (m, n) = (matrix.Height, matrix.Width);  // rows, columns
        var (squareSides, maxSide) = (new uint[m+n], new uint[1]);
            
        // iterate over rows
        for (var i = 0; i < m; i++)
        {
            var prevRow = new ArraySegment<uint>(squareSides, m-i-1, n+1);
            var prevSide = new uint[1];
                
            // iterate over columns (row cells)
            for (var j = 0; j < n; j++)
                // bottom-right square corner test, followed by updating the side length accordingly
                prevRow[j] = prevSide[0] = matrix[i, j] switch {
                    true => 1 + prevSide.Concat(prevRow[j..(j+2)]).Min(),
                    false => 0U
                };
                
            maxSide[0] = maxSide.Concat(prevRow).Max();
        }
            
        return checked(maxSide[0] * maxSide[0]);
    }
}