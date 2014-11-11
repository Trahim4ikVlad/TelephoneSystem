using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneSystem.ATSModel;

namespace TelephoneSystem.BillingSystem
{
    public static class BillingSystem
    {
        public static double CostCall(this Call call, TariffPlan plan)
        {
            if(call.IsOutgoig)
                return call.Duration * plan.OutCallCostMinute;

            return call.Duration * plan.InCallCostMinute;
        }

        public static IList<Call> FilterCallBy(this Abonent abonent, DateTime date)
        {
            return abonent.Calls().Where(x => x.DateTimeCall == date).ToList();
        }


        public static IList<Call> FilterCallBy(this Abonent abonent, Abonent subscriber)
        {
            return abonent.Calls().Where(x => x.Port == subscriber.Port).ToList();
        }

       /* public static IList<Call> FilterCallByCost(this Abonent abonent)
        {
          abonent.Port.Calls().OrderBy()
        }*/
    }
}
