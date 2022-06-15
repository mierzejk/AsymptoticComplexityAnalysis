using TaskOne.Utils;

namespace TaskOneTests;

public abstract class SortTests
{
    private Func<IEnumerable<double>, bool, double[]>? _sortMethod;

    private double[] AscendingSort(IEnumerable<double> collection) => this._sortMethod!(collection, false);
    
    private double[] ReversedSort(IEnumerable<double> collection) => this._sortMethod!(collection, true);

    protected abstract CompareSort<double> GetSortAlgorithm();

    [SetUp]
    public void Init() => this._sortMethod = this.GetSortAlgorithm().Sort;

    [Test]
    public void Sort_Null_ArgumentNullException()
    {
        // Arrange
        IEnumerable<double>? input = null;
        // Act & Assert
        Assert.Catch<ArgumentNullException>(() => this.AscendingSort(input!));
    }
}
