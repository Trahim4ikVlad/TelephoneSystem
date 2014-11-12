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


        public event EventHandler<EventArgsCall> BeginningCall;
        public event EventHandler<EventArgsCall> Called;
        public event EventHandler<EventArgsCall> FinishingCall;


        private  EventArgsCall argsCall = new EventArgsCall();

        protected virtual void OnFinishingCall()
        {
            EventHandler<EventArgsCall> handler = FinishingCall;
            argsCall.EndCall = DateTime.Now;
            if (handler != null) handler(this, argsCall);
        }

        protected virtual void OnCall()
        {
            EventHandler<EventArgsCall> handler = Called;
            argsCall.StartCall = DateTime.Now;

            if (handler != null) handler(this, argsCall);
        }

        protected virtual void OnStartCall(int phoneNumber)
        {
            EventHandler<EventArgsCall> handler = BeginningCall;
            argsCall.PhoneNumber = phoneNumber;
            if (handler != null) handler(this, argsCall);
        }

        public void StartCall(int numberPhone)
        {
            this.Terminal.State = TerminalState.CallState;
            OnStartCall(numberPhone);
            
        }

        public void Call()
        {
            OnCall();
        }

        public void FinishCall()
        {
            OnFinishingCall();
        }

        public event EventHandler<EventArgsWiewReport> BrowsingCallsInfo;

        protected virtual void OnViewReportCallInfo(int numberMonth)
        {
            EventHandler<EventArgsWiewReport> handler = BrowsingCallsInfo;
            if (handler != null) handler(this, new EventArgsWiewReport(numberMonth));
        }

        public void ViewReportCallForMoth(int numberMonth)
        {
            if (numberMonth <= 12 && numberMonth >= 1)
            {
                OnViewReportCallInfo(numberMonth);
            }
            else
            {
                throw new Exception("Month with this number does not exist!!");
            }
        }

        public void ChangeTariffPlan(TariffPlan plan)
        {
            if ((plan.DateConnection - this.TariffPlan.DateConnection).Days > 30)
            {
                TariffPlan = plan;
            }
            else
            {
                throw new Exception("Unable to change the tariff plan.");
            }
        }

        public Abonent(Port port, TariffPlan plan, Terminal terminal)
        {
            this.Port = port;
            this.TariffPlan = plan;
            this.Terminal = terminal;
        }

        public void DisconnectFromPort()
        {
            this.Port.State = PortState.Disabled;
        }

        public void ConnectToPort()
        {
            this.Port.State = PortState.Connected;
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
    }
}
