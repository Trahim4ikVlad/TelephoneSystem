using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TelephoneSystem.ATSModel
{
    public class AutomaticTelephoneSystem:ICollection<Abonent>
    {
        private IList<Abonent> _abonents = new List<Abonent>();

        private IList<Abonent> PublicAbonents()
        {
            return _abonents.Where(x => (x.Port.State == PortState.Open) && x.Terminal.State == TerminalState.InitialState).ToList();
        }


        private void ConnectionWith(int phoneNumber)
        {
            Abonent abonent = new Abonent(phoneNumber);

            if (PublicAbonents().Contains(abonent))
            {
                abonent.Port.State = PortState.Closed;
            }
        }

        public AutomaticTelephoneSystem()
        {
            
        }



        # region implemention ICollection<Abonent>
        public void Add(Abonent item)
        {
            _abonents.Add(item);
        }

        public void Clear()
        {
            _abonents.Clear();
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

        public void CopyTo(Abonent[] array, int arrayIndex)
        {
            for (int i = 0; i < _abonents.Count; i++)
            {
                array[i] = (Abonent)_abonents[i];
            }
        }

        public int Count
        {
            get { return _abonents.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Abonent item)
        {
            bool result = false;

            for (int i = 0; i < _abonents.Count; i++)
            {
                Abonent cur = (Abonent)_abonents[i];
                if (cur.Equals(item))
                {
                    _abonents.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }

        public IEnumerator<Abonent> GetEnumerator()
        {
            return _abonents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        # endregion


    }
}
