using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneSystem.ATSModel;
using TelephoneSystem.BillingSystem;

namespace TelephoneSystem
{
  
    public  class CallInfo
    {
        public DateTime TimeStartCall { get; set; }

        public DateTime TimeEndCall { get; set; }

        public int Duration
        {
            get { return CalculateDuration(); } 
        }

        public int OutgoingNumber { get; set; }

        public int IngoingNumber { get; set; }

        public double CostCall { get; set; }


        private int CalculateDuration()
        {
            return (TimeEndCall - TimeStartCall).Minutes;
        }

        public CallInfo(DateTime starCall, DateTime endCall, int ingoingNumber, int outgoingNumber)
        {
            this.TimeStartCall = starCall;
            this.TimeEndCall = endCall;
            this.IngoingNumber = ingoingNumber;
            this.OutgoingNumber = outgoingNumber;
        }

        public CallInfo(int ingoingNumber, int outgoingNumber)
        { 
            this.IngoingNumber = ingoingNumber;
            this.OutgoingNumber = outgoingNumber;
        }
    }
}
