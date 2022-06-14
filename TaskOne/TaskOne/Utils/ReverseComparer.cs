using System;
using System.Collections.Generic;

namespace TaskOne.Utils;

/// <summary>
/// A class implementing the <see cref="IComparer{T}"/> that reverses the comparison result.
/// </summary>
/// <typeparam name="T">The type of objects to compare.</typeparam>
public class ReverseComparer<T> : Comparer<T>
{
    /// <summary>
    /// Returns a reversed sort order comparer for the type specified by the generic argument.
    /// </summary>
    public new static ReverseComparer<T> Default { get; } = new(Comparer<T>.Default);
        
    public Comparer<T> BaseComparer { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReverseComparer{T}"/> class to a specified base comparer.
    /// </summary>
    /// <param name="baseComparer">An instance of the <see cref="Comparer{T}"/> class, whose <see cref="Comparer{T}.Compare"/> result will be reversed.</param>
    public ReverseComparer(Comparer<T> baseComparer)
    {
        ArgumentNullException.ThrowIfNull(baseComparer);
        this.BaseComparer = baseComparer;
    }

    public override int Compare(T? x, T? y) => this.BaseComparer.Compare(y, x);
}