using Microsoft.Toolkit.HighPerformance;

namespace TaskTwo.Contracts;

/// <summary>
/// Defines a method to get the area of the largest square comprised of true values.
/// </summary>
public interface ILargestSquare
{
    /// <summary>
    /// Given a two-dimensional matrix of <see cref="bool"/> values, returns the area of the largest square that
    /// contains only true elements.
    /// </summary>
    /// <param name="matrix">A two-dimensional array of binary values.</param>
    /// <returns>The area of the largest square that contains only true values.</returns>
    uint GetLargestSquareArea(in ReadOnlySpan2D<bool> matrix);
}