using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Reflections.Models
{
    [Icon("customer.jpg")]
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(int id, string firstName, string lastName, decimal salary)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
        }
        
        public int Id { get; set; }
        [Description("Imię")]
        [Required]        
        public string FirstName { get; set; }
        
        [Description("Nazwisko")] 
        public string LastName { get; set; }

        [Description("Wynagrodzenie")]
        [Icon("image1.jpg")]
        public decimal Salary { get; set; }


        //public object this[string propertyName]
        //{
        //    get => GetType().GetProperty(propertyName).GetValue(this);
        //    set => GetType().GetProperty(propertyName).SetValue(this, value, null);
        //}


    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class IconAttribute : Attribute
    {
        public IconAttribute(string filename)
        {
            Filename = filename;
        }

        public string Filename { get; set; }
    }

    public class Order
    {
        public Order()
        {
            this.OrderDate = DateTime.Now;
        }


        public Order(string number, decimal totalAmount, Customer customer)
            : this()
        {            
            Number = number;
            TotalAmount = totalAmount;
            Customer = customer;
        }

        public string Number { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Customer Customer { get; set; }
    }
}
