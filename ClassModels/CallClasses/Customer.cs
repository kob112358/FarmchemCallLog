using System.Collections.Generic;

namespace CallLog
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string CustomerNotes { get; set; }
        public Business Business { get; set; }

        public ICollection<Call> Calls { get; set; }
    }
}
