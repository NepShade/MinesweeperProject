using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary
{
    // Classe di supporto per l'utilizzo della libreria
    public class MinesweeperHelper
    {
        // Attributi della classe
        // lista che associa a ciascun campo minato un nome univoco, una descrizione e un creatore
        private readonly List<Tuple<string, string, Type>> _minefieldsTable;

        // Costruttore che associa a ciascun campo minato un nome univoco, una descrizione e un creatore
        public MinesweeperHelper()
        {
            // si acquisiscono tutti i tipi contenuti nella libreria
            Type[] libraryTypes = GetType().Assembly.GetTypes();

            // creazione e inizializzazione della lista dei tipi di creatore di campi minati
            List<Type> minefieldCreatorTypes = new List<Type>();
            foreach (Type libraryType in libraryTypes)
            {
                // se il tipo analizzato è un creatore di campi minati allora viene aggiunto alla lista
                if (typeof(IMinefieldCreator).IsAssignableFrom(libraryType))
                    minefieldCreatorTypes.Add(libraryType);
            }
            // si rimuove dalla lista il tipo rappresentante l'interfaccia di un creatore di campi minati
            minefieldCreatorTypes.Remove(typeof(IMinefieldCreator));

            // insieme di elementi non uguali contenente i nomi dei campi minati
            HashSet<string> minefieldNamesHashSet = new HashSet<string>();

            // creazione e inizializzazione della "tabella" delle informazioni dei campi minati
            _minefieldsTable = new List<Tuple<string, string, Type>>();
            foreach (Type creatorType in minefieldCreatorTypes)
            {
                // si crea un'istanza di un creatore di campi minati
                IMinefieldCreator creator = (IMinefieldCreator)Activator.CreateInstance(creatorType);
                // si aggiunge il nome del campo minato a cui il creatore fa riferimento nella struttura HashSet
                minefieldNamesHashSet.Add(creator.GetMinefieldName().Trim());
                // si aggiunge alla "tabella" delle informazioni dei campi minati una nuova entry
                _minefieldsTable.Add(new Tuple<string, string, Type>(creator.GetMinefieldName().Trim(),
                                                                     creator.GetMinefieldInfo().Trim(),
                                                                     creatorType));
            }

            if (minefieldNamesHashSet.Count < minefieldCreatorTypes.Count)
                throw new Exception
                    ("Ciascun creatore di campo minato deve restituire un nome univoco di campo minato!");
        }

        // Metodo che restituisce i nomi univoci associati alle diverse tipologie di campo minato
        public string[] GetMinefieldsNames()
        {
            // array contenente i nomi univoci associati ai vari tipi di campo minato
            string[] minefieldsNames = new string[_minefieldsTable.Count];

            // si copia ciascun nome univoco di campo minato all'interno dell'array
            for (int i = 0; i < minefieldsNames.Length; i++)
                minefieldsNames[i] = _minefieldsTable[i].Item1;

            return minefieldsNames;
        }

        // Metodo che restituisce la descrizione associata al campo minato dal nome specificato
        public string GetMinefieldInfo(string minefieldName)
        {
            // descrizione del campo minato da restituire
            string minefieldDescription = null;

            // si cerca la descrizione corrispondente al nome specificato di campo minato
            foreach (Tuple<string, string, Type> entry in _minefieldsTable)
                if (entry.Item1 == minefieldName)
                {
                    minefieldDescription = entry.Item2;
                    break;
                }

            return minefieldDescription;
        }

        // Metodo che restituisce il creatore associato al campo minato dal nome specificato
        public IMinefieldCreator GetMinefieldCreator(string minefieldName)
        {
            // creatore di campo minato da restituire
            IMinefieldCreator minefieldCreator = null;

            // si cerca il creatore corrispondente al nome specificato di campo minato
            foreach (Tuple<string, string, Type> entry in _minefieldsTable)
                if (entry.Item1 == minefieldName)
                {
                    minefieldCreator = (IMinefieldCreator)Activator.CreateInstance(entry.Item3);
                    break;
                }

            return minefieldCreator;
        }
    }
}
