﻿namespace CallLog
{
    public class Call
    {
        public int CallID { get; set; }
        public Customer Cust { get; set; }
        public Business Bus { get; set; }
        public Address Add { get; set; }
        public CallInfo CallInformation { get; set; }

    }
}
