using System;
using TelephoneSystem.BillingSystem;
using TelephoneSystem.EventArgsChildren;

namespace TelephoneSystem.ATSModel
{
    public class Terminal
    {

        public event EventHandler<EventArgsCall> BeginningCall;
        public event EventHandler<EventArgsCall> Called;
        public event EventHandler<EventArgsFinishCall> FinishingCall;

        protected virtual void OnFinishingCall()
        {
            EventHandler<EventArgsFinishCall> handler = FinishingCall;
            if (handler != null) handler(this, new EventArgsFinishCall());
        }

        protected virtual void OnCall()
        {
            EventHandler<EventArgsCall> handler = Called;
            if (handler != null) handler(this, new EventArgsCall());
        }

        protected virtual void OnStartCall()
        {
            EventHandler<EventArgsCall> handler = BeginningCall;

            if (handler != null) handler(this, new EventArgsCall());
        }

        public void StartCall()
        {
            OnStartCall();
        }

        public void Call()
        {

            OnCall();
        }

        public void FinishCall()
        {
            OnFinishingCall();
        }
    }
}
