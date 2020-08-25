using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using MinesweeperLibrary;

namespace MinesweeperGUI
{
    // Classe rappresentante il controllore dedicato a gestire il form principale di gioco
    public class MainController
    {
        // Attributi della classe
        // form principale di gioco
        private readonly MainView _mainForm;
        // controllore dedicato alla gestione delle impostazioni
        private SettingsController _settingsController;
        // memento delle impostazioni di gioco
        private IMemento _settingsMemento;
        // logica dietro una partita a "Campo Minato"
        private MinesweeperGame _minesweeperGame;
        // modalità di gioco
        private MinesweeperGame.GameModality _modality;
        // creatore di campo minato
        private IMinefieldCreator _minefieldCreator;
        // campo minato
        private Minefield _minefield;
        // insieme di zone interagibili dall'utente nel form di gioco
        private InteractableZone[,] _interactableZones;
        // durata di una partita
        private TimeSpan _gameDuration;
        // numero di zone da contrassegnare come minate per vincere la partita
        private int _zonesToFlag;
        // lunghezza, altezza e numero di mine del campo minato
        private int _length, _height, _mines;
        // variabile che indica se almeno una zona interagibile è stata scoperta
        private bool _interactableZoneUncovered = false;

        // Costruttore che crea il controllore dedicato a gestire il form principale di gioco
        public MainController()
        {
            _mainForm = new MainView();
            // si inizializzano gli eventi e i controlli del form principale di gioco
            InitializeFormEvents();
            InitializeFormControls();
            // si creano e si inizializzano le zone interagibili
            CreateInteractableZones();
            // viene visualizzato il form principale di gioco come finestra di dialogo modale
            _mainForm.ShowDialog();
        }

        // Metodo che inizializza gli eventi del form principale di gioco
        private void InitializeFormEvents()
        {
            // si creano dei gestori eventi per il click di ogni opzione del menu di gioco
            _mainForm.NewOptionClick += NewGame;
            _mainForm.SettingsOptionClick += CallUpSettings;
            _mainForm.RulesOptionClick += CallUpRules;
            _mainForm.ExitOptionClick += ExitGame;
            _mainForm.InfoOptionClick += CallUpGameInfo;
            // si crea un gestore eventi per il click del bottone della partita
            _mainForm.GameButtonClick += NewGame;
            // si crea un gestore eventi per il tick del timer della partita
            _mainForm.GameTimerTick += TimePassed;
        }

        // Metodo che inizializza i controlli del form principale di gioco
        private void InitializeFormControls()
        {
            // viene impostata l'immagine iniziale del bottone della partita
            _mainForm.SetGameButtonImage(Properties.Resources.InitialButton);
            // viene impostato il valore iniziale dell'etichetta-timer e dell'etichetta-contatore
            UpdateTimerLabel(TimeSpan.Zero);
            UpdateMinesCounterLabel(0);
            // si disabilita l'opzione del menu per consultare le informazioni della partita
            _mainForm.InfoOptionEnabled = false;
        }

        // Metodo che configura i controlli del form principale di gioco
        private void SetFormControls()
        {
            // si imposta l'immagine iniziale del bottone della partita
            _mainForm.SetGameButtonImage(Properties.Resources.InitialButton);
            // si ferma il timer, si azzera la durata della partita e si reimposta l'etichetta-timer
            _mainForm.StopGameTimer();
            _gameDuration = TimeSpan.Zero;
            UpdateTimerLabel(TimeSpan.Zero);
            // si reimposta il numero di zone da contrassegnare come minate e l'etichetta-contatore
            _zonesToFlag = _mines;
            UpdateMinesCounterLabel(_mines);
            // si disabilita l'opzione del menu per consultare le informazioni della partita
            _mainForm.InfoOptionEnabled = false;
        }

        // Metodo che crea e inizializza le zone interagibili
        private void CreateInteractableZones()
        {
            // creazione e inizializzazione di una matrice di zone interagibili di dimensioni massime
            _interactableZones = new InteractableZone[Minefield.MaxSide, Minefield.MaxSide];
            for (int y = 0; y < Minefield.MaxSide; y++)
                for (int x = 0; x < Minefield.MaxSide; x++)
                {
                    // si calcolano le coordinate di posizione della zona interagibile
                    int positionX = MainView.EdgeSpacing + (Properties.Resources.CoveredZone.Width * x);
                    int positionY = MainView.TopEdgeSpacing + (Properties.Resources.CoveredZone.Height * y);

                    // si crea una zona interagibile e se ne impostano le proprietà
                    _interactableZones[x, y] = new InteractableZone(x, y)
                    {
                        Width = Properties.Resources.CoveredZone.Width,
                        Height = Properties.Resources.CoveredZone.Height,
                        Location = new Point(positionX, positionY),
                        Enabled = false,
                        Visible = false
                    };

                    // si aggiunge un gestore eventi al click della zona interagibile
                    _interactableZones[x, y].MouseClick += InteractableZoneSelected;
                    // si aggiunge la zona interagibile alla raccolta di controlli del form
                    _mainForm.Controls.Add(_interactableZones[x, y]);
                }
        }

        // Metodo che ridimensiona il form principale di gioco e che visualizza le zone interagibili
        private void SetInteractableZones()
        {
            // si calcolano le nuove dimensioni del form principale di gioco per poi ridimensionarlo
            int lengthMinefieldForm = Properties.Resources.CoveredZone.Width * _length;
            int heightMinefieldForm = Properties.Resources.CoveredZone.Height * _height;
            _mainForm.ResizeClientArea(lengthMinefieldForm, heightMinefieldForm);

            // si stabilisce quali zone interagibili abilitare e rendere visibili
            for (int y = 0; y < Minefield.MaxSide; y++)
                for (int x = 0; x < Minefield.MaxSide; x++)
                {
                    if (x < _length && y < _height)
                    {
                        // se le coordinate esaminate sono valide rispetto alle dimensioni del campo
                        // minato allora si rende la zona interagibile visibile e capace di rispondere
                        // alle interazioni con gli utenti...
                        _interactableZones[x, y].Visible = true;
                        _interactableZones[x, y].Enabled = true;
                        // ...inoltre si stabilisce l'immagine e lo stato iniziale da associare a tale zona
                        _interactableZones[x, y].Image = Properties.Resources.CoveredZone;
                        _interactableZones[x, y].Status = InteractableZone.ZoneState.Covered;
                    }
                    else
                    {
                        // se le coordinate esaminate non sono valide rispetto alle dimensioni del
                        // campo minato allora si rende la zona interagibile invisibile e incapace
                        // di rispondere alle interazioni con gli utenti
                        _interactableZones[x, y].Visible = false;
                        _interactableZones[x, y].Enabled = false;
                    }
                }

            // si annota che nessuna zona interagibile è stata ancora scoperta dall'utente
            _interactableZoneUncovered = false;
        }

        // Metodo eseguito quando si vuole iniziare una nuova partita
        private void NewGame(object sender, EventArgs e)
        {
            if (_minesweeperGame == null)
            {
                // se la partita non è stata configurata viene richiamato il form delle impostazioni
                CallUpSettings(sender, e);
            }
            else
            {
                // se la partita è stata configurata ed è stata scoperta almeno una zona interagibile...
                if (_interactableZoneUncovered)
                {
                    // ...si reimpostano le zone interagibili...
                    SetInteractableZones();
                    // ...si riconfigurano i controlli del form di gioco...
                    SetFormControls();
                    // ...si crea un nuovo campo minato dalle medesime caratteristiche...
                    _minefield.CreateNewMinefield();
                    // ...e si crea una nuova partita dalle medesime impostazioni
                    _minesweeperGame.NewGame();
                }
            }
        }

        // Metodo che richiama il form delle impostazioni
        private void CallUpSettings(object sender, EventArgs e)
        {
            // si crea il controllore dedicato a gestire il form delle impostazioni
            _settingsController = new SettingsController();
            // si crea un gestore eventi per acquisire la configurazione di gioco stabilita dall'utente
            _settingsController.ConfigurationEstablished += AcquireConfiguration;
            // si ripristina lo stato precedente del form delle impostazioni
            _settingsController.RestoreState(_settingsMemento);
            // si visualizza il form delle impostazioni
            _settingsController.ShowForm();
        }

        // Metodo che richiama il form delle regole di gioco
        private void CallUpRules(object sender, EventArgs e)
        {
            // si crea il form delle regole di gioco
            RulesView rulesView = new RulesView();
            // si imposta l'immagine contenente le regole di gioco
            rulesView.SetRulesImage(Properties.Resources.MinesweeperRules);
            // si visualizza il form come finestra di dialogo modale
            rulesView.ShowDialog();
        }

        // Metodo che chiude il form principale di gioco
        private void ExitGame(object sender, EventArgs e)
        {
            // si chiude il form principale di gioco
            _mainForm.Close();
        }

        // Metodo che richiama il form che visualizza le informazioni relative alla partita in corso
        private void CallUpGameInfo(object sender, EventArgs e)
        {
            // si crea il form delle informazioni sulla partita
            InfoView gameInfo = new InfoView
            {
                // vengono aggiornate le informazioni contenute nel form
                MinefieldName = _minefieldCreator.GetMinefieldName(),
                MinefieldLength = _length.ToString(),
                MinefieldHeight = _height.ToString(),
                MinefieldMines = _mines.ToString(),
                ModalityName = _minesweeperGame.SelectedModality.ToString(),
                Attempts = "---"
            };

            // vengono visualizzati i tentativi rimanenti e totali se tale informazione è definita
            if (_minesweeperGame.RemainingAttempts != null)
                gameInfo.Attempts = _minesweeperGame.RemainingAttempts + " / " + _minesweeperGame.TotalAttempts;

            // si visualizza il form come finestra di dialogo modale
            gameInfo.ShowDialog();
        }

        // Metodo eseguito quando l'utente ha stabilito la configurazione di gioco
        private void AcquireConfiguration(object sender, EventArgs e)
        {
            if (_settingsMemento == null)
            {
                // CASO A - Non è stata acquisita alcuna configurazione di gioco
                // si acquisisce la configurazione di gioco stabilita dall'utente
                _minefieldCreator = _settingsController.GetGameConfiguration(out _length, out _height,
                                                                             out _mines, out _modality);

                // si crea un campo minato e una partita opportunamente configurate con le scelte dell'utente
                _minefield = _minefieldCreator.CreateMinefield(_length, _height, _mines);
                _minesweeperGame = new MinesweeperGame(_minefield, _modality);
                // si crea un gestore eventi per la scoperta di una zona del campo minato
                _minesweeperGame.ZoneUncovered += UpdateInteractableZone;
                // si configurano le zone interagibili e i controlli del form di gioco
                SetInteractableZones();
                SetFormControls();
                // si memorizza lo stato attuale del form delle impostazioni
                _settingsMemento = _settingsController.SaveState();
            }
            else
            {
                // CASO B - Almeno una configurazione di gioco è stata acquisita
                // si acquisisce lo stato attuale del form delle impostazioni
                IMemento newMemento = _settingsController.SaveState();

                // si configura una nuova partita se almeno una zona interagibile è stata scoperta
                // oppure se il vecchio e il nuovo memento delle impostazioni differiscono tra loro
                if (_interactableZoneUncovered || !_settingsMemento.Equals(newMemento))
                {
                    // si acquisisce e si analizza la configurazione di gioco più recente
                    AcquireNewConfiguration();
                    // si configurano le zone interagibili e i controlli del form di gioco
                    SetInteractableZones();
                    SetFormControls();
                    // si memorizza lo stato attuale del form delle impostazioni
                    _settingsMemento = newMemento;
                }
            }
        }

        // Metodo che acquisisce e analizza la configurazione di gioco più recente stabilita dall'utente
        private void AcquireNewConfiguration()
        {
            // si memorizzano a parte il vecchio creatore di campo minato e la vecchia modalità di gioco
            IMinefieldCreator oldMinefieldCreator = _minefieldCreator;
            MinesweeperGame.GameModality oldModality = _modality;

            // si acquisisce la nuova configurazione di gioco stabilita dall'utente
            _minefieldCreator = _settingsController.GetGameConfiguration(out _length, out _height,
                                                                         out _mines, out _modality);

            // CASO A - se si ha acquisito una nuova tipologia di campo minato oppure una nuova
            // modalità di gioco allora si crea una nuova partita con nuove impostazioni
            if (oldMinefieldCreator.GetMinefieldName() != _minefieldCreator.GetMinefieldName() ||
                oldModality != _modality)
            {
                // si determina se creare un nuovo campo minato dello stesso tipo oppure di tipo diverso
                if (oldMinefieldCreator.GetMinefieldName() != _minefieldCreator.GetMinefieldName())
                    _minefield = _minefieldCreator.CreateMinefield(_length, _height, _mines);
                else
                    _minefield.CreateNewMinefield(_length, _height, _mines);

                // si crea una nuova partita con nuove impostazioni
                _minesweeperGame.NewGame(_minefield, _modality);
            }
            else
            {
                // CASO B - se si ha acquisito la stessa tipologia di campo minato e la stessa
                // modalità di gioco allora si crea una nuova partita con le medesime impostazioni
                _minefield.CreateNewMinefield(_length, _height, _mines);
                _minesweeperGame.NewGame();
            }
        }

        // Metodo eseguito quando l'utente clicca su una zona interagibile
        private void InteractableZoneSelected(object sender, MouseEventArgs e)
        {
            // conversione esplicita del riferimento al mittente in zona interagibile
            InteractableZone selectedZone = (InteractableZone)sender;

            if (e.Button == MouseButtons.Left)
            {
                // se si è premuto il pulsante sinistro del mouse la zona viene scoperta
                UncoverZone(selectedZone.X, selectedZone.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                // se si è premuto il pulsante destro del mouse la zona viene contrassegnata
                // come minata oppure ripulita (ossia NON contrassegnata come minata)
                FlagUnflagZone(selectedZone.X, selectedZone.Y);
            }
        }

        // Metodo che contrassegna come minata oppure "ripulisce" la zona dalle coordinate indicate
        private void FlagUnflagZone(int x, int y)
        {
            // si prova a contrassegnare o "ripulire" la zona del campo minato dalle coordinate indicate
            if (_minesweeperGame.FlagUnflagZone(x, y, out bool? zoneIsFlagged))
            {
                // a operazione riuscita si valuta se si abbia contrassegnato o "ripulito" la zona
                if (zoneIsFlagged == true)
                {
                    // si aggiorna l'immagine e lo stato della zona interagibile per indicare che
                    // la corrispondente zona del campo minato è stata contrassegnata come minata
                    _interactableZones[x, y].Image = Properties.Resources.FlaggedZone;
                    _interactableZones[x, y].Status = InteractableZone.ZoneState.Flagged;
                    // si decrementa il numero di zone da contrassegnare come minate
                    // aggiornando anche l'etichetta-contatore nel form di gioco
                    UpdateMinesCounterLabel(--_zonesToFlag);
                }
                else if (zoneIsFlagged == false)
                {
                    // si aggiorna l'immagine e lo stato della zona interagibile per indicare che
                    // la corrispondente zona del campo minato è stata "ripulita"
                    _interactableZones[x, y].Image = Properties.Resources.CoveredZone;
                    _interactableZones[x, y].Status = InteractableZone.ZoneState.Covered;
                    // si incrementa il numero di zone da contrassegnare come minate
                    // aggiornando anche l'etichetta-contatore nel form di gioco
                    UpdateMinesCounterLabel(++_zonesToFlag);
                }
            }
        }

        // Metodo che scopre la zona dalle coordinate indicate
        private void UncoverZone(int x, int y)
        {
            // se non è stata ancora scoperta alcuna zona interagibile...
            if (!_interactableZoneUncovered)
            {
                // ...si annota che almeno una zona interagibile è stata scoperta, si avvia il timer
                // e si abilita l'opzione del menu per consultare le informazioni della partita
                _interactableZoneUncovered = true;
                _mainForm.StartGameTimer();
                _mainForm.InfoOptionEnabled = true;
            }

            // si prova a scoprire la zona del campo minato dalle coordinate indicate
            if (_minesweeperGame.UncoverZone(x, y))
            {
                // a operazione riuscita si verifica se la partita è terminata
                if (_minesweeperGame.EndGame)
                {
                    // si ferma il timer e si valuta l'esito della partita per stabilire quali azioni intraprendere
                    _mainForm.StopGameTimer();
                    if (_minesweeperGame.Victory == true)
                        GameWon();
                    else if (_minesweeperGame.Victory == false)
                        GameLost();
                }
            }
        }

        // Metodo che aggiorna una zona interagibile quando la corrispondente zona del campo minato viene scoperta
        private void UpdateInteractableZone(object sender, ZoneEventArgs e)
        {
            if (e.Mined)
            {
                // se la zona scoperta è minata si modifica l'immagine e lo stato della
                // corrispondente zona interagibile per indicare una zona minata esplosa
                _interactableZones[e.X, e.Y].Image = Properties.Resources.RedMinedZone;
                _interactableZones[e.X, e.Y].Status = InteractableZone.ZoneState.Mined;
                // si riduce il numero di zone da contrassegnare come minate
                // aggiornando anche l'etichetta-contatore nel form di gioco
                UpdateMinesCounterLabel(--_zonesToFlag);
            }
            else
            {
                // se la zona scoperta non è minata si modifica lo stato della corrispondente
                // zona interagibile per indicare una zona sicura...
                _interactableZones[e.X, e.Y].Status = InteractableZone.ZoneState.Safe;
                // ...assieme alla sua immagine per riflettere il numero di mine rilevate attorno alla zona
                switch (e.AdjacentMines)
                {
                    case 0:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone0;
                        break;
                    case 1:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone1;
                        break;
                    case 2:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone2;
                        break;
                    case 3:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone3;
                        break;
                    case 4:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone4;
                        break;
                    case 5:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone5;
                        break;
                    case 6:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone6;
                        break;
                    case 7:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone7;
                        break;
                    case 8:
                        _interactableZones[e.X, e.Y].Image = Properties.Resources.UncoveredZone8;
                        break;
                }
            }
        }

        // Metodo che aggiorna opportunamente il form principale di gioco in caso di partita vinta
        private void GameWon()
        {
            // le rimanenti zone coperte e non contrassegnate vengono contrassegnate come minate
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _length; x++)
                {
                    // si disabilita la risposta della zona interagibile alle interazioni degli utenti
                    _interactableZones[x, y].Enabled = false;

                    // se la zona interagibile è coperta e non contrassegnata...
                    if (_interactableZones[x, y].Status == InteractableZone.ZoneState.Covered)
                    {
                        // ...allora si modifica la sua immagine per indicare una zona contrassegnata
                        _interactableZones[x, y].Image = Properties.Resources.FlaggedZone;
                        // si riduce il numero di zone da contrassegnare come minate
                        // aggiornando anche l'etichetta-contatore nel form di gioco
                        UpdateMinesCounterLabel(--_zonesToFlag);
                    }
                }

            // si modifica l'immagine associata al bottone della partita per indicare la vittoria
            _mainForm.SetGameButtonImage(Properties.Resources.VictoryButton);
        }

        // Metodo che aggiorna opportunamente il form principale di gioco in caso di partita persa
        private void GameLost()
        {
            // si acquisiscono le coordinate di posizione delle mine
            List<Tuple<int, int>> minesCoordinates = _minefield.GetMinesCoordinates();

            // si visualizzano le mine non individuate e le zone erroneamente contrassegnate come minate
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _length; x++)
                {
                    // si disabilita la risposta della zona interagibile alle interazioni degli utenti
                    _interactableZones[x, y].Enabled = false;

                    if (minesCoordinates.Contains(new Tuple<int, int>(x, y)))
                    {
                        // se le coordinate analizzate fanno riferimento a una zona minata ma la corrispondente
                        // zona interagibile non è stata contrassegnata allora l'immagine e lo stato della
                        // zona interagibile vengono modificate per indicare una zona minata non individuata
                        if (_interactableZones[x, y].Status == InteractableZone.ZoneState.Covered)
                        {
                            _interactableZones[x, y].Image = Properties.Resources.MinedZone;
                            _interactableZones[x, y].Status = InteractableZone.ZoneState.Mined;
                        }
                    }
                    else
                    {
                        // se le coordinate analizzate non fanno riferimento a una zona minata ma la
                        // corrispondente zona interagibile è stata contrassegnata allora l'immagine della
                        // zona interagibile viene modificata per indicare una zona erroneamente contrassegnata
                        if (_interactableZones[x, y].Status == InteractableZone.ZoneState.Flagged)
                            _interactableZones[x, y].Image = Properties.Resources.ErroredFlaggedZone;
                    }
                }

            // numero di mine non contrassegnate come minate al termine della partita
            int minesNotFlagged = 0;
            // si conteggiano tutte le zone minate che non sono state contrassegnate al termine della partita
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _length; x++)
                    if (_interactableZones[x, y].Status == InteractableZone.ZoneState.Mined)
                        minesNotFlagged++;

            // si aggiorna l'etichetta-contatore per indicare la quantità totale di mine non individuate
            UpdateMinesCounterLabel(minesNotFlagged);

            // si modifica l'immagine associata al bottone della partita per indicare la sconfitta
            _mainForm.SetGameButtonImage(Properties.Resources.DefeatButton);
        }

        // Metodo che aggiorna l'etichetta-contatore nel form principale di gioco
        private void UpdateMinesCounterLabel(int number)
        {
            // nuovo valore in formato stringa da assegnare all'etichetta-contatore
            string stringNewValue = "";

            // se il numero acquisito è negativo si aggiunge il simbolo "meno" al valore finale
            if (number < 0)
                stringNewValue += "-";

            // si converte in stringa il valore assoluto del numero acquisito
            string stringAbsoluteNumber = Math.Abs(number).ToString();
            // si aggiungono caratteri al valore finale fino a comporre un numero a tre cifre
            if (stringAbsoluteNumber.Length == 1)
                stringNewValue += ("00" + stringAbsoluteNumber);
            else if (stringAbsoluteNumber.Length == 2)
                stringNewValue += ("0" + stringAbsoluteNumber);
            else
                stringNewValue += stringAbsoluteNumber;

            // si aggiorna il valore dell'etichetta-contatore
            _mainForm.MinesCounterLabel = stringNewValue;
        }

        // Metodo eseguito quando il timer della partita varia
        private void TimePassed(object sender, EventArgs e)
        {
            // la durata della partita viene incrementata di un secondo
            _gameDuration += new TimeSpan(0, 0, 1);
            // si aggiorna l'etichetta-timer nel form principale di gioco
            UpdateTimerLabel(_gameDuration);
        }

        // Metodo che aggiorna l'etichetta-timer nel form principale di gioco
        private void UpdateTimerLabel(TimeSpan time)
        {
            // si convertono in stringa i secondi, i minuti, e le ore passate dall'inizio della partita
            string seconds = time.Seconds.ToString();
            string minutes = time.Minutes.ToString();
            string hours = time.Hours.ToString();

            // se i secondi sono a singola cifra viene aggiunta un'altra cifra per le decine
            if (seconds.Length == 1)
                seconds = "0" + seconds;

            // se i minuti sono a singola cifra viene aggiunta un'altra cifra per le decine
            if (minutes.Length == 1)
                minutes = "0" + minutes;

            // se la partita raggiunge l'ora di durata...
            if (hours != "0")
            {
                // ...si azzera la sua durata per poi aggiornare l'etichetta-timer...
                _gameDuration = TimeSpan.Zero;
                UpdateTimerLabel(_gameDuration);
            }
            else
                // ...altrimenti si aggiorna direttamente l'etichetta-timer
                _mainForm.GameDurationLabel = minutes + ":" + seconds;
        }
    }
}
