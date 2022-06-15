using TaskOne;

namespace TaskOneTests;

public sealed class IntroSortTests : SortTests
{
    protected override IntroSort<double> GetSortAlgorithm() => new();
}
