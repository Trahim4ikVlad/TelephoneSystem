using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneSystem.BillingSystem;

namespace TelephoneSystem.ATSModel
{
    public class Port : INotifyPropertyChanged
    {
        
        public PortState State { get; set; }

        public string PhoneNumber { get; set; }

        private IList<Call> _calls = new List<Call>();


        public void AddCall(Call call)
        {
            _calls.Add(call);
        }

        //событие пополнение баланса
        //событие блокировка порта, если не оплачено по нужное число

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        
       
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new
                PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
