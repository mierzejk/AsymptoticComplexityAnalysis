using TaskOne.Utils;

namespace TaskOneTests;

public sealed class HelperTests
{
    [Test]
    public void ToSmartArray_Null_ArgumentNullException()
    {
        // Arrange
        IEnumerable<object>? input = null;
        // Act & Assert
        Assert.Catch<ArgumentNullException>(() => input!.ToArraySmart());
    }
    
    [Test]
    public void ToSmartArray_Array_Same()
    {
        // Arrange
        var input = new[] {6, 8, 12};
        // Act
        var result = input.ToArraySmart();
        // Assert
        CollectionAssert.AreEqual(input, result);
    }
    
    [Test]
    public void ToSmartArray_List_Same()
    {
        // Arrange
        var input = new List<uint> {6, 8, 12, 0};
        // Act
        var result = input.ToArraySmart();
        // Assert
        CollectionAssert.AreEqual(input, result);
    }
    
    [Test]
    public void ToSmartArray_Sized_Same()
    {
        // Arrange
        var input = new HashSet<string?> {@"text", string.Empty, @"technical assignment", @"text", null};
        // Act
        var result = input.ToArraySmart();
        // Assert
        CollectionAssert.AreEquivalent(input, result);
    }
    
    [Test]
    public void ToSmartArray_EmptyEnumerable_EmptyArray()
    {
        // Arrange
        var input = Enumerable.Empty<byte>();
        // Act
        var result = input.ToArraySmart();
        // Assert
        CollectionAssert.IsEmpty(result);
    }
}
