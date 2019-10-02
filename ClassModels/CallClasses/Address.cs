using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLog
{
    public class Address
    {
        public int AddressID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Call> Calls { get; set; }
       
    }
}
