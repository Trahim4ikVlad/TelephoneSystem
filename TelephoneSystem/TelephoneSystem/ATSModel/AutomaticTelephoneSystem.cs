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

        //отчет по абоненту
        public IList<CallInfo> FilterBy(Abonent abonent)
        {

         return   _callsInfo.Where(
                x => x.IngoingNumber == abonent.Port.PhoneNumber || x.OutgoingNumber == abonent.Port.PhoneNumber)
                .ToList();
        }

        private IList<Abonent> PublicAbonents()
        {
            return _abonents.Where(x => x.Port.State == PortState.Connected).ToList();
        }


        public void AddAbonent(Abonent item)
        {
            if(!_abonents.Contains(item))
            _abonents.Add(item);
            item.Terminal.FinishingCall+=Terminal_FinishingCall;

        }

        private void Terminal_FinishingCall(object sender, EventArgsFinishCall e)
        {
            AddCallInfo(e.CallInfo);
        }

        public void AddCallInfo(CallInfo info)
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
