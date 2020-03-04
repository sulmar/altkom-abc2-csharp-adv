using MethodTimer;
using Reflections.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Reflections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Customer customer = new Customer(1, "Marcin", "Sulecki", 100m);
            // customer["Salary"] = 200m;

            string text = Formatter.ToText(customer);
            Console.WriteLine(text);

            Order order = new Order("ZAM 001", 1000, customer);

            Console.WriteLine(Formatter.ToText(order));

        }
    }

    // Programowanie aspektowe

    // PM> Install-Package Fody
    // PM> Install-Package MethodTimer.Fody

    // Tworzenie wlasnych atrybytow
    // https://github.com/Fody/MethodDecorator

    public class Formatter
    {
        [Time]
        public static string ToText(object entity)
        {
//            Stopwatch stopwatch = Stopwatch.StartNew();

            StringBuilder builder = new StringBuilder();

            Type type = entity.GetType();

            PropertyInfo[] properties = type.GetProperties();

            foreach (var property in properties)
            {

                
                object value = property.GetValue(entity);

                DescriptionAttribute descriptionAttribute = property.GetCustomAttribute<DescriptionAttribute>();

                string name = descriptionAttribute != null ? descriptionAttribute.Description : property.Name;

                builder.AppendLine($"| {name} | {value} |");
            }

            string result = builder.ToString();

            //stopwatch.Stop();

            //Console.WriteLine(stopwatch.Elapsed);

            return result;
        }
    }

    public class CustomerFormatter
    {
        public static string ToText1(Customer customer)
        {
            // | Id        | 10
            // | FirstName | Marcin
            // | LastName  | Sulecki
            // | Salary    | 100

            string result = string.Empty;


            for (int i = 0; i < 500; i++)
            {
                result += $"| Id | {customer.Id} \n";
                result += $"| FirstName | {customer.FirstName} \n";
                result += $"| LastName | {customer.LastName} \n";
                result += $"| Salary | {customer.Salary} \n";

            }

            return result;
        }

        public static string ToText(Customer customer)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 500; i++)
            {
                builder.AppendLine($"| Id | {customer.Id}");
                builder.AppendLine($"| FirstName | {customer.FirstName}");
                builder.AppendLine($"| LastName | {customer.LastName}");
                builder.AppendLine($"| Salary | {customer.Salary}");

            }

            return builder.ToString();
        }

        
    }
}
