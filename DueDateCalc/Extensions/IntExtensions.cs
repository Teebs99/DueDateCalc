namespace DueDateCalc.Extensions;

public static class IntExtensions
{
    public static void VerifyIsGreaterThanZero(this int num)
    {
        if(num < 1)
        {
            throw new ArgumentException($"The given number should be greater than zero. Given number: {num}");
        }
    }
}
