using DueDateCalc.Exceptions;
using DueDateCalc.Extensions;

namespace DueDateCalc;

public static class TaskCompletionCalculator
{
    public static DateTime CalculateDueDate(DateTime date, int turnAroundTime)
    {
        try
        {
            turnAroundTime.VerifyIsGreaterThanZero();

            date.VerifyIsWorkingHours();

            var completedDate = GetCompletionDate(date, turnAroundTime);
            
            return completedDate;
        }
        catch(WorkingHourException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new DueDateCalculatorException("Failed to complete CalculateDueDate", ex);
        }
    }

    public static DateTime GetCompletionDate(DateTime date, double turnAroundTime)
    {
        while (turnAroundTime > 0)
        {
            var remainingTime = 17 - date.Hour;

            if (turnAroundTime >= remainingTime)
            {
                turnAroundTime -= remainingTime;
                date = date.GetNextWorkingDay();
            }
            else
            {
                date = date.AddHours(turnAroundTime);
                turnAroundTime = 0;
            }
        }

        return date;
    }
}
