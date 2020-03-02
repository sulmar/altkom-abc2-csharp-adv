using System;

namespace ExtensionMethods
{
    public class DateTimeHelper
    {
        public static bool IsHoliday(DateTime dateTime)
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Saturday
                || DateTime.Today.DayOfWeek == DayOfWeek.Sunday;
        }
    }





}
