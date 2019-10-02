using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLog
{
    public class Business
    {
        public int BusinessID { get; set; }
        public string CustomerCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNotes { get; set; }
        public string OutsideRep { get; set; }
        public Address Address { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Call> Calls { get; set; }
    }
}
