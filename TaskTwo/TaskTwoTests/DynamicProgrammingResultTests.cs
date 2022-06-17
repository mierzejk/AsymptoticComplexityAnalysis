using Microsoft.Toolkit.HighPerformance;

using TaskTwo;
using TaskTwo.Contracts;

namespace TaskTwoTests;
    
public sealed class DynamicProgrammingResultTests
{
    private readonly ILargestSquare _algorithm = new DynamicProgramming();
        
    [Test]
    public void LargestSquare_EmptyMatrix_Zero()
    {
        // Arrange
        var matrix = new bool[,] { };
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.Zero(result);
    }
        
    [Test]
    public void LargestSquare_ZeroHeight_Zero()
    {
        // Arrange
        var matrix = new bool[0, 10];
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.Zero(result);
    }
        
    [Test]
    public void LargestSquare_ZeroWidth_Zero()
    {
        // Arrange
        var matrix = new bool[5, 0];
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.Zero(result);
    }
        
    [Test]
    public void LargestSquare_AllZero_Zero()
    {
        // Arrange
        var matrix = new bool[5, 10];
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.Zero(result);
    }
        
    [Test]
    public void LargestSquare_SquareAllOne_SideSquared()
    {
        // Arrange
        const int sideLength = 10;
        Span2D<bool> matrix = new bool[sideLength, sideLength];
        matrix.Fill(true);
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.AreEqual(result, sideLength * sideLength);
    }
        
    [Test]
    public void LargestSquare_VerticalRectangleAllOne_ShortSideSquared()
    {
        // Arrange
        const int sideLength = 10;
        Span2D<bool> matrix = new bool[sideLength+5, sideLength];
        matrix.Fill(true);
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.AreEqual(result, sideLength * sideLength);
    }
        
    [Test]
    public void LargestSquare_HorizontalRectangleAllOne_ShortSideSquared()
    {
        // Arrange
        const int sideLength = 10;
        Span2D<bool> matrix = new bool[sideLength, sideLength+5];
        matrix.Fill(true);
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.AreEqual(result, sideLength * sideLength);
    }
        
    [Test]
    public void LargestSquare_ZeroPerimeter_InnerSideSquared()
    {
        // Arrange
        const int sideLength = 10;
        Span2D<bool> matrix = new bool[sideLength+2, sideLength+2];
        matrix[1..^1, 1..^1].Fill(true);
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.AreEqual(result, sideLength * sideLength);
    }
        
    [Test]
    public void LargestSquare_SeparateSameSizeSquares_OneSideSquared()
    {
        // Arrange
        const int sideLength = 5;
        Span2D<bool> matrix = new bool[2*sideLength+3, 2*sideLength+2];
        matrix[1..(sideLength+1), ..sideLength].Fill(true);
        matrix[(sideLength+2)..(2*sideLength+2), (sideLength+1)..(2*sideLength+1)].Fill(true);
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.AreEqual(result, sideLength * sideLength);
    }
        
    [Test]
    public void LargestSquare_SeparateDifferentSizeSquares_LargestSquareArea()
    {
        // Arrange
        const int sideLength = 5;
        Span2D<bool> matrix = new bool[2*sideLength+3, 2*sideLength+2];
        matrix[1..(sideLength+1), ..sideLength].Fill(true);  // large square
        matrix[(sideLength+2)..(2*sideLength), (sideLength+1)..(2*sideLength)].Fill(true); // small square
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.AreEqual(result, sideLength * sideLength);
    }
        
    [Test]
    public void LargestSquare_OverlappingFigures_SquareArea()
    {
        // Arrange
        const int sideLength = 8;
        Span2D<bool> matrix = new bool[7*sideLength/4, sideLength+2];
        matrix[..sideLength, 1..(sideLength+1)].Fill(true); // square
        matrix[.., 1..(sideLength/2)].Fill(true); // overlapping vertical rectangle
        // Act
        var result = this._algorithm.GetLargestSquareArea(matrix);
        // Assert
        Assert.AreEqual(result, sideLength * sideLength);
    }
}