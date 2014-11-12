using System;
using TelephoneSystem.BillingSystem;
using TelephoneSystem.EventArgsChildren;

namespace TelephoneSystem.ATSModel
{
    public class Terminal
    {
        public TerminalState State { get; set; }

        public Terminal()
        {
            this.State = TerminalState.InitialState;
        }
    }
}
