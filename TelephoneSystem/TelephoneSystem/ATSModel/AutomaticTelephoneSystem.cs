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
            item.FiltringCallByAbonent+=Abonent_FiltringCallByAbonent;
            item.FiltringCallByDate+=Abonent_FiltringCallByDate;
        }

        private void Abonent_FiltringCallByDate(object sender, EventArgsFilterDate e)
        {
            var abonent = sender as Abonent;

            if (abonent != null)
            {
                Console.WriteLine("Filtering by date " + e.Date);
                var callsinfo =
                    _callsInfo.Where(x => x.TimeStartCall == e.Date).ToList();
                foreach (var info in callsinfo)
                {
                    Console.WriteLine("Date call: " + info.TimeStartCall.ToShortDateString() + " Cost call: " + info.CostCall +
                        " Duration: " + info.Duration + "Abonent: " + info.IngoingNumber
                        );
                }
            }
        }

        private void Abonent_FiltringCallByAbonent(object sender, EventArgsFilterAbonent e)
        {
            var abonent = sender as Abonent;

            if (abonent != null)
            {
                Console.WriteLine("Filtering subscriber " + e.Number);
                var callsinfo =
                    _callsInfo.Where(x => x.IngoingNumber == e.Number || x.OutgoingNumber == e.Number).ToList();
                foreach (var info in callsinfo)
                {
                    Console.WriteLine("Date call: " + info.TimeStartCall.ToShortDateString() + " Cost call: " + info.CostCall +
                        " Duration: "+ info.Duration
                        );
                }
            }
        }

        private void RegistrationCall(object sender, EventArgsCall e)
        {
            var abonent = sender as Abonent;
            CallInfo call;

            var outAbonent = _abonents.First(x => x.Port.PhoneNumber == e.OutPhoneNumber);
            var inAbonent = _abonents.First(x => x.Port.PhoneNumber == e.InPhoneNumber);

            if (outAbonent != null && inAbonent!=null)
            {

               call = new CallInfo(e.StartCall, e.EndCall, e.InPhoneNumber, e.OutPhoneNumber);
               call.CostCall = call.Duration * outAbonent.TariffPlan.OutCallCostMinute; 
              
                this.AddCallInfo(call);

                Console.WriteLine("АТС:  Звонок успешно зарегистрирован");

                outAbonent.Port.State = PortState.Connected;
                outAbonent.Terminal.State = TerminalState.InitialState;

                inAbonent.Terminal.State = TerminalState.CallState;
                inAbonent.Port.State = PortState.Connected;
            }
        }

        private void Call(object sender, EventArgsCall e)
        {
            Abonent outAbonent = sender as Abonent;
            
            if (outAbonent != null)
            {
                var inAbonent = _abonents.First(x => x.Port.PhoneNumber == e.InPhoneNumber) as Abonent;

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

                Abonent inAbonent = PublicAbonents().First(x => x.Port.PhoneNumber == e.InPhoneNumber);

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

            foreach (CallInfo callInfo in callInfos)
            {
                Console.WriteLine("CostCall:" + callInfo.CostCall + " Ingoing :" + callInfo.IngoingNumber 
                    +" Outgoing:" + callInfo.OutgoingNumber);
            }

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

        public void WithdrawMoney()
        {
            foreach (Abonent abonent in _abonents)
            {
                var cost = _callsInfo.Where(x => x.OutgoingNumber == abonent.Port.PhoneNumber).Sum(x=>x.CostCall);      
                abonent.Port.Balance = abonent.Port.Balance - cost;

                if(abonent.Port.Balance <= 0)
                    abonent.Port.State = PortState.BLocked;
            }
        }
    }
}
