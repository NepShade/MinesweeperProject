using System;

namespace MinesweeperLibrary
{
    // Classe rappresentante una zona di un campo minato
    public class MinefieldZone
    {
        // Attributi della classe
        // variabile che indica se la zona è coperta
        private bool _covered = true;
        // variabile che indica se la zona è contrassegnata come minata
        private bool _flagged = false;
        // variabile che indica se la zona è minata
        private bool _mined = false;
        // numero di mine rilevate attorno alla zona
        private int _adjacentMines = 0;

        // Costruttore che genera una zona sicura e senza mine adiacenti
        public MinefieldZone() { }

        // Costruttore che genera una zona dalle caratteristiche specificate
        public MinefieldZone(int adjacentMines, bool zoneMined)
        {
            SetZone(adjacentMines, zoneMined);
        }

        // Metodo che reimposta la zona e che configura alcune sue caratteristiche
        public void SetZone(int adjacentMines, bool zoneMined)
        {
            _covered = true;
            _flagged = false;
            _mined = zoneMined;

            if (adjacentMines < 0)
                _adjacentMines = 0;
            else if (adjacentMines > 8)
                _adjacentMines = 8;
            else
                _adjacentMines = adjacentMines;
        }

        // Metodo che contrassegna la zona come minata o la riporta al suo stato originario
        // restituendo un valore booleano che indica se essa è stata contrassegnata come minata
        public bool FlagUnflag()
        {
            if (!Covered)
                throw new InvalidOperationException
                    ("Non si può contrassegnata come minata una zona scoperta!");

            _flagged = !Flagged;
            return Flagged;
        }

        // Metodo che scopre la zona restituendo un valore boolano che indica se essa è minata
        // assieme al numero di mine rilevate attorno alla zona
        public bool Uncover(out int adjacentMines)
        {
            if (Flagged)
                throw new InvalidOperationException
                    ("Non si può scoprire una zona contrassegnata come minata!");

            if (!Covered)
                throw new InvalidOperationException
                    ("Non si può scoprire una zona già scoperta!");

            _covered = false;

            adjacentMines = _adjacentMines;
            return _mined;
        }

        // Metodo che indica se la zona scoperta è minata
        public bool IsMined()
        {
            if (Covered)
                throw new InvalidOperationException
                    ("Non si può accedere alle informazioni di una zona coperta!");

            return _mined;
        }

        // Metodo che restituisce il numero di mine rilevate attorno alla zona scoperta
        public int GetAdjacentMines()
        {
            if (Covered)
                throw new InvalidOperationException
                    ("Non si può accedere alle informazioni di una zona coperta!");

            return _adjacentMines;
        }

        // Proprietà che indica se la zona è coperta
        public bool Covered
        {
            get { return _covered; }
        }

        // Proprietà che indica se la zona è contrassegnata come minata
        public bool Flagged
        {
            get { return _flagged; }
        }

        // Proprietà che indica se la zona è minata
        internal bool Mined
        {
            get { return _mined; }
        }

        // Proprietà che restituisce il numero di mine rilevate attorno alla zona
        internal int AdjacentMines
        {
            get { return _adjacentMines; }
        }
    }
}
