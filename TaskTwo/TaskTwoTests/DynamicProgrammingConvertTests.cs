using System;
using Microsoft.Toolkit.HighPerformance;
using NUnit.Framework;

namespace TaskTwoTests
{
    using TaskTwo;
    using TaskTwo.Contracts;
    using TaskTwo.Utils;
    
    public sealed class DynamicProgrammingConvertTests
    {
        private readonly ILargestSquare _algorithm = new DynamicProgramming();
        
        [Test]
        public void LargestSquare_UintMatrix_ExplicitConverted()
        {
            // Arrange
            const int sideLength = 4;
            Memory2D<uint> matrix = new uint[sideLength, sideLength];
            matrix.Span.Fill(1U);
            // Act
            var result = this._algorithm.GetLargestSquareArea(matrix);
            // Assert
            Assert.AreEqual(result, sideLength * sideLength);
        }
        
        [Test]
        public void LargestSquare_IntMatrix_ExplicitConverted()
        {
            // Arrange
            const int sideLength = 4;
            var matrix = new int[sideLength, sideLength];
            // Act
            var result = this._algorithm.GetLargestSquareArea(matrix);
            // Assert
            Assert.Zero(result);
        }
        
        [Test]
        public void LargestSquare_StrMatrix_ExplicitConverted()
        {
            // Arrange
            var matrix = new[,]{{@"0", @"1"}, {@"1", @"0"}};
            // Act
            var result = this._algorithm.GetLargestSquareArea<string>(
                matrix,
                s => s switch
                {
                    @"1" => true,
                    _ => false
                });
            // Assert
            Assert.NotZero(result);
        }
        
        [Test]
        public void LargestSquare_NotSupportedDateTimeMatrix_ArgumentException()
        {
            // Arrange
            var matrix = new DateTime[0, 0];
            // Act & Assert
            Assert.Catch<ArgumentException>(() => this._algorithm.GetLargestSquareArea<DateTime>(matrix));
        }
    }
}