using System;

namespace ClassModels.CallClasses
{
    public class DTCall
    {

        public int CallID { get; set; }
        public DateTime CallDate { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CallNotes { get; set; }
        public bool CallResolved { get; set; }

        public DTCall(int call, DateTime date, string contName, string compName, string city, string state, string notes, bool resolved)
        {
            CallID = call;
            CallDate = date;
            ContactName = contName;
            CompanyName = compName;
            City = city;
            State = state;
            CallNotes = notes;
            CallResolved = resolved;
        }


        public DTCall()
        {
        }

    }
}
