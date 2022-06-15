using TaskOne;

namespace TaskOneTests;

public sealed class HeapSortTests : SortTests
{
    protected override HeapSort<double> GetSortAlgorithm() => new();
}
