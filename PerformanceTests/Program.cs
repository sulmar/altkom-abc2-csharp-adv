using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using Reflections;
using Reflections.Models;
using System;
using System.Collections.Generic;

namespace PerformanceTests
{
    class Program
    {

        
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<FormmatersBenchmarks>();

#if DEBUG
            Console.WriteLine("type: dotnet run -c Release");
#endif
        }
    }



    // Install-Package BenchmarkDotNet
    
    [RankColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class FormmatersBenchmarks
    {
        private IList<Customer> customers = new List<Customer>();

        [Params(10, 100, 1000)]
        public int CustomerQuantity { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            // Install-Package Bogus
            customers = new Faker<Customer>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p => p.Salary, f => f.Random.Decimal(0, 1000))
                .Generate(CustomerQuantity);
        }

        [Benchmark]
        public void StringFormatter()
        {
            foreach (var customer in customers)
            {
                CustomerFormatter.ToText1(customer);
            }
        }

        [Benchmark]
        public void StringBuilderFormatter()
        {
            foreach (var customer in customers)
            {
                CustomerFormatter.ToText(customer);
            }
        }

        [Benchmark]
        public void ReflectionFormatter()
        {
            foreach (var customer in customers)
            {
                Formatter.ToText(customer);
            }
        }
    }
}
