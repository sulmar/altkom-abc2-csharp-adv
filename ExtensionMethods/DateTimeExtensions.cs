using System;

namespace Altkom.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static bool IsHoliday(this DateTime dateTime)
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Saturday
                || DateTime.Today.DayOfWeek == DayOfWeek.Sunday;
        }

        public static DateTime AddWorkingDays(this DateTime dateTime, int days)
        {
            return dateTime.AddDays(days);
        }

        public static bool IsDaylightSavingTime(this DateTime dateTime)
        {
            return true;
        }


    }





}
