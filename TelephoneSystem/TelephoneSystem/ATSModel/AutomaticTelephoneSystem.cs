using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TelephoneSystem.EventArgsChildren;

namespace TelephoneSystem.ATSModel
{
    public class AutomaticTelephoneSystem
    {

        private IList<Abonent> _abonents = new List<Abonent>();
        private  IList<CallInfo> _callsInfo = new List<CallInfo>();

        public IList<CallInfo> CallInfos()
        {
            return this._callsInfo;
        }

        private IList<Abonent> PublicAbonents()
        {
            return _abonents.Where(x => x.Port.State == PortState.Connected && x.Terminal.State == TerminalState.InitialState).ToList();
        }

        public void AddAbonent(Abonent item)
        {
            if(!_abonents.Contains(item))
            _abonents.Add(item);
            SubscriberEvents(item);
        }

        private void SubscriberEvents(Abonent item)
        {
            item.BeginningCall += Connection;
            item.Called += Call;
            item.FinishingCall += RegistrationCall;
            item.BrowsingCallsInfo += ProvideReportToSubscriber;
        }

        private void RegistrationCall(object sender, EventArgsCall e)
        {
            var abonent = sender as Abonent;

            if (abonent != null)
            {
                CallInfo call = new CallInfo(e.StartCall, e.EndCall, abonent.Port.PhoneNumber, e.PhoneNumber);
                call.CostCall = call.Duration*abonent.TariffPlan.OutCallCostMinute;
                
                this.AddCallInfo(call);
                Console.WriteLine("АТС:  Звонок успешно зарегистрирован");
                abonent.Port.State = PortState.Connected;
                abonent.Terminal.State = TerminalState.InitialState;
            }

            var inAbonent = _abonents.First(x => x.Port.PhoneNumber == e.PhoneNumber);
            if (inAbonent != null)
            {
                inAbonent.Terminal.State = TerminalState.CallState;
                inAbonent.Port.State = PortState.Connected;
            }
        }

        private void Call(object sender, EventArgsCall e)
        {
            Abonent outAbonent = sender as Abonent;
            
            if (outAbonent != null)
            {
                var inAbonent = _abonents.First(x => x.Port.PhoneNumber == e.PhoneNumber) as Abonent;

                if (inAbonent != null)
                {
                        inAbonent.Terminal.State = TerminalState.CallState;
                        Console.WriteLine("АТС:  Звонок.......");  
                }
            }
        }
        
        private void Connection(object sender, EventArgsCall e)
        {
            Abonent outAbonent = sender as Abonent;

            if(outAbonent != null)
            {
                outAbonent.Port.State = PortState.Call;

                Abonent inAbonent = PublicAbonents().First(x => x.Port.PhoneNumber == e.PhoneNumber);

                if (inAbonent != null)
                {
                    if (inAbonent.Port.State == PortState.Connected &&
                        inAbonent.Terminal.State == TerminalState.InitialState)
                    {
                        inAbonent.Port.State = PortState.Call;
                    }
                    else
                    {
                        throw new Exception("The subscriber is temporarily unavailable.");
                    }
                }
                Console.WriteLine("АТС:  Звонок начался.......");
            }
        }

        private void ProvideReportToSubscriber(object sender, EventArgsWiewReport e)
        {
            var abonent = sender as Abonent;
            IList<CallInfo> callInfos = new List<CallInfo>();
            if (abonent != null)
            {
                callInfos = _callsInfo.Where(
                    x => (x.IngoingNumber == abonent.Port.PhoneNumber 
                          || x.OutgoingNumber == abonent.Port.PhoneNumber)
                         &&x.TimeStartCall.Month == e.NumberMonth
                    ).ToList();
            }
          //как быть в такой ситуации?
        }

        private void AddCallInfo(CallInfo info)
        {
            _callsInfo.Add(info);
        }

        public bool Contains(Abonent item)
        {
            bool found = false;

            foreach (var sentence in _abonents)
            {
                if (sentence.Equals(item))
                {
                    found = true;
                }
            }

            return found;
        }
    }
}
