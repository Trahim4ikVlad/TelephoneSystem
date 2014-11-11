using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneSystem.BillingSystem
{
    public class Business:TariffPlan
    {
        public Business(DateTime connection, double outCostMinute, double inCostMinute) : base(connection, outCostMinute, inCostMinute)
        {
        }
    }
}
