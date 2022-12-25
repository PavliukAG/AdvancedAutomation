using System;
using System.Collections.Generic;

namespace FactoryMethod
{
    public abstract class EndPeriod
    {
        public abstract object FindEnd();
        public object GetEndTimespan()
        {
            return FindEnd();
        }
    }

    public class DayEnd : EndPeriod
    {
        TimeSpan todayDay = TimeSpan.FromHours( DateTime.Now.Hour);
        public override object FindEnd()
        { 
            return 24 - todayDay.Hours;
        }
    }

    public class WeekEnd : EndPeriod
    {
        DayOfWeek weekDay = DateTime.Now.DayOfWeek;
        public override object FindEnd()
        {
            return DayOfWeek.Sunday - weekDay;
        }
    }

    public class MonthEnd : EndPeriod
    {
        TimeSpan dayOfMonth = TimeSpan.FromDays(DateTime.Now.Day);
        public override object FindEnd()
        {
            !!!!!!!!!
            return TimeSpan.MaxValue.TotalDays;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*TimeSpan timeSpan1 = TimeSpan.MinValue;
            TimeSpan timeSpan = DateTime.Now.TimeOfDay;
            var week = DateTime.Now.DayOfWeek;
            int timeToTheEnd = 24 - timeSpan.Hours;
            var weekToTheEnd = DateTime.Today.DayOfWeek.ToString();
            var dayInTheMonth = TimeSpan.FromDays(DateTime.Now.DayOfWeek);
            int time = TimeSpan.MaxValue.Hours - timeSpan.Hours;
            var var1 = TimeSpan.MaxValue.Days;
            var var2 = TimeSpan.MaxValue.TotalDays;*/

            TimeSpan timeSpan = DateTime.Now.;
            EndPeriod endPeriod = new DayEnd();
            var time = endPeriod.FindEnd(timeSpan);
            Console.WriteLine("Hello World!");
        }

    }
}
