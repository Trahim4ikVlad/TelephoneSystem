using System;
using TelephoneSystem.BillingSystem;

namespace TelephoneSystem.ATSModel
{
    public class Terminal
    {
        public TerminalState State { get; set; }

        public event EventHandler<EventArgs> BeginningCall;
        public event EventHandler<EventArgs> Called;
        public event EventHandler<EventArgs> FinichingCall;

        protected virtual void OnFinishingCall()
        {
            EventHandler<EventArgs> handler = FinichingCall;
            if (handler != null) handler(this, EventArgs.Empty);

        }


        protected virtual void OnCall()
        {
            EventHandler<EventArgs> handler = Called;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnStartCall()
        {
            EventHandler<EventArgs> handler = BeginningCall;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void StartCall()
        {
            this.State = TerminalState.OutboundSet;
            OnStartCall();
        }

        public void Call()
        {
            this.State = TerminalState.ConversationalState;
            OnCall();
        }

        public void FinishCall()
        {
            this.State = TerminalState.InitialState;
            OnFinishingCall();
        }
    }
}
