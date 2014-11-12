using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneSystem.EventArgsChildren
{
    public class EventArgsCall:EventArgs
    {
        public int PhoneNumber { get; set; }

        public DateTime StartCall { get; set; }

        public DateTime EndCall { get; set; }
    }
}
