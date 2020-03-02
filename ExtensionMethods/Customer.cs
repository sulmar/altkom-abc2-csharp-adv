using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExtensionMethods
{
    public class Person : IEnumerable<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person[] Children { get; set; }

        public Person Current => throw new NotImplementedException();

        public IEnumerator<Person> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
