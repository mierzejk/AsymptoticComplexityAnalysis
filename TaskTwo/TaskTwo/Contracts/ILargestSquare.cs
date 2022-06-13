using Microsoft.Toolkit.HighPerformance;

namespace TaskTwo.Contracts
{
    public interface ILargestSquare
    {
        /// <summary>
        /// Given a two-dimensional matrix of <seealso cref="bool"/> values, returns the area of the largest square
        /// that contains only true elements.
        /// </summary>
        /// <param name="matrix">A two-dimensional array of binary values.</param>
        /// <returns>The area of the largest square that contains only true values.</returns>
        uint GetLargestSquareArea(in ReadOnlySpan2D<bool> matrix);
    }
}