using TaskOne.Utils;

namespace TaskOneTests;

public sealed class ReverseComparerTests
{
    private readonly IComparer<int> _reverseComparer = ReverseComparer<int>.Default;

    [Test]
    public void Compare_DifferentNumbers_Reversed()
    {
        // Arrange & Act
        var result = this._reverseComparer.Compare(2, 5);
        // Assert
        Assert.That(result, Is.GreaterThan(0));
    }
}
