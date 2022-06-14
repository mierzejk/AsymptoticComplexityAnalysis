using System;
using Microsoft.Toolkit.HighPerformance;
using TaskTwo;
using TaskTwo.Utils;

var a = new DynamicProgramming();
bool[,] b = {{false, true}, {true, false}};
uint[,] v = {{1, 0, 1, 0, 0}, {1, 0, 1, 1, 1}, {1, 1, 1, 1, 1}, {1, 0, 0, 1, 0}};
int[,] vv = {{1, 0, 1, 0, 0}, {1, 1, 1, 1, 0}, {1, 1, 1, 1, 1}, {0, 0, 0, 0, 0}};
Console.WriteLine(a.GetLargestSquareArea(new bool[0, 0]));
Console.WriteLine(a.GetLargestSquareArea(new bool[4, 2]));
// Console.WriteLine(a.GetLargestSquareArea(b));
// Console.WriteLine(a.GetLargestSquareArea(vv));
Console.WriteLine(a.GetLargestSquareArea(v));
// ReadOnlyMemory2D<DateTime> dt = new DateTime[0, 0];
// Console.WriteLine(a.GetLargestSquareArea(dt));
Console.WriteLine(a.GetLargestSquareArea<DateTime>(new DateTime[0, 0]));

