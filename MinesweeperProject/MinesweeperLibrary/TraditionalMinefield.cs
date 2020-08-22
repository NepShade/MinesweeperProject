using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary
{
    // Classe rappresentante un campo minato 'tradizionale', ossia un campo minato che
    // evita di generare mine all'interno della regione iniziale scoperta dall'utente
    public class TraditionalMinefield : Minefield
    {
        // Attributi della classe
        // coordinate della prima zona da scoprire del campo minato
        private int _initialZoneX;
        private int _initialZoneY;
        // variabile che indica se le coordinate della prima zona da scoprire sono state acquisite
        private bool _initialZoneCoordinatesAcquired = false;

        // Costruttore che genera un campo minato 'tradizionale' dalle caratteristiche specificate
        public TraditionalMinefield(int length, int height, int mines) : base(length, height, mines) { }

        // Metodo che crea un nuovo campo minato dalle caratteristiche già impostate
        public override void CreateNewMinefield()
        {
            // si annota che le coordinate delle zona iniziale devono essere acquisite
            _initialZoneCoordinatesAcquired = false;
        }

        // Metodo che genera casualmente le coordinate delle mine all'esterno della regione iniziale
        protected override List<Tuple<int, int>> GenerateMinesCoordinates(int minesToGenerate)
        {
            // determinazione e memorizzazione della regione iniziale esente da mine
            List<int> initialRegion = GetInitialRegion(_initialZoneX, _initialZoneY);

            // insieme di elementi non uguali contenente la posizione delle mine
            HashSet<int> minesPosition = new HashSet<int>();
            // generatore di numeri pseudo-casuali
            Random positionsGenerator = new Random();
            // si generano posizioni tutte diverse fintanto che non si raggiunge il numero di mine da generare
            while (minesPosition.Count < minesToGenerate)
            {
                int minePosition = positionsGenerator.Next(TotalZones);
                // se la posizione generata non è inclusa nella regione iniziale allora viene aggiunta all'HashSet
                if (!initialRegion.Contains(minePosition))
                    minesPosition.Add(minePosition);
            }

            // lista contenente le coordinate delle mine
            List<Tuple<int, int>> minesCoordinates = new List<Tuple<int, int>>();
            // si converte la posizione di ciascuna mina in coordinate per poi aggiungerle nella lista
            foreach (int position in minesPosition)
                minesCoordinates.Add(ConvertToCoordinates(position));

            return minesCoordinates;
        }

        // Metodo che determina la posizione delle zone che costituiscono la regione iniziale esente da mine
        private List<int> GetInitialRegion(int x, int y)
        {
            // lista contenente la posizione della zona iniziale scoperta e delle sue zone adiacenti
            List<int> initialRegion = new List<int>();

            // si calcola la posizione della zona iniziale per poi aggiugerla alla lista
            initialRegion.Add(ConvertToPosition(x, y));

            // se esiste una zona a Nord-Ovest allora la sua posizione viene aggiunta alla lista
            if (AreCoordinatesValid(x - 1, y - 1))
                initialRegion.Add(ConvertToPosition(x - 1, y - 1));

            // se esiste una zona a Nord allora la sua posizione viene aggiunta alla lista
            if (AreCoordinatesValid(x, y - 1))
                initialRegion.Add(ConvertToPosition(x, y - 1));

            // se esiste una zona a Nord-Est allora la sua posizione viene aggiunta alla lista
            if (AreCoordinatesValid(x + 1, y - 1))
                initialRegion.Add(ConvertToPosition(x + 1, y - 1));

            // se esiste una zona a Ovest allora la sua posizione viene aggiunta alla lista
            if (AreCoordinatesValid(x - 1, y))
                initialRegion.Add(ConvertToPosition(x - 1, y));

            // se esiste una zona a Est allora la sua posizione viene aggiunta alla lista
            if (AreCoordinatesValid(x + 1, y))
                initialRegion.Add(ConvertToPosition(x + 1, y));

            // se esiste una zona a Sud-Ovest allora la sua posizione viene aggiunta alla lista
            if (AreCoordinatesValid(x - 1, y + 1))
                initialRegion.Add(ConvertToPosition(x - 1, y + 1));

            // se esiste una zona a Sud allora la sua posizione viene aggiunta alla lista
            if (AreCoordinatesValid(x, y + 1))
                initialRegion.Add(ConvertToPosition(x, y + 1));

            // se esiste una zona a Sud-Est allora la sua posizione viene aggiunta alla lista
            if (AreCoordinatesValid(x + 1, y + 1))
                initialRegion.Add(ConvertToPosition(x + 1, y + 1));

            return initialRegion;
        }

        // Metodo che prova a controllare se la zona dalle coordinate specificate è contrassegnata
        public override bool IsZoneFlagged(int x, int y, out bool? zoneFlagged)
        {
            // se le coordinate della prima zona non sono state acquisite...
            if (!_initialZoneCoordinatesAcquired)
            {
                // ...si comunica che l'operazione è fallita...
                zoneFlagged = null;
                return false;
            }
            else
            {
                // ...altrimenti si richiama la definizione del metodo stabilita nella classe base
                return base.IsZoneFlagged(x, y, out zoneFlagged);
            }
        }

        // Metodo che prova a contrassegnare o "ripulire" la zona dalle coordinate specificate
        public override bool FlagUnflagZone(int x, int y, out bool? zoneFlagged)
        {
            // se le coordinate della prima zona non sono state acquisite...
            if (!_initialZoneCoordinatesAcquired)
            {
                // ...si comunica che l'operazione è fallita...
                zoneFlagged = null;
                return false;
            }
            else
            {
                // ...altrimenti si richiama la definizione del metodo stabilita nella classe base
                return base.FlagUnflagZone(x, y, out zoneFlagged);
            }
        }

        // Metodo che prova a controllare se la zona dalle coordinate specificate è coperta
        public override bool IsZoneCovered(int x, int y, out bool? zoneCovered)
        {
            // se le coordinate della prima zona non sono state acquisite...
            if (!_initialZoneCoordinatesAcquired)
            {
                // ...si comunica che l'operazione è fallita...
                zoneCovered = null;
                return false;
            }
            else
            {
                // ...altrimenti si richiama la definizione del metodo stabilita nella classe base
                return base.IsZoneCovered(x, y, out zoneCovered);
            }
        }

        // Metodo che prova a scoprire la zona dalle coordinate specificate
        public override bool UncoverZone(int x, int y, out bool? zoneMined, out int? adjacentMines)
        {
            // se le coordinate della prima zona non sono state acquisite allora vengono acquisite
            if (!_initialZoneCoordinatesAcquired)
            {
                if (AreCoordinatesValid(x, y))
                {
                    // se le coordinate specificate sono valide allora si acquisiscono per poi
                    // annotare che le coordinate delle zona iniziale sono state acquisite
                    _initialZoneX = x;
                    _initialZoneY = y;
                    _initialZoneCoordinatesAcquired = true;
                    // generazione delle mine e configurazione delle zone presenti nel campo minato
                    base.CreateNewMinefield();
                }
                else
                {
                    // se le coordinate specificata non sono valide si comunica che l'operazione è fallita
                    zoneMined = null;
                    adjacentMines = null;
                    return false;
                }
            }

            // si richiama la definizione del metodo stabilita nella classe base
            return base.UncoverZone(x, y, out zoneMined, out adjacentMines);
        }

        // Metodo che prova a controllare se la zona scoperta dalle coordinate specificate è minata
        public override bool IsUncoveredZoneMined(int x, int y, out bool? zoneMined)
        {
            // se le coordinate della prima zona non sono state acquisite...
            if (!_initialZoneCoordinatesAcquired)
            {
                // ...si comunica che l'operazione è fallita...
                zoneMined = null;
                return false;
            }
            else
            {
                // ...altrimenti si richiama la definizione del metodo stabilita nella classe base
                return base.IsUncoveredZoneMined(x, y, out zoneMined);
            }
        }

        // Metodo che prova a controllare se la zona dalle coordinate specificate è minata
        internal override bool IsZoneMined(int x, int y, out bool? zoneMined)
        {
            // se le coordinate della prima zona non sono state acquisite...
            if (!_initialZoneCoordinatesAcquired)
            {
                // ...si comunica che l'operazione è fallita...
                zoneMined = null;
                return false;
            }
            else
            {
                // ...altrimenti si richiama la definizione del metodo stabilita nella classe base
                return base.IsZoneMined(x, y, out zoneMined);
            }
        }

        // Metodo che prova a restituire il numero di mine adiacenti alla zona scoperta dalle coordinate specificate
        public override bool GetUncoveredZoneAdjacentMines(int x, int y, out int? adjacentMines)
        {
            // se le coordinate della prima zona non sono state acquisite...
            if (!_initialZoneCoordinatesAcquired)
            {
                // ...si comunica che l'operazione è fallita...
                adjacentMines = null;
                return false;
            }
            else
            {
                // ...altrimenti si richiama la definizione del metodo stabilita nella classe base
                return base.GetUncoveredZoneAdjacentMines(x, y, out adjacentMines);
            }
        }

        // Metodo che prova a restituire il numero di mine adiacenti alla zona dalle coordinate specificate
        internal override bool GetZoneAdjacentMines(int x, int y, out int? adjacentMines)
        {
            // se le coordinate della prima zona non sono state acquisite...
            if (!_initialZoneCoordinatesAcquired)
            {
                // ...si comunica che l'operazione è fallita...
                adjacentMines = null;
                return false;
            }
            else
            {
                // ...altrimenti si richiama la definizione del metodo stabilita nella classe base
                return base.GetZoneAdjacentMines(x, y, out adjacentMines);
            }
        }

        // Metodo che restituisce le coordinate delle mine presenti nel campo minato
        public override List<Tuple<int, int>> GetMinesCoordinates()
        {
            // se le coordinate della prima zona non sono state acquisite...
            if (!_initialZoneCoordinatesAcquired)
            {
                // ...si restituisce un riferimento nullo...
                return null;
            }
            else
            {
                // ...altrimenti si richiama la definizione del metodo stabilita nella classe base
                return base.GetMinesCoordinates();
            }
        }
    }
}
