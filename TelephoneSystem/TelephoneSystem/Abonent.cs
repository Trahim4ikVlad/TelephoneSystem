using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneSystem.ATSModel;
using TelephoneSystem.BillingSystem;

namespace TelephoneSystem
{
    public class Abonent
    {

        public Port Port { get; set; }
     
        public  Terminal Terminal { get; set; }

        public TariffPlan CurrentTariffPlan { get; set; }

        public double CurretntBalans { get; set; }



        #region event chengedTariffPlan
        public event EventHandler<EventArgs> ChangedTariffPlan;


        protected virtual void OnChangedTariffPlan()
        {
            EventHandler<EventArgs> handler = ChangedTariffPlan;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        #endregion

        public void ChangeTariffPlan(TariffPlan plan)
        {
            if ((plan.DateConnection - this.CurrentTariffPlan.DateConnection).Days > 30)
            {
                CurrentTariffPlan = plan;
                OnChangedTariffPlan();
            }
            else
            {
                throw new Exception("Unable to change the tariff plan.");
            }
        }

        
    }
}
