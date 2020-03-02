using Altkom.ExtensionMethods;

using System;
using System.Collections.Generic;

using System.Linq;

namespace ExtensionMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // DateTime.DaysInMonth

            if (DateTime.Today.IsHoliday())
            {

            }

            DateTime day = DateTime.Today.AddWorkingDays(10);

            if (DateTimeHelper.IsHoliday(DateTime.Today))
            {

            }

            
            DateTime.Today.IsDaylightSavingTime();

            IEnumerable<int> numbers = new List<int> { 49, 54, 76, 74, 10, 5 };

            Person person = new Person { FirstName = "Marcin" };

            foreach (var child in person)
            {

            }

            var myNumbers = numbers.Where(n => n > 50).OrderBy(n=>n);

        }
    }





}
