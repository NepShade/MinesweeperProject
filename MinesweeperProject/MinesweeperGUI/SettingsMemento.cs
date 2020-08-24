using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperGUI
{
    // Classe contenente lo stato parziale del form delle impostazioni
    internal class SettingsMemento : IMemento
    {
        // Attributi della classe
        // array contenente le opzioni selezionate dall'utente nel form delle impostazioni
        private readonly int[] _state;

        // Costruttore esplicito che acquisisce lo stato parziale del form delle impostazioni
        public SettingsMemento(params int[] state)
        {
            if (state == null)
                throw new ArgumentNullException
                    ("Non è possibile passare un riferimento nullo al memento!");

            if (state.Length == 0)
                throw new ArgumentException
                    ("Il memento deve memorizzare almeno un valore!");

            // si crea l'array e si memorizzano le opzioni selezionate dall'utente nel form delle impostazioni
            _state = new int[state.Length];
            for (int i = 0; i < state.Length; i++)
                _state[i] = state[i];
        }

        // Metodo che restituisce lo stato parziale memorizzato
        public int[] GetState()
        {
            // duplicato dell'array contenente lo stato parziale del form delle impostazioni
            int[] copyOfState = new int[_state.Length];

            // si duplica e si restituisce lo stato parziale memorizzato
            for (int i = 0; i < _state.Length; i++)
                copyOfState[i] = _state[i];

            return copyOfState;
        }

        // Metodo che determina se l'oggetto specificato è uguale all'oggetto corrente
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                try
                {
                    // si converte esplicitamente l'oggetto specificato nel tipo opportuno e se i due
                    // oggetti hanno il medesimo hash code allora si comunica che gli oggetti sono uguali
                    SettingsMemento memento = (SettingsMemento)obj;
                    if (GetHashCode() == memento.GetHashCode())
                        return true;
                }
                catch { }
            }
            // si comunica che gli oggetti sono diversi
            return false;
        }

        // Metodo che restituisce l'hash code dell'oggetto corrente
        public override int GetHashCode()
        {
            unchecked
            {
                // si calcola l'hash code sfruttando i valori memorizzati nell'oggetto
                int hashCode = 11;
                for (int i = 0; i < _state.Length; i++)
                    hashCode = hashCode * 101 + _state[i].GetHashCode();

                return hashCode;
            }
        }
    }
}
