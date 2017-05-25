using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
   public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public Customer(string firstName, string lastName, string ssn)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Ssn = ssn;
        }
        public Customer()
        {

        }
    }
}
