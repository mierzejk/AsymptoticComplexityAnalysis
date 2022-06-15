# TaskTwo: Largest Square Area Problem
> Given a two-dimensional matrix of binary values, return the area of the largest square that contains only ones.

The **DynamicProgramming::GetLargestSquareArea** method scans the input matrix left-to-right, top-to bottom, testing if the cell being process is the bottom-right corner of a square. If the cell value is set to `true`, then the side length of the square is stored in an auxiliary collection; otherwise (value is `false`) the cell is not deemed a part of a square (side length set to `0`).

The `squareSides` array holds the side length of squares whose bottom-right corner is in the row above the current one (`i-1`). Its length is `m+n`, and all elements are initialized to `0`, for they refer to the _virtual_ `-1` row and `-1` column, so there are no superfluous _if-else_ statements. The `prevSide` variable holds the value relevant to the previous cell (`j-1`).

Hence, for every row iteration the `prevRow` array segment is slid one cell left which corresponds to discarding the last array element, as it will not be updated in the next loop run, and prepending `0` to account for the _virtual_ `-1` column.

As the method employs an _optimal substructure_ (`squareSides` / `prevRow`) that is used in _overlapping sub-problems_ (a cell can be part of already identified square), the algorithm satisfies the **Dynamic Programming** optimization method definition.
* Time complexity: **`O(m⋅n)`**, as there is a linear **`O(n)`** loop (column iteration) embedded in another linear loop **`O(m)`** (row iteration).
* Space complexity: **`O(m+n)`** due to the ancillary `squareSides` array size.  
As a matter of fact, space complexity can be reduced to `O(n)` by doing away with the `squareSides` and promoting `prevRow` to a dynamic collection, for example `List<uint>`. Then at every row iteration the collection would have to be modified, so the last element is discarded and `0` is prepended, incurring performance degradation. This is an academic example of the time-memory complexity tradeoff.
## Prototype
To facilitate .NET 6 / C# 10 solution implementation, the following Python 3.10 prototype has been created. The input `matrix` is comprised of `'0'` and `'1'` charactrers.
```
m, n = len(matrix), len(matrix[0])  # rows, columns
max_side, square_sides = 0, [0] * (n+1)

# iterate over rows
for i in range(m):
    prev_side = 0
	# iterate over columns (row cells)
    for j in range(n):
        square_sides[j] = prev_side = 0 if r'0' == matrix[i][j] else 1 + min(prev_side, *square_sides[j:j+2])

    square_sides.pop()
    square_sides.insert(0, 0)
    max_side = max(max_side, *square_sides)

return max_side ** 2
```
