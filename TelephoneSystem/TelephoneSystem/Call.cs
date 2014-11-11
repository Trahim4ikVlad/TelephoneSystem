using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneSystem.ATSModel;
using TelephoneSystem.BillingSystem;

namespace TelephoneSystem
{
    public  class Call
    {
        public DateTime DateTimeCall { get; set; }
     
        public int Duration { get; set; }
        
        public Port Port { get; set; }
        
        public bool IsOutgoig { get; set; }

        public Call(DateTime dateTimeCall, Port port, bool isOutgoing)
        {
            this.DateTimeCall = dateTimeCall;
            this.IsOutgoig = isOutgoing;
            this.Port = port;
        }
    }
}
