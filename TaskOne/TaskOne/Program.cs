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
        var sort2 = new HeapSort<int>();
        // sort2.Sort(new[] {1, 1, 1, 1, 1}, reverse: false);
        sort2.Sort(new[] {1, 3, 2, 5, 4}, reverse: false);
        new HeapSort<double>().Sort(array, reverse: false);
        // Console.WriteLine(string.Join(@", ", sort2.Sort(new[] {1, 3, 2, 5, 4}, reverse: false)));

        // TODO: Mention O(n) - count sort?, exclude HeapExtensions, add introsort: https://docs.microsoft.com/en-us/dotnet/api/system.array.sort
    }
}