using System;
using System.Linq;

using Microsoft.Toolkit.HighPerformance;

namespace TaskTwo
{
    using Contracts;
    
    /// <summary>Dynamic programming implementation of the <see cref="ILargestSquare"/> contract.</summary>
    public sealed class DynamicProgramming : ILargestSquare
    {
        public uint GetLargestSquareArea(in ReadOnlySpan2D<bool> matrix)
        {
            /* The method scans the input matrix left-to-right, top-to bottom, testing if the cell being process is
                the bottom-right corner of a square. If the cell value is set to `true`, then the side length of
                the square is stored in an auxiliary collection; otherwise (value is `false`) the cell is not deemed
                a part of a square (side length set to `0`).
               The `squareSides` array holds the side length of squares whose bottom-right corner is in the row above
                the current one (`i-1`). Its length is `m+n`, and all elements are initialized to `0`, for they refer to
                the virtual `-1` row and `-1` column, so there are no superfluous if-else statements. The `prevSide`
                variable holds the value relevant to the previous cell (`j-1`).
               Hence, for every row iteration the `prevRow` array segment is slid one cell left which corresponds to
                discarding the last array element, as it will not be updated in the next loop run, and prepending `0`
                to account for the virtual `-1` column.
               As the method employs an optimal substructure (`squareSides` / `prevRow`) that is used for overlapping
                sub-problems (a cell can be a part of already identified square), the algorithm satisfies the
                Dynamic Programming optimization method definition.
             * Time complexity: O(m*n), as there is a linear O(n) loop embedded in another linear loop O(m).
             * Space complexity: O(m+n) due to the ancillary `squareSides` array size. As a matter of fact, space
                complexity can be reduced to O(n) by doing away with the `squareSides` and promoting `prevRow` to
                a dynamic collection, for example `List<uint>`. Then at every row iteration the collection would have to
                be modified, so the last element is discarded and 0 is prepended, incurring performance degradation.
                This is an academic example of the time-memory complexity tradeoff.
             */
            
            var (m, n) = (matrix.Height, matrix.Width);  // rows, columns
            var (squareSides, maxSide) = (new uint[m+n], new uint[1]);
            
            // iterate over rows
            for (var i = 0; i < m; i++)
            {
                var prevRow = new ArraySegment<uint>(squareSides, m-i-1, n+1);
                var prevSide = new uint[1];
                
                // iterate over columns (row cells)
                for (var j = 0; j < n; j++)
                    prevRow[j] = prevSide[0] = matrix[i, j] switch {
                        // bottom-right square corner test, followed by updating the side length accordingly
                        true => 1 + prevSide.Concat(prevRow[j..(j+2)]).Min(),
                        false => 0U
                    };
                
                maxSide[0] = maxSide.Concat(prevRow).Max();
            }
            
            return checked(maxSide[0] * maxSide[0]);
        }
    }
}