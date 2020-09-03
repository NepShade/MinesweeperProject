using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    // Classe che fornisce dati evento relativi a una zona interagibile
    public class InteractableZoneEventArgs : EventArgs
    {
        // Attributi della classe
        // coordinate della zona a cui la zona interagibile fa riferimento
        private readonly int _minefieldZoneX;
        private readonly int _minefieldZoneY;
        // stato assunto dalla zona interagibile
        private readonly InteractableZone.ZoneState _interactableZoneState;
        // pulsante del mouse premuto sulla zona interagibile
        private readonly MouseButtons _mouseButtonClicked;

        // Costruttore che memorizza dati relativi a una zona interagibile
        public InteractableZoneEventArgs(int x, int y, InteractableZone.ZoneState zoneState, MouseButtons mouseButton)
        {
            _minefieldZoneX = x;
            _minefieldZoneY = y;
            _interactableZoneState = zoneState;
            _mouseButtonClicked = mouseButton;
        }

        // Proprietà che restituisce la coordinata X della zona a cui la zona interagibile fa riferimento
        public int X
        {
            get { return _minefieldZoneX; }
        }

        // Proprietà che restituisce la coordinata Y della zona a cui la zona interagibile fa riferimento
        public int Y
        {
            get { return _minefieldZoneY; }
        }

        // Proprietà che restituisce lo stato della zona interagibile
        public InteractableZone.ZoneState Status
        {
            get { return _interactableZoneState; }
        }

        // Proprietà che restituisce il pulsante del mouse premuto sulla zona interagibile
        public MouseButtons MouseButtonClicked
        {
            get { return _mouseButtonClicked; }
        }
    }
}
