using System;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    // Classe rappresentante una zona del campo minato che risulta visibile e interagibile nel form di gioco
    public class InteractableZone : PictureBox
    {
        // stati assumibili da una zona interagibile
        public enum ZoneState { Covered, Flagged, IncorrectlyFlagged, Safe0, Safe1, Safe2,
                                Safe3, Safe4, Safe5, Safe6, Safe7, Safe8, Mined, Exploded };

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
            Status = ZoneState.Covered;
            Height = Properties.Resources.CoveredZone.Height;
            Width = Properties.Resources.CoveredZone.Width;
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
                // si verifica se lo stato specificato è valido
                if (Enum.IsDefined(typeof(ZoneState), value))
                {
                    // si aggiorna lo stato e l'immagine della zona interagibile
                    _zoneState = value;
                    switch (value)
                    {
                        case ZoneState.Covered:
                            Image = Properties.Resources.CoveredZone;
                            break;
                        case ZoneState.Flagged:
                            Image = Properties.Resources.FlaggedZone;
                            break;
                        case ZoneState.IncorrectlyFlagged:
                            Image = Properties.Resources.ErroredFlaggedZone;
                            break;
                        case ZoneState.Safe0:
                            Image = Properties.Resources.UncoveredZone0;
                            break;
                        case ZoneState.Safe1:
                            Image = Properties.Resources.UncoveredZone1;
                            break;
                        case ZoneState.Safe2:
                            Image = Properties.Resources.UncoveredZone2;
                            break;
                        case ZoneState.Safe3:
                            Image = Properties.Resources.UncoveredZone3;
                            break;
                        case ZoneState.Safe4:
                            Image = Properties.Resources.UncoveredZone4;
                            break;
                        case ZoneState.Safe5:
                            Image = Properties.Resources.UncoveredZone5;
                            break;
                        case ZoneState.Safe6:
                            Image = Properties.Resources.UncoveredZone6;
                            break;
                        case ZoneState.Safe7:
                            Image = Properties.Resources.UncoveredZone7;
                            break;
                        case ZoneState.Safe8:
                            Image = Properties.Resources.UncoveredZone8;
                            break;
                        case ZoneState.Mined:
                            Image = Properties.Resources.MinedZone;
                            break;
                        case ZoneState.Exploded:
                            Image = Properties.Resources.RedMinedZone;
                            break;
                    }
                }
            }
        }
    }
}
