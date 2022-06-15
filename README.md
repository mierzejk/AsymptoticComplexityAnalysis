# TaskOne: Collection Sort Problem
> Order an array of double values.

The custom **HeapSort::Sort** method orders a collection of items that can be [compared](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icomparer-1). It exploits a binary max‑heap, backed by a contiguous array layout. There are two ways to _heapify_ a binary tree, so it satisfies the maximum heap property (or minimum just the opposite):
1. Start with an empty binary heap followed by adding every new element as the last leaf, and _swap_ it up with its parent as long as the node value is greater than the parent’s, or when the tree root is reached. Given `n` the input array length, namely the number of elements to add, the maximal path length between the root and a leaf is `log(n)` thanks to the binary tree design. Hence the time complexity of this approach is `O(n⋅log(n))`. Intuitively, this is due to the fact that the deepest and widest tree level requires the most shifts upwards, for the longest path is from a tree leaf to the root.
2. Alter the array of elements in place by swapping non‑leaf nodes down with the greatest of their children, until all child values are less or the bottom of the tree is reached. This approach accounts for `O(n)` time complexity, as only non-leaf nodes are shifted, whose maximal path downwards is shorter for levels with more elements (deeper levels) comparing to higher levels with less elements (closer to the root).

To optimize performance, the implementation provided makes use of the **2.** algorithm design to make up a heap with the input array.

Because the max‑heap property guarantees the maximal element is always the tree root, the sort procedure swaps the root (the first array element) with the last leaf (the last array element), so the greatest element is at the end of the collection. After the greatest element being now the last node is precluded from the structure, the tree is _heapified_ starting from the root, which takes up to `O(log(n))` (the tree depth). The action is repeated for every one of the `n` array item, imposing a total of `O(n⋅log(n))` time complexity, and resulting in the ordered input collection.
* The method overall time complexity is `O(n+n⋅log(n))`=**`O(n⋅log(n))`**.  
The linear time sorting algorithms, eg. _counting sort_ have not been considered for the task for the sake of a likely massive memory trade‑off, as the array elements are unbounded floating‑point double numbers.
* The memory complexity is just linear `O(n)` for the array holding the binary max‑heap node elements.
> Please be adivised that the base of logarithm equates to `2`, as the **binary** heap is being discussed. Therefore `log` refers precisely to the `log₂`.
## Supplementary methods
There are two more sorting procedures implemented in the solution:
* Another heapsort – **PriorityQueueSort**, that is based on an array-backed, quaternary min-heap available as the [.NET 6 PriorityQueue class](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2).
* [Introspective sort](https://en.wikipedia.org/wiki/Introsort) (**IntroSort**) implemented by the inherent [Array::Sort method](https://docs.microsoft.com/en-us/dotnet/api/system.array.sort), that combines an insertion sort, heapsort and quicksort, depending on the size of the input collection.

Unless there are some specific assumptions and requirements, any of these two out-of-the-box method are very likely to be recommended over a custom implementation in production settings.

# TaskTwo: Largest Square Area Problem
> Given a two-dimensional matrix of binary values, return the area of the largest square that contains only ones.

The **DynamicProgramming::GetLargestSquareArea** method scans the input matrix left‑to‑right, top‑to‑bottom, testing if the cell being process is the bottom-right corner of a square. If the cell value is set to `true`, then the side length of the square is stored in an auxiliary collection; otherwise (value is `false`) the cell is not deemed a part of a square (side length set to `0`).

The `squareSides` array holds the side length of squares whose bottom‑right corner is in the row above the current one (`i-1`). Its length is `m+n`, and all elements are initialized to `0`, for they refer to the _virtual_ `-1` row and `-1` column, so there are no superfluous _if-else_ statements. The `prevSide` variable holds the value relevant to the previous cell (`j-1`).

Hence, for every row iteration the `prevRow` array segment is slid one cell left which corresponds to discarding the last array element, as it will not be updated in the next loop run, and prepending `0` to account for the _virtual_ `-1` column.

As the method employs an _optimal substructure_ (`squareSides` / `prevRow`) that is used in _overlapping sub-problems_ (a cell can be part of already identified square), the algorithm satisfies the **Dynamic Programming** optimization method definition.
* Time complexity: **`O(m⋅n)`**, as there is a linear **`O(n)`** loop (column iteration) embedded in another linear loop **`O(m)`** (row iteration).
* Space complexity: **`O(m+n)`** due to the ancillary `squareSides` array size.  
As a matter of fact, space complexity can be reduced to `O(n)` by doing away with the `squareSides` and promoting `prevRow` to a dynamic collection, for example `List<uint>`. Then at every row iteration the collection would have to be modified, so the last element is discarded and `0` is prepended, incurring performance degradation. This is an academic example of the time‑memory complexity trade‑off.
## Prototype
To facilitate .NET 6 / C# 10 solution implementation, the following Python 3.10 prototype has been created. The input `matrix` is comprised of `'0'` and `'1'` characters.
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
