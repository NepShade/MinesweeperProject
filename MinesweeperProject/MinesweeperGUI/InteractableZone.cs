using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    // Classe rappresentante una zona del campo minato che risulta visibile e interagibile nel form di gioco
    public class InteractableZone : PictureBox
    {
        // stati assumibili da una zona interagibile
        public enum ZoneState { Covered, Flagged, Safe, Mined };

        // Attributi della classe
        // coordinate della zona a cui la zona interagibile fa riferimento
        private readonly int _minefieldZoneX;
        private readonly int _minefieldZoneY;
        // stato assunto dalla zona interagibile
        private ZoneState _zoneState;

        // Costruttore che genera una zona interagibile
        public InteractableZone(int minefieldZoneX, int minefieldZoneY)
        {
            _minefieldZoneX = minefieldZoneX;
            _minefieldZoneY = minefieldZoneY;
            _zoneState = ZoneState.Covered;
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

        // Proprietà che restituisce e imposta lo stato della zona interagibile
        public ZoneState Status
        {
            get { return _zoneState; }
            set
            {
                // si aggiorna lo stato della zona interagibile se lo stato specificato è valido
                if (Enum.IsDefined(typeof(ZoneState), value))
                    _zoneState = value;
            }
        }
    }
}
