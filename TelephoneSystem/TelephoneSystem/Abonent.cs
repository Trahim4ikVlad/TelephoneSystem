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

        public int PhoneNumber { get; private set; }

        public double Balance { get; set; }

        private  IList<Call> _calls = new List<Call>(); 


        public Abonent(int phoneNumber)
        {
            this.PhoneNumber = phoneNumber;

            Terminal.BeginningCall+=Terminal_BeginningCall;
            Terminal.Called+=Terminal_Called;
            Terminal.FinichingCall+=Terminal_FinichingCall;

            this.Port = new Port();
        }

        public IEnumerable<Call> Calls()
        {
            return _calls;
        }

        private void Terminal_FinichingCall(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        private void Terminal_Called(object sender, EventArgs e)
        {
           
        }

        private void Terminal_BeginningCall(object sender, EventArgs e)
        {
            Port.State = PortState.Closed;
        }


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
