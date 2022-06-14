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
        
    public Comparer<T> InnerComparer { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReverseComparer{T}"/> class to the <see cref="Comparer{T}"/>
    /// object, whose <seealso cref="Comparer{T}.Compare"/> result will be reversed.
    /// </summary>
    /// <param name="innerComparer"></param>
    public ReverseComparer(Comparer<T> innerComparer)
    {
        var t = new string("");
        ArgumentNullException.ThrowIfNull(innerComparer);
        this.InnerComparer = innerComparer;
    }

    public override int Compare(T? x, T? y) => this.InnerComparer.Compare(y, x);
}