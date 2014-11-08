using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneSystem.BillingSystem
{
    public class Vip:TariffPlan
    {
        public Vip(DateTime connection)
        {
            this.DateConnection = connection;
        }

        public Vip(DateTime connection, double costMinute, double subscriberPay)
        {
            this.CostMinute = costMinute;
            this.SubscriberPay = subscriberPay;
            this.DateConnection = connection;
        }


    }
}
