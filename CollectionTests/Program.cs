using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionTests
{
    class Program
    {
        static void Main(string[] args)
        {

            var customerRepository = new DbCustomerRepository();

            customerRepository.Add(new Customer());

            // customerRepository.Save("customers.txt");




            IList<int> numbers = new List<int> { 49, 54, 76, 74, 10, 5 };


           

            numbers.Add(100);

            // numbers[2]
            
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }




        }
    }
}
