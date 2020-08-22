using System;
using System.Collections.Generic;

namespace MinesweeperLibrary
{
    // Classe astratta rappresentante un campo minato
    public abstract class Minefield
    {
        // Attributi della classe
        // dimensione minima e massima di un lato di un campo minato
        public const int MinSide = 8;
        public const int MaxSide = 30;
        // numero minimo di mine presenti in un campo minato
        public const int MinMines = 10;
        // percentuale massima di copertura del campo minato da mine
        private const double MaxMinesCoverage = 0.8;
        // lista contenente le coordinate delle mine
        private readonly List<Tuple<int, int>> _minesCoordinates;
        // matrice di zone rappresentante il campo minato
        private readonly MinefieldZone[,] _minefield;
        // lunghezza (numero di colonne) del campo minato
        private int _length;
        // altezza (numero di righe) del campo minato
        private int _height;
        // numero di mine presenti nel campo minato
        private int _mines;

        // Costruttore che genera un campo minato dalle caratteristiche specificate
        public Minefield(int length, int height, int mines)
        {
            // controllo e acquisizione delle caratteristiche del campo minato
            CheckParameters(length, height, mines);
            _length = length;
            _height = height;
            _mines = mines;

            // creazione di un campo minato dalle dimensioni massime
            _minefield = new MinefieldZone[MaxSide, MaxSide];
            // inizializzazione del campo minato con zone sicure e senza mine attorno ad esse
            for (int y = 0; y < MaxSide; y++)
                for (int x = 0; x < MaxSide; x++)
                    _minefield[x, y] = new MinefieldZone();

            // creazione della lista contenente le coordinate delle mine
            _minesCoordinates = new List<Tuple<int, int>>();
        }

        // Metodo che controlla la correttezza dei parametri specificati per il campo minato
        private void CheckParameters(int length, int height, int mines)
        {
            if (length < MinSide || height < MinSide || length > MaxSide || height > MaxSide)
                throw new ArgumentOutOfRangeException
                    ("La lunghezza e l'altezza del campo minato devono essere comprese tra " +
                     MinSide + " e " + MaxSide + "!");

            if (mines < MinMines || mines > GetMaxMines(length, height))
                throw new ArgumentOutOfRangeException
                    ("Il numero di mine del campo minato deve essere compreso tra " +
                     MinMines + " e " + GetMaxMines(length, height) + "(*)!" +
                     "\n(*) Rispetto a lunghezza e altezza specificate");
        }

        // Metodo che crea un nuovo campo minato dalle caratteristiche specificate
        public void CreateNewMinefield(int length, int height, int mines)
        {
            // controllo e acquisizione delle caratteristiche del campo minato
            CheckParameters(length, height, mines);
            _length = length;
            _height = height;
            _mines = mines;
            // si svuota la lista delle coordinate delle mine
            _minesCoordinates.Clear();
            // creazione di un nuovo campo minato
            CreateNewMinefield();
        }

        // Metodo che crea un nuovo campo minato dalle caratteristiche già impostate
        public virtual void CreateNewMinefield()
        {
            // si svuota la lista delle coordinate delle mine
            _minesCoordinates.Clear();
            // fase 1: generazione e acquisizione delle coordinate delle mine
            List<Tuple<int, int>> minesCoordinates = GenerateMinesCoordinates(_mines);
            foreach (Tuple<int, int> coordinates in minesCoordinates)
                _minesCoordinates.Add(new Tuple<int, int>(coordinates.Item1, coordinates.Item2));

            // fase 2: controllo di correttezza sulle coordinate delle mine
            CheckMinesCoordinates(_minesCoordinates);
            // fase 3: ordinamento crescente delle coordinate delle mine
            SortMinesCoordinates(_minesCoordinates);
            // fase 4: configurazione delle zone del campo minato
            ConfigureZones();
        }

        // Metodo che restituisce una lista di coordinate di mine
        protected abstract List<Tuple<int, int>> GenerateMinesCoordinates(int minesToGenerate);

        // Metodo che controlla la correttezza delle coordinate delle mine
        private void CheckMinesCoordinates(List<Tuple<int, int>> minesCoordinates)
        {
            if (minesCoordinates.Count != _mines)
                throw new ArgumentException
                    ("Generata una quantità errata di mine!");

            // insieme di elementi non uguali contenente la posizione delle mine
            HashSet<int> minesPosition = new HashSet<int>();
            // si calcola la posizione di ciascuna mina e la si aggiunge all'hashset
            // che eliminerà automaticamente eventuali valori duplicati al suo interno
            foreach (Tuple<int, int> coordinates in minesCoordinates)
                minesPosition.Add(ConvertToPosition(coordinates.Item1, coordinates.Item2));

            if (minesPosition.Count < _mines)
                throw new ArgumentException
                    ("Generate delle coordinate duplicate di mine!");
        }

        // Metodo che ordina le coordinate delle mine in ordine crescente
        private void SortMinesCoordinates(List<Tuple<int, int>> minesCoordinates)
        {
            // lista contenente la posizione delle mine all'interno del campo minato
            List<int> minesPosition = new List<int>();

            // si calcola la posizione di ciascuna mina e la si aggiunge alla lista
            foreach (Tuple<int, int> coordinates in minesCoordinates)
                minesPosition.Add(ConvertToPosition(coordinates.Item1, coordinates.Item2));

            // le posizioni vengono ordinate in ordine crescente
            minesPosition.Sort();

            // si svuota la lista non ordinata di coordinate
            minesCoordinates.Clear();
            // si ricalcolano le coordinate di ciascuna mina e le si aggiungono alla lista di coordinate
            foreach (int position in minesPosition)
                minesCoordinates.Add(ConvertToCoordinates(position));
        }

        // Metodo che configura le zone presenti all'interno del campo minato
        private void ConfigureZones()
        {
            // numero di mine create nel campo minato
            int createdMines = 0;
            // si memorizzano le coordinate della prima mina
            int mineX = _minesCoordinates[createdMines].Item1;
            int mineY = _minesCoordinates[createdMines].Item2;

            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Length; x++)
                {
                    // si calcola il numero di mine adiacenti alla zona presa in esame
                    int adjacentMines = CountAdjacentMines(x, y);

                    // se ci sono ancora mine da creare e le coordinate della zona
                    // esaminata combaciano con quelle della mina presa in considerazione...
                    if (createdMines < Mines && x == mineX && y == mineY)
                    {
                        // ...allora viene impostata una zona minata...
                        _minefield[x, y].SetZone(adjacentMines, true);

                        // si aumenta il numero di mine create e se ci sono ancora mine da
                        // creare allora si memorizzano le coordinate della mina successiva
                        if (++createdMines < Mines)
                        {
                            mineX = _minesCoordinates[createdMines].Item1;
                            mineY = _minesCoordinates[createdMines].Item2;
                        }
                    }
                    else
                        // ...altrimenti viene impostata una zona sicura
                        _minefield[x, y].SetZone(adjacentMines, false);
                }
        }

        // Metodo che rileva e conteggia le mine attorno a una specifica zona
        private int CountAdjacentMines(int x, int y)
        {
            // numero di mine rilevate attorno alla zona dalle coordinate specificate
            int adjacentMines = 0;

            // se esiste una zona a Nord-Ovest ed essa è minata allora si aumenta il numero di mine rilevate
            if (AreCoordinatesValid(x - 1, y - 1) && !AreCoordinatesSafe(x - 1, y - 1))
                adjacentMines++;

            // se esiste una zona a Nord ed essa è minata allora si aumenta il numero di mine rilevate
            if (AreCoordinatesValid(x, y - 1) && !AreCoordinatesSafe(x, y - 1))
                adjacentMines++;

            // se esiste una zona a Nord-Est ed essa è minata allora si aumenta il numero di mine rilevate
            if (AreCoordinatesValid(x + 1, y - 1) && !AreCoordinatesSafe(x + 1, y - 1))
                adjacentMines++;

            // se esiste una zona a Ovest ed essa è minata allora si aumenta il numero di mine rilevate
            if (AreCoordinatesValid(x - 1, y) && !AreCoordinatesSafe(x - 1, y))
                adjacentMines++;

            // se esiste una zona a Est ed essa è minata allora si aumenta il numero di mine rilevate
            if (AreCoordinatesValid(x + 1, y) && !AreCoordinatesSafe(x + 1, y))
                adjacentMines++;

            // se esiste una zona a Sud-Ovest ed essa è minata allora si aumenta il numero di mine rilevate
            if (AreCoordinatesValid(x - 1, y + 1) && !AreCoordinatesSafe(x - 1, y + 1))
                adjacentMines++;

            // se esiste una zona a Sud ed essa è minata allora si aumenta il numero di mine rilevate
            if (AreCoordinatesValid(x, y + 1) && !AreCoordinatesSafe(x, y + 1))
                adjacentMines++;

            // se esiste una zona a Sud-Est ed essa è minata allora si aumenta il numero di mine rilevate
            if (AreCoordinatesValid(x + 1, y + 1) && !AreCoordinatesSafe(x + 1, y + 1))
                adjacentMines++;

            return adjacentMines;
        }

        // Metodo che indica se le coordinate specificate fanno riferimento a una zona sicura
        private bool AreCoordinatesSafe(int x, int y)
        {
            // variabile che indica se la zona dalle coordinate specificate è sicura
            bool safeZone = true;

            // finché le coordinate specificate fanno riferimento a una zona sicura e tutte le mine non sono
            // state analizzate si confrontano le coordinate specificate con le coordinate delle mine
            for (int i = 0; safeZone && i < Mines; i++)
            {
                // se le coordinate specificate combaciano con quelle della mina presa in considerazione
                // allora si imposta che la zona dalle coordinate specificate non è sicura
                if (x == _minesCoordinates[i].Item1 && y == _minesCoordinates[i].Item2)
                    safeZone = false;
            }

            return safeZone;
        }

        // Metodo che indica se le coordinate specificate sono valide rispetto alle dimensioni del campo minato
        public bool AreCoordinatesValid(int x, int y)
        {
            return x >= 0 && x < Length && y >= 0 && y < Height;
        }

        // Metodo che calcola l'indice di posizione di una zona a partire dalle sue coordinate
        protected int ConvertToPosition(int x, int y)
        {
            // l'indice di posizione di una zona è determinato scorrendo prima le colonne e poi le righe
            // del campo minato a partire dalla zona più a Nord-Ovest del campo minato
            return Length * y + x;
        }

        // Metodo che calcola le coordinate di una zona a partire dal suo indice di posizione
        protected Tuple<int, int> ConvertToCoordinates(int position)
        {
            int x = position % Length;
            int y = position / Length;
            return new Tuple<int, int>(x, y);
        }

        // Metodo che prova a controllare se la zona dalle coordinate specificate è contrassegnata
        public virtual bool IsZoneFlagged(int x, int y, out bool? zoneFlagged)
        {
            // se le coordinate sono valide...
            if (AreCoordinatesValid(x, y))
            {
                // ...allora si controlla se la zona è contrassegnata come minata...
                zoneFlagged = _minefield[x, y].Flagged;
                return true;
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                zoneFlagged = null;
                return false;
            }
        }

        // Metodo che prova a contrassegnare o "ripulire" la zona dalle coordinate specificate
        public virtual bool FlagUnflagZone(int x, int y, out bool? zoneFlagged)
        {
            // se le coordinate sono valide e la zona è coperta...
            if (AreCoordinatesValid(x, y) && _minefield[x, y].Covered)
            {
                // ...allora si contrassegna o "ripulisce" la zona...
                zoneFlagged = _minefield[x, y].FlagUnflag();
                return true;
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                zoneFlagged = null;
                return false;
            }
        }

        // Metodo che prova a controllare se la zona dalle coordinate specificate è coperta
        public virtual bool IsZoneCovered(int x, int y, out bool? zoneCovered)
        {
            // se le coordinate sono valide...
            if (AreCoordinatesValid(x, y))
            {
                // ...allora si controlla se la zona è coperta...
                zoneCovered = _minefield[x, y].Covered;
                return true;
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                zoneCovered = null;
                return false;
            }
        }

        // Metodo che prova a scoprire la zona dalle coordinate specificate
        public virtual bool UncoverZone(int x, int y, out bool? zoneMined, out int? adjacentMines)
        {
            // se le coordinate sono valide, la zona è coperta e non è contrassegnata...
            if (AreCoordinatesValid(x, y) && _minefield[x, y].Covered && !_minefield[x, y].Flagged)
            {
                // ...allora si scopre la zona...
                zoneMined = _minefield[x, y].Uncover(out int mines);
                adjacentMines = mines;
                return true;
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                zoneMined = null;
                adjacentMines = null;
                return false;
            }
        }

        // Metodo che prova a controllare se la zona scoperta dalle coordinate specificate è minata
        public virtual bool IsUncoveredZoneMined(int x, int y, out bool? zoneMined)
        {
            // se le coordinate sono valide e la zona è scoperta...
            if (AreCoordinatesValid(x, y) && !_minefield[x, y].Covered)
            {
                // ...allora si controlla se la zona è minata...
                zoneMined = _minefield[x, y].IsMined();
                return true;
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                zoneMined = null;
                return false;
            }
        }

        // Metodo che prova a controllare se la zona dalle coordinate specificate è minata
        internal virtual bool IsZoneMined(int x, int y, out bool? zoneMined)
        {
            // se le coordinate sono valide...
            if (AreCoordinatesValid(x, y))
            {
                // ...allora si controlla se la zona è minata...
                zoneMined = _minefield[x, y].Mined;
                return true;
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                zoneMined = null;
                return false;
            }
        }

        // Metodo che prova a restituire il numero di mine adiacenti alla zona scoperta dalle coordinate specificate
        public virtual bool GetUncoveredZoneAdjacentMines(int x, int y, out int? adjacentMines)
        {
            // se le coordinate sono valide e la zona è scoperta...
            if (AreCoordinatesValid(x, y) && !_minefield[x, y].Covered)
            {
                // ...allora si restituisce il numero di mine adiacenti alla zona...
                adjacentMines = _minefield[x, y].GetAdjacentMines();
                return true;
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                adjacentMines = null;
                return false;
            }
        }

        // Metodo che prova a restituire il numero di mine adiacenti alla zona dalle coordinate specificate
        internal virtual bool GetZoneAdjacentMines(int x, int y, out int? adjacentMines)
        {
            // se le coordinate sono valide...
            if (AreCoordinatesValid(x, y))
            {
                // ...allora si restituisce il numero di mine adiacenti alla zona...
                adjacentMines = _minefield[x, y].AdjacentMines;
                return true;
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                adjacentMines = null;
                return false;
            }
        }

        // Metodo che restituisce le coordinate delle mine presenti nel campo minato
        public virtual List<Tuple<int, int>> GetMinesCoordinates()
        {
            // duplicato della lista contenente le coordinate delle mine
            List<Tuple<int, int>> copyOfMinesCoordinates = null;

            // se la lista contiene delle coordinate allora ne viene creato un duplicato da restituire
            if (_minesCoordinates.Count > 0)
            {
                copyOfMinesCoordinates = new List<Tuple<int, int>>();
                foreach (Tuple<int, int> coordinates in _minesCoordinates)
                    copyOfMinesCoordinates.Add(new Tuple<int, int>(coordinates.Item1, coordinates.Item2));
            }

            return copyOfMinesCoordinates;
        }

        // Metodo che restituisce il massimo numero di mine che può contenere un campo minato dalle dimensioni indicate
        public static int GetMaxMines(int minefieldLength, int minefieldHeight)
        {
            return (int)(minefieldLength * minefieldHeight * MaxMinesCoverage);
        }

        // Proprietà che restituisce la lunghezza (numero di colonne) del campo minato
        public int Length
        {
            get { return _length; }
        }

        // Proprietà che restituisce l'altezza (numero di righe) del campo minato
        public int Height
        {
            get { return _height; }
        }

        // Proprietà che restituisce il numero di mine contenute nel campo minato
        public int Mines
        {
            get { return _mines; }
        }

        // Proprietà che restituisce il numero totale di zone che costituiscono il campo minato
        public int TotalZones
        {
            get { return Length * Height; }
        }

        // Proprietà che restituisce il numero di zone sicure presenti nel campo minato
        public int SafeZones
        {
            get { return TotalZones - Mines; }
        }
    }
}
