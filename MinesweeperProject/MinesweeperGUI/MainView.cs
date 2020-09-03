using System;
using System.Drawing;
using System.Windows.Forms;
using MinesweeperLibrary;

namespace MinesweeperGUI
{
    // Classe rappresentante il form principale di gioco
    public partial class MainView : Form
    {
        // Eventi della classe
        // evento che segnala che una zona interagibile nel form è stata cliccata
        public event EventHandler<InteractableZoneEventArgs> InteractableZoneClick;

        // Attributi della classe
        // spaziatura dal bordo superiore del form
        private const int TopEdgeSpacing = 70;
        // spaziatura dal bordo inferiore e dai bordi laterali del form
        private const int EdgeSpacing = 10;
        // altezza occupata dal menu principale di gioco nel form
        private const int MainMenuHeight = 25;
        // insieme di zone interagibili dall'utente nel form
        private readonly InteractableZone[,] _interactableMinefield;
        // lunghezza e altezza della porzione visibile del campo minato interagibile
        private int _interactableMinefieldActualLength = 0;
        private int _interactableMinefieldActualHeight = 0;

        // Costruttore che inizializza i componenti del form
        public MainView()
        {
            InitializeComponent();

            // creazione e inizializzazione di un campo minato interagibile di dimensioni massime
            _interactableMinefield = new InteractableZone[Minefield.MaxSide,Minefield.MaxSide];
            for (int y = 0; y < Minefield.MaxSide; y++)
                for (int x = 0; x < Minefield.MaxSide; x++)
                {
                    // si crea una zona interagibile e se ne impostano alcune proprietà
                    _interactableMinefield[x, y] = new InteractableZone(x, y)
                    {
                        Enabled = false,
                        Visible = false
                    };
                    _interactableMinefield[x, y].MouseClick += InteractableZoneMouseClicked;

                    // si calcola e si imposta la posizione della zona interagibile
                    int positionX = EdgeSpacing + (_interactableMinefield[x, y].Width * x);
                    int positionY = TopEdgeSpacing + (_interactableMinefield[x, y].Height * y);
                    _interactableMinefield[x, y].Location = new Point(positionX, positionY);
                    // si aggiunge la zona interagibile alla raccolta di controlli del form
                    Controls.Add(_interactableMinefield[x, y]);
                }
        }

        // Metodo che segnala quando una zona interagibile nel form è stata cliccata dall'utente
        private void InteractableZoneMouseClicked(object sender, MouseEventArgs e)
        {
            // conversione esplicita del riferimento al mittente in zona interagibile
            InteractableZone zoneClicked = (InteractableZone)sender;

            // si segnala che una zona interagibile nel form è stata cliccata dall'utente
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                if (InteractableZoneClick != null)
                    try { InteractableZoneClick(this, new InteractableZoneEventArgs(zoneClicked.X,
                                                                                      zoneClicked.Y,
                                                                                      zoneClicked.Status,
                                                                                      e.Button)); }
                    catch { }
        }

        // Metodo che ridimensiona l'area client del form e che ricolloca al suo interno alcuni elementi
        private void ResizeClientArea(int newLength, int newHeight)
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

        // Metodo che imposta le dimensioni del campo minato interagibile
        public bool SetInteractableMinefieldSize(int length, int height)
        {
            if (length >= Minefield.MinSide && length <= Minefield.MaxSide &&
                height >= Minefield.MinSide && height <= Minefield.MaxSide)
            {
                // si stabilisce quali zone interagibili abilitare e rendere visibili
                for (int y = 0; y < Minefield.MaxSide; y++)
                    for (int x = 0; x < Minefield.MaxSide; x++)
                    {
                        if (x < length && y < height)
                        {
                            _interactableMinefield[x, y].Visible = true;
                            _interactableMinefield[x, y].Enabled = true;
                            _interactableMinefield[x, y].Status = InteractableZone.ZoneState.Covered;
                        }
                        else
                        {
                            _interactableMinefield[x, y].Visible = false;
                            _interactableMinefield[x, y].Enabled = false;
                        }
                    }

                // si ridimensiona il form in base alla quantità di zone interattive da visualizzare
                int interactableMinefieldLengthPixel = _interactableMinefield[0, 0].Width * length;
                int interactableMinefieldHeightPixel = _interactableMinefield[0, 0].Height * height;
                ResizeClientArea(interactableMinefieldLengthPixel, interactableMinefieldHeightPixel);
                // si acquisiscono le nuove dimensioni attuali del campo minato interattivo
                _interactableMinefieldActualLength = length;
                _interactableMinefieldActualHeight = height;

                return true;
            }

            return false;
        }

        // Metodo che imposta l'immagine del bottone della partita
        public void SetGameButtonImage(Image buttonImage)
        {
            _gameButton.Image = buttonImage;
        }

        // Metodo che avvia il timer della partita
        public void StartGameTimer()
        {
            _gameTimer.Start();
        }

        // Metodo che arresta il timer della partita
        public void StopGameTimer()
        {
            _gameTimer.Stop();
        }

        // Metodo che restituisce lo stato della zona interagibile dalle coordinate indicate
        public InteractableZone.ZoneState? GetInteractableZoneState(int x, int y)
        {
            if (x >= 0 && x < InteractableMinefieldLength && y >= 0 && y < InteractableMinefieldHeight)
                return _interactableMinefield[x, y].Status;
            else
                return null;
        }

        // Metodo che imposta lo stato della zona interagibile dalle coordinate indicate
        public bool SetInteractableZoneState(int x, int y, InteractableZone.ZoneState newZoneState)
        {
            if (x >= 0 && x < InteractableMinefieldLength && y >= 0 && y < InteractableMinefieldHeight)
                if (Enum.IsDefined(typeof(InteractableZone.ZoneState), newZoneState))
                {
                    _interactableMinefield[x, y].Status = newZoneState;
                    return true;
                }

            return false;
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta relativa al numero di zone da contrassegnare
        public string MinesCounterLabel
        {
            get { return _minesCounterLabel.Text; }
            set { _minesCounterLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta relativa alla durata della partita
        public string GameDurationLabel
        {
            get { return _gameDurationLabel.Text; }
            set { _gameDurationLabel.Text = value; }
        }

        // Proprietà che abilita e disabilita l'opzione 'info' del menu
        public bool InfoOptionEnabled
        {
            get { return _infoToolStripMenuItem.Enabled; }
            set { _infoToolStripMenuItem.Enabled = value; }
        }

        // Proprietà che restituisce la lunghezza del campo minato interagibile
        public int InteractableMinefieldLength
        {
            get { return _interactableMinefieldActualLength; }
        }

        // Proprietà che restituisce l'altezza del campo minato interagibile
        public int InteractableMinefieldHeight
        {
            get { return _interactableMinefieldActualHeight; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al click dell'opzione 'nuova' del menu
        public event EventHandler NewOptionClick
        {
            add { _newToolStripMenuItem.Click += value; }
            remove { _newToolStripMenuItem.Click -= value; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al click dell'opzione 'configura' del menu
        public event EventHandler SettingsOptionClick
        {
            add { _settingsToolStripMenuItem.Click += value; }
            remove { _settingsToolStripMenuItem.Click -= value; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al click dell'opzione 'regole' del menu
        public event EventHandler RulesOptionClick
        {
            add { _rulesToolStripMenuItem.Click += value; }
            remove { _rulesToolStripMenuItem.Click -= value; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al click dell'opzione 'esci' del menu
        public event EventHandler ExitOptionClick
        {
            add { _exitToolStripMenuItem.Click += value; }
            remove { _exitToolStripMenuItem.Click -= value; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al click dell'opzione 'info' del menu
        public event EventHandler InfoOptionClick
        {
            add { _infoToolStripMenuItem.Click += value; }
            remove { _infoToolStripMenuItem.Click -= value; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al click del bottone della partita
        public event EventHandler GameButtonClick
        {
            add { _gameButton.Click += value; }
            remove { _gameButton.Click -= value; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al tick del timer della partita
        public event EventHandler GameTimerTick
        {
            add { _gameTimer.Tick += value; }
            remove { _gameTimer.Tick -= value; }
        }
    }
}
