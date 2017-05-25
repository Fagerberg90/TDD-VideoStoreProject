using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
   public class Customer
    {
        public string Name { get; set; }
        public string Ssn { get; set; }
        public Customer(string name, string ssn)
        {
            this.Name = name;
            this.Ssn = ssn;
        }
        public Customer()
        {

        }
    }
}
