using TaskOne.Utils;

namespace TaskOneTests;

public abstract class SortTests
{
    private Func<IEnumerable<double>, bool, double[]>? _sortMethod;

    // ReSharper disable once MemberCanBePrivate.Global
    protected double[] AscendingSort(IEnumerable<double> collection) => this._sortMethod!(collection, false);
    
    // ReSharper disable once MemberCanBePrivate.Global
    protected double[] ReversedSort(IEnumerable<double> collection) => this._sortMethod!(collection, true);

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
    
    [Test]
    public void Sort_Empty_Empty()
    {
        // Arrange
        var input = Array.Empty<double>();
        // Act
        var result = this.AscendingSort(input);
        // Assert
        CollectionAssert.IsEmpty(result);
    }
    
    [Test]
    public void Sort_OneElement_OneElement()
    {
        // Arrange
        var input = new List<double> { 3.141592654D };
        // Act
        var result = this.AscendingSort(input);
        // Assert
        Assert.That(result, Has.Length.EqualTo(1));
        CollectionAssert.AreEqual(input, result);
    }
    
    [Test]
    public void Sort_UniqueCollection_SortedAscending()
    {
        // Arrange
        var input = new List<double> { 8D, 12.5D, 1024.5D, -0D, -6D, 0D };
        // Act
        var result = this.AscendingSort(input);
        // Assert
        Assert.That(result, Has.Length.EqualTo(input.Count));
        CollectionAssert.AreEqual(new[] { -6D, -0D, 0D, 8D, 12.5D, 1024.5D }, result);
    }
    
    [Test]
    public void SortReversed_UniqueCollection_SortedDescending()
    {
        // Arrange
        var input = new List<double> { 8D, 12.5D, 1024.5D, -0D, -6D, 0D };
        // Act
        var result = this.ReversedSort(input);
        // Assert
        Assert.That(result, Has.Length.EqualTo(input.Count));
        CollectionAssert.AreEqual(new[] { 1024.5D, 12.5D, 8D, 0D, -0D, -6D }, result);
    }
    
    [Test]
    public void Sort_DuplicatedItems_SortedAscending()
    {
        // Arrange
        var input = new List<double> { 8D, 12.5D, 8D, 12.5D, -8D, 8D };
        // Act
        var result = this.AscendingSort(input);
        input.Sort();
        // Assert
        Assert.That(result, Has.Length.EqualTo(input.Count));
        CollectionAssert.AreEqual(input, result);
    }
    
    [Test]
    public void SortReversed_DuplicatedItems_SortedDescending()
    {
        // Arrange
        var input = new List<double> { 8D, 12.5D, 8D, 12.5D, -8D, 8D };
        // Act
        var result = this.ReversedSort(input);
        input.Sort();
        input.Reverse();
        // Assert
        Assert.That(result, Has.Length.EqualTo(input.Count));
        CollectionAssert.AreEqual(input, result);
    }
    
    [Test]
    public void Sort_Collection_NotInPlace()
    {
        // Arrange
        var input = new [] { 3.141592654D, 9.80665D, 2.71828D };
        // Act
        var result = this.AscendingSort(input);
        // Assert
        Assert.That(result, Has.Length.EqualTo(input.Length));
        CollectionAssert.AreNotEqual(input, result);
    }
    
    [Test]
    public void Sort_BigRandomCollection_SortedAscending()
    {
        // Arrange
        var rnd = new Random();
        var input = new List<double>(short.MaxValue);
        input.AddRange(Enumerable.Range(0, short.MaxValue).Select(_ => rnd.NextDouble()));
        // Act
        var result = this.AscendingSort(input);
        // Assert
        Assert.That(result, Has.Length.EqualTo(input.Count));
        CollectionAssert.IsOrdered(result);
        CollectionAssert.AreEquivalent(input, result);
    }
}
