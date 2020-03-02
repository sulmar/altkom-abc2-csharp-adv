using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionTests
{
    interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        void Add(Customer customer);
    }

    class DbCustomerRepository : ICustomerRepository
    {
        public void Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }
    }

    class FileCustomerRepository : ICustomerRepository
    {
        private readonly ICollection<Customer> customers;

        public void Save(string filename)
        {

        }

        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }
    }

    class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
