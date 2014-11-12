using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneSystem.ATSModel;
using TelephoneSystem.BillingSystem;

namespace TelephoneSystem
{
    class Program
    {
        static void Main(string[] args)
        {

          AutomaticTelephoneSystem system = new AutomaticTelephoneSystem();

          Abonent a = new Abonent(new Port(2000), new Business(DateTime.UtcNow, 200), new Terminal());
          Abonent b = new Abonent(new Port(3000), new Business(DateTime.UtcNow, 200), new Terminal());
          Abonent c = new Abonent(new Port(4000), new Business(DateTime.UtcNow, 200), new Terminal());
          
            system.AddAbonent(a);
            system.AddAbonent(b);
            system.AddAbonent(c);

            a.StartCall(3000);
            a.Call();
            a.FinishCall();

            c.StartCall(2000);
            c.Call();
            c.FinishCall();
     
        }

       
    }
}
