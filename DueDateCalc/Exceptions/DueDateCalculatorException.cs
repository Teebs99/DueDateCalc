namespace DueDateCalc.Exceptions;

public class DueDateCalculatorException : Exception
{
    public DueDateCalculatorException()
    {
    }

    public DueDateCalculatorException(string? message) : base(message)
    {
    }

    public DueDateCalculatorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
