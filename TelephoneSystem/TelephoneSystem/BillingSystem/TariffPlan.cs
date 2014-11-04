using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneSystem.BillingSystem
{
    public abstract class TariffPlan
    {
        public double SubscriberPay { get; set; } 

        public double CostMinute { get; set; }

    }
}
