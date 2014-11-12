using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneSystem.BillingSystem
{
    public class Home:TariffPlan
    {
        public Home(DateTime connection, double outCostMinute) : base(connection, outCostMinute)
        {
        }
    }
}
