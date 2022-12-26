using System;

namespace FactoryMethod
{
    public interface IEndPeriod
    {
        public object FindEnd();
    }

    public class DayEnd : IEndPeriod
    {
        DateTime dateTime = DateTime.Now;

        public object FindEnd()
        {
            return dateTime.Date.AddDays(1).AddTicks(-0);
        }
    }

    public class WeekEnd : IEndPeriod
    {
        DateTime dateTime = DateTime.Now;

        public object FindEnd()
        {
            while(dateTime.DayOfWeek != DayOfWeek.Sunday)
            {
                dateTime = dateTime.AddDays(1);
            }
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }
    }

    public class MonthEnd : IEndPeriod
    {
        DateTime dateTime = DateTime.Now;

        public object FindEnd()
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month)).AddDays(1).AddTicks(-1);
        }
    }

    public enum PeriodType
    {
        Day,
        Week,
        Month
    }

    public class GetEndOfThePeriod
    {
        public static IEndPeriod ReturnPeriod(PeriodType period)
        {
            IEndPeriod endPeriod;
            switch (period)
            {
                case PeriodType.Day:
                    endPeriod = new DayEnd();
                    break;
                case PeriodType.Week:
                    endPeriod = new WeekEnd();
                    break;
                case PeriodType.Month:
                    endPeriod = new MonthEnd();
                    break;
                default:
                    endPeriod = new DayEnd();
                    break;
            }
            return endPeriod;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var period = GetEndOfThePeriod.ReturnPeriod(PeriodType.Week);
            Console.WriteLine(period.FindEnd());
            Console.ReadKey();
        }

    }
}
