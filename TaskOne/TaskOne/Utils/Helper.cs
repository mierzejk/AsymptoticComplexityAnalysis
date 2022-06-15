namespace TaskOne.Utils;

internal static class Helper
{
    public static (T1, T2) Identity<T1, T2>(T1 arg1, T2 arg2) => (arg1, arg2);
}