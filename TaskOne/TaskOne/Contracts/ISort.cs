using System.Collections.Generic;

namespace TaskOne.Contracts;

/// <summary>
/// Defines a method to sort a collection of items.
/// </summary>
/// <typeparam name="T">The type of objects to sort.</typeparam>
public interface ISort<T>
{
    /// <summary>
    /// Sorts the <paramref name="collection"/> elements.
    /// </summary>
    /// <param name="collection">The collection to be sorted.</param>
    /// <param name="comparer">Provides means of comparing <paramref name="collection"/> elements.</param>
    /// <returns>An array of sorted <paramref name="collection"/> elements.</returns>
    T[] Sort(IEnumerable<T> collection, Comparer<T>? comparer);
}