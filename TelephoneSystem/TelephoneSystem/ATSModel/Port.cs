using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneSystem.BillingSystem;

namespace TelephoneSystem.ATSModel
{
    public class Port : IEquatable<Port>
    {

        public PortState State { get; set; }

        public int PhoneNumber { get; set; }

        public double  Balance { get; set; }

        public Port(int number)
        {
            this.PhoneNumber = number;
            this.State = PortState.Connected;
            this.Balance = 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Port) obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) State*397) ^ PhoneNumber;
            }
        }

        public static bool operator ==(Port left, Port right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Port left, Port right)
        {
            return !Equals(left, right);
        }

        public bool Equals(Port other)
        {
            return State == other.State && PhoneNumber == other.PhoneNumber;
        }

   }
}
