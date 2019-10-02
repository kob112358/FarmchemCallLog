using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallLog
{
    public class CallInfo
    {
        public int CallInfoID { get; set; }
        public string ReasonForCall { get; set; }
        public string CallNotes { get; set; }
        public bool CallResolved  { get; set; }
        public DateTime CallDate { get; set; }


        public ICollection<Call> Calls { get; set; }
    }
}
