namespace DueDateCalc.Exceptions;

public class WorkingHourException : Exception
{
    public WorkingHourException()
    {
    }

    public WorkingHourException(string? message) : base(message)
    {
    }

    public WorkingHourException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
