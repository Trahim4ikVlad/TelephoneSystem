using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneSystem.EventArgsChildren
{
    public class EventArgsFinishCall:EventArgs
    {
       public CallInfo CallInfo { get; set; }
    }
}
