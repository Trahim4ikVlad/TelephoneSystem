using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneSystem.ATSModel;
using TelephoneSystem.BillingSystem;
using TelephoneSystem.EventArgsChildren;

namespace TelephoneSystem
{
    public class Abonent : IEquatable<Abonent>
    {
       
        public Port Port { get; set; }

        public Terminal Terminal { get; set; }

        public TariffPlan TariffPlan { get; set; }


        public Abonent(Port port, TariffPlan plan, Terminal terminal)
        {
            this.Port = port;
            this.TariffPlan = plan;
            this.Terminal = terminal;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            Terminal.BeginningCall += Terminal_BeginningCall;
            Terminal.Called += Terminal_Called;
            Terminal.FinishingCall += Terminal_FinishingCall;            
        }

        public void DisconnectFromPort()
        {
        
            this.Port.State = PortState.Disabled;
        
        }

        public void ConnectToPort()
        {
            this.Port.State = PortState.Connected;
        }

        private void Terminal_FinishingCall(object sender, EventArgs e)
        {
            Port.State = PortState.Connected;
        }

        private void Terminal_Called(object sender, EventArgsCall e)
        {
            Port.State = PortState.Call;
        }

        private void Terminal_BeginningCall(object sender, EventArgs e)
        {
            Port.State = PortState.Call;
        }

        public bool Equals(Abonent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return  Equals(Port, other.Port) && Equals(Terminal, other.Terminal) && Equals(TariffPlan, other.TariffPlan);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Port != null ? Port.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Terminal != null ? Terminal.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TariffPlan != null ? TariffPlan.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Abonent left, Abonent right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Abonent left, Abonent right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Abonent)obj);
        }

        #region event chengedTariffPlan
        public event EventHandler<EventArgs> ChangedTariffPlan;

        private void OnChangedTariffPlan()
        {
            EventHandler<EventArgs> handler = ChangedTariffPlan;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        #endregion

        public void ChangeTariffPlan(TariffPlan plan)
        {
            if ((plan.DateConnection - this.TariffPlan.DateConnection).Days > 30)
            {
                TariffPlan = plan;
                OnChangedTariffPlan();
            }
            else
            {
                throw new Exception("Unable to change the tariff plan.");
            }
        }
    }
}
