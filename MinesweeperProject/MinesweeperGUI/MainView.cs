using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    // Classe rappresentante il form principale di gioco
    public partial class MainView : Form
    {
        // Attributi della classe
        // spaziatura dal bordo superiore del form
        public const int TopEdgeSpacing = 70;
        // spaziatura dal bordo inferiore e dai bordi laterali del form
        public const int EdgeSpacing = 10;
        // altezza occupata dal menu principale di gioco nel form
        private const int MainMenuHeight = 25;

        // Costruttore che inizializza i componenti del form
        public MainView()
        {
            InitializeComponent();
        }

        // Metodo che ridimensiona l'area client del form e che ricolloca al suo interno alcuni elementi
        public void ResizeClientArea(int newLength, int newHeight)
        {
            // viene ridimensionata l'area client del form
            int lengthClientArea = EdgeSpacing + newLength + EdgeSpacing;
            int heightClientArea = TopEdgeSpacing + newHeight + EdgeSpacing;
            ClientSize = new Size(lengthClientArea, heightClientArea);

            // si calcola lo spazio occupano dal 'menu della partita'
            int GameMenuHeight = TopEdgeSpacing - MainMenuHeight;

            // si cambia la posizione dell'etichetta-timer all'interno del form
            int beginTimerX = lengthClientArea - EdgeSpacing - _gameDurationLabel.Width;
            int beginTimerY = ((GameMenuHeight - _gameDurationLabel.Height) / 2) + MainMenuHeight;
            _gameDurationLabel.Location = new Point(beginTimerX, beginTimerY);

            // si calcola la coordinata X del punto in cui l'etichetta-contatore finisce nel form
            int endCounterX = _minesCounterLabel.Location.X + _minesCounterLabel.Width;
            // si calcola la distanza tra l'etichetta-contatore e l'etichetta-timer
            int spaceBetweenTimerAndCounter = beginTimerX - endCounterX;
            // si calcola e si arrotonda lo scostamento del bottone di gioco dal contatore e dal timer
            double spaceFromTimerAndCounter = (spaceBetweenTimerAndCounter - _gameButton.Width) / 2;
            int roundedSpaceFromTimerAndCounter = (int)Math.Round(spaceFromTimerAndCounter);

            // si cambia la posizione del bottone di gioco all'interno del form
            int beginButtonX = endCounterX + roundedSpaceFromTimerAndCounter;
            int beginButtonY = ((GameMenuHeight - _gameButton.Height) / 2) + MainMenuHeight;
            _gameButton.Location = new Point(beginButtonX, beginButtonY);
        }
    }
}
