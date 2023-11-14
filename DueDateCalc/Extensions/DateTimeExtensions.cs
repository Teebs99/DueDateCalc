using DueDateCalc.Exceptions;

namespace DueDateCalc.Extensions;

public static class DateTimeExtensions
{
    public static void VerifyIsWorkingHours(this DateTime time)
    {
        if(time.DayOfWeek == DayOfWeek.Sunday || time.DayOfWeek == DayOfWeek.Saturday)
        {
            throw new WorkingHourException("Given day is a weekend. Please submit tasks during the week");
        }

        if(time.Hour < 9 || time.Hour >= 17)
        {
            throw new WorkingHourException("Given time is not within normal working hours. Please submit tasks between 9AM-5PM.");
        }
    }

    public static DateTime GetNextWorkingDay(this DateTime time)
    {
        var backToDayStart = time.Hour - 9;

        var newTime = time.AddHours(-backToDayStart);

        if(time.DayOfWeek == DayOfWeek.Friday)
        {
            newTime = newTime.AddDays(3);
        }
        else
        {
            newTime = newTime.AddDays(1);
        }

        return newTime;
    }
}
