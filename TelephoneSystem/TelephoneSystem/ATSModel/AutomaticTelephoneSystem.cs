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

        private IList<Abonent> PublicAbonents()
        {
            return _abonents.Where(x => x.Port.State == PortState.Connected).ToList();
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
            item.FinishingCall += Finish;
        }

        private void Finish(object sender, EventArgsCall e)
        {
          //добавить Callinfo  в список
        }


        private void Call(object sender, EventArgsCall e)
        {
            //найти абонента по номеру и изменить состояние его порта и терминала
        }
        
        
        ////////////////////////////////////
        private void Connection(object sender, EventArgsCall e)
        {
            // проверить доступен ли абонент которому звонит и порт, иначе сгенерить исключение
        }

        public void ProvideReportToSubscriber(object sender, EventArgs e)
        {
            var abonent = sender as Abonent;

            if (abonent != null)
            {

                var infos =
                    from info in _callsInfo
                    where
                        info.IngoingNumber == abonent.Port.PhoneNumber ||
                        info.OutgoingNumber == abonent.Port.PhoneNumber
                    select new
                    {
                        info.Duration,
                        info.TimeStartCall
                    };


                var callinfos =
                    _callsInfo.Where(
                        x => (x.IngoingNumber == abonent.Port.PhoneNumber || x.OutgoingNumber == abonent.Port.PhoneNumber) 
                        );
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
    }
}
