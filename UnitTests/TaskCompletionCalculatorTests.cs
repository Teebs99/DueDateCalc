using DueDateCalc;
using DueDateCalc.Exceptions;

namespace UnitTests;

public class TaskCompletionCalculatorTests
{
    const int dayStart = 9;
    const int dayEnd = 17;

    [Fact]
    public void Expect_SameWorkingDay()
    {
        var day = new DateTime(2000, 1, 3, dayStart, 0, 0);

        var turnAroundTime = 5;
        var expectedEndTime = dayStart + turnAroundTime;

        var finishDateTime = TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime);

        Assert.Equal(expectedEndTime, finishDateTime.Hour);
        Assert.Equal(day.Day, finishDateTime.Day);
        Assert.Equal(day.Minute, finishDateTime.Minute);
    }

    [Fact]
    public void Expect_NextWorkingDay()
    {
        var day = new DateTime(2000, 1, 3, dayStart, 0, 0);

        var turnAroundTime = 12;
        var expectedEndTime = 13;
        var expectedFinishDay = day.AddDays(1).Day;

        var finishDateTime = TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime);

        Assert.Equal(expectedEndTime, finishDateTime.Hour);
        Assert.Equal(expectedFinishDay, finishDateTime.Day);
        Assert.Equal(day.Minute, finishDateTime.Minute);
    }

    [Fact]
    public void Expect_CompletionAfterWeekend()
    {
        var day = new DateTime(2000, 1, 7, dayStart, 1, 0);

        var turnAroundTime = 8;

        var finishDateTime = TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime);

        Assert.Equal(dayStart, finishDateTime.Hour);
        Assert.Equal(DayOfWeek.Monday, finishDateTime.DayOfWeek);
        Assert.Equal(day.Minute, finishDateTime.Minute);
    }

    [Fact]
    public void Expect_NextWorkingDayWithNonZeroMinutes()
    {
        var day = new DateTime(2000, 1, 3, dayStart, 1, 0);

        var turnAroundTime = 8;
        var expectedEndTime = 9;
        var expectedFinishDay = day.AddDays(1).Day;

        var finishDateTime = TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime);

        Assert.Equal(expectedEndTime, finishDateTime.Hour);
        Assert.Equal(expectedFinishDay, finishDateTime.Day);
        Assert.Equal(day.Minute, finishDateTime.Minute);
    }

    [Fact]
    public void Expect_NextWorkingDayWithNewMonth()
    {
        var day = new DateTime(2000, 1, 31, dayStart, 1, 0);

        var turnAroundTime = 8;
        var expectedEndTime = 9;
        var expectedFinishDay = day.AddDays(1).Day;

        var finishDateTime = TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime);

        Assert.Equal(expectedEndTime, finishDateTime.Hour);
        Assert.Equal(expectedFinishDay, finishDateTime.Day);
        Assert.Equal(day.Minute, finishDateTime.Minute);
        Assert.Equal(2, finishDateTime.Month);
    }

    [Fact]
    public void Expect_NextWorkingDayWithNewYear()
    {
        var day = new DateTime(2001, 12, 31, dayStart, 1, 0);

        var turnAroundTime = 8;
        var expectedEndTime = 9;
        var expectedFinishDay = day.AddDays(1).Day;

        var finishDateTime = TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime);

        Assert.Equal(expectedEndTime, finishDateTime.Hour);
        Assert.Equal(expectedFinishDay, finishDateTime.Day);
        Assert.Equal(day.Minute, finishDateTime.Minute);
        Assert.Equal(1, finishDateTime.Month);
        Assert.Equal(2002, finishDateTime.Year);
    }

    [Fact]
    public void Expect_MultidayTask()
    {
        var startAndEndTime = 14;
        var day = new DateTime(2000, 1, 3, startAndEndTime, 12, 0);

        var turnAroundTime = 16;
        var expectedFinishDay = day.AddDays(2).Day;

        var finishDateTime = TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime);

        Assert.Equal(startAndEndTime, finishDateTime.Hour);
        Assert.Equal(expectedFinishDay, finishDateTime.Day);
        Assert.Equal(day.Minute, finishDateTime.Minute);
    }

    [Fact]
    public void Expect_FullWorkWeekTask()
    {
        var startAndEndTime = 14;
        var day = new DateTime(2000, 1, 3, startAndEndTime, 12, 0);

        var turnAroundTime = 40;
        var expectedFinishDay = day.AddDays(7).Day;

        var finishDateTime = TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime);

        Assert.Equal(startAndEndTime, finishDateTime.Hour);
        Assert.Equal(expectedFinishDay, finishDateTime.Day);
        Assert.Equal(day.Minute, finishDateTime.Minute);
    }

    [Fact]
    public void ExpectException_BecauseOfWeekend()
    {
        var day = new DateTime(2000, 1, 1, dayStart, 1, 0);

        var turnAroundTime = 8;

        Assert.Throws<WorkingHourException>(() => TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime));
    }

    [Fact]
    public void ExpectException_BecauseOutsideWorkingHours()
    {
        var day = new DateTime(2000, 1, 1, dayStart - 1, 1, 0);

        var turnAroundTime = 8;

        Assert.Throws<WorkingHourException>(() => TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime));
    }

    [Fact]
    public void ExpectException_TurnAroundTimeIsZero()
    {
        var day = new DateTime(2000, 1, 1, dayStart - 1, 1, 0);

        var turnAroundTime = 0;

        Assert.Throws<DueDateCalculatorException>(() => TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime));
    }

    [Fact]
    public void ExpectException_TurnAroundTimeIsNegative()
    {
        var day = new DateTime(2000, 1, 1, dayStart - 1, 1, 0);

        var turnAroundTime = 0;

        Assert.Throws<DueDateCalculatorException>(() => TaskCompletionCalculator.CalculateDueDate(day, turnAroundTime));
    }
}