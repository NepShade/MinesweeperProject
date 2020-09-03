using System;

namespace MinesweeperLibrary
{
    // Classe che fornisce dati evento relativi a una zona del campo minato
    public class ZoneEventArgs : EventArgs
    {
        // Attributi della classe
        // coordinate di posizione della zona all'interno del campo minato
        private readonly int _zoneCoordinateX;
        private readonly int _zoneCoordinateY;
        // variabile che indica se la zona è coperta
        private readonly bool _covered;
        // variabile che indica se la zona è contrassegnata come minata
        private readonly bool _flagged;
        // variabile che indica se la zona è minata
        private readonly bool _mined;
        // numero di mine rilevate attorno alla zona
        private readonly int _adjacentMines;

        // Costruttore che memorizza dati relativi a una zona del campo minato 
        public ZoneEventArgs(int x, int y, bool covered, bool flagged, bool mined, int adjacentMines)
        {
            _zoneCoordinateX = x;
            _zoneCoordinateY = y;
            _covered = covered;
            _flagged = flagged;
            _mined = mined;
            _adjacentMines = adjacentMines;
        }

        // Proprietà che restituisce la coordinata X della zona rispetto al campo minato
        public int X
        {
            get { return _zoneCoordinateX; }
        }

        // Proprietà che restituisce la coordinata Y della zona rispetto al campo minato
        public int Y
        {
            get { return _zoneCoordinateY; }
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
        public bool Mined
        {
            get { return _mined; }
        }

        // Proprietà che restituisce il numero di mine rilevate attorno alla zona
        public int AdjacentMines
        {
            get { return _adjacentMines; }
        }
    }
}
