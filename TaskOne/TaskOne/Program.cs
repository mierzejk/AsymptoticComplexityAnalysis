using System;
using System.Collections.Generic;
using System.Linq;

using static System.Linq.Enumerable;

namespace TaskOne;

using Utils;

public static class Program
{
    

    public static void Main(string[] args)
    {
        var array = new[] {1.0F, -0.0F, 0F, 2.5F, 123, 100, 44, 55, 44, -3.123};
        var sort = new PriorityQueueSort<double>();
        Console.WriteLine(string.Join(@", ", sort.Sort(array, reverse: false)));
    }
}