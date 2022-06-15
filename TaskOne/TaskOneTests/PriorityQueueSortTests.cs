using TaskOne;

namespace TaskOneTests;

public sealed class PriorityQueueSortTests : SortTests
{
    protected override PriorityQueueSort<double> GetSortAlgorithm() => new();
}
