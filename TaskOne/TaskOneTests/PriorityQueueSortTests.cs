using TaskOne;

namespace TaskOneTests;

public sealed class PriorityQueueSortTests : SortTests
{
    protected override PriorityQueueSort<double> GetSortAlgorithm() => new();
    
    [Test]
    public void Sort_NonSizable_Sorted()
    {
        // Arrange
        var input = new List<double> { 8D, 12.5D, 1024.5D, -0D, -6D, 0D };
        var enumerable = from i in input
                         where i % 1 == 0
                         select i;
        // Act
        var result = this.AscendingSort(enumerable);
        // Assert
        Assert.That(result, Has.Length.EqualTo(4));
        CollectionAssert.AreEqual(new[] { -6D, -0D, 0D, 8D }, result);
    }
}
