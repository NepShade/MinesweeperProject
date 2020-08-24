using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinesweeperLibrary;

namespace MinesweeperGUI
{
    // Classe rappresentante il controllore dedicato a gestire il form delle impostazioni
    public class SettingsController
    {
        // Eventi della classe
        // evento che segnala che la configurazione di gioco è stata impostata dall'utente
        public event EventHandler ConfigurationEstablished;

        // Attributi della classe
        // nome associato alla difficoltà personalizzata
        private const string CustomDifficulty = "Personalizzata";
        // form delle impostazioni di gioco
        private readonly SettingsView _settings;
        // assistente per l'utilizzo della libreria
        private readonly MinesweeperHelper _helper;
        // lista delle difficoltà di gioco con associati i valori delle caratteristiche del campo minato
        private List<Tuple<string, int, int, int>> _difficultiesList;

        // Costruttore che crea il controllore dedicato a gestire il form delle impostazioni
        public SettingsController()
        {
            _settings = new SettingsView();
            _helper = new MinesweeperHelper();
            // viene creata la lista delle difficoltà di gioco
            CreateDifficultiesList();
            // si configurano i controlli e si inizializzano gli eventi del form delle impostazioni
            SetFormControls();
            InitializeFormEvents();
        }

        // Metodo che crea e popola la lista delle difficoltà
        private void CreateDifficultiesList()
        {
            // nome associato ad una difficoltà di gioco
            string difficultyName;
            // lunghezza, altezza e numero di mine associate ad una difficoltà di gioco
            int length, height, mines;

            // viene creata la lista delle difficoltà di gioco
            _difficultiesList = new List<Tuple<string, int, int, int>>();

            // viene momentaneamente inserita la difficoltà 'personalizzata' al primo posto della lista
            difficultyName = CustomDifficulty;
            length = Minefield.MinSide;
            height = Minefield.MinSide;
            mines = Minefield.MinMines;
            _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));

            // si inseriscono le altre difficoltà di gioco nella lista
            #region Difficoltà di gioco aggiuntive

            difficultyName = "Banale";
            length = 9;
            height = 9;
            mines = 10;
            if (DifficultyIsCorrect(difficultyName, length, height, mines))
                _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));

            difficultyName = "Molto facile";
            length = 12;
            height = 12;
            mines = 20;
            if (DifficultyIsCorrect(difficultyName, length, height, mines))
                _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));

            difficultyName = "Facile";
            length = 16;
            height = 16;
            mines = 40;
            if (DifficultyIsCorrect(difficultyName, length, height, mines))
                _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));

            difficultyName = "Intermedia";
            length = 18;
            height = 18;
            mines = 65;
            if (DifficultyIsCorrect(difficultyName, length, height, mines))
                _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));

            difficultyName = "Difficile";
            length = 20;
            height = 20;
            mines = 100;
            if (DifficultyIsCorrect(difficultyName, length, height, mines))
                _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));

            difficultyName = "Molto difficile";
            length = 22;
            height = 22;
            mines = 135;
            if (DifficultyIsCorrect(difficultyName, length, height, mines))
                _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));

            difficultyName = "Estrema";
            length = 25;
            height = 25;
            mines = 185;
            if (DifficultyIsCorrect(difficultyName, length, height, mines))
                _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));

            #endregion

            // si sposta la difficoltà 'personalizzata' dal primo all'ultimo posto della lista
            difficultyName = CustomDifficulty;
            length = Minefield.MinSide;
            height = Minefield.MinSide;
            mines = Minefield.MinMines;
            _difficultiesList.Remove(new Tuple<string, int, int, int>(difficultyName, length, height, mines));
            _difficultiesList.Add(new Tuple<string, int, int, int>(difficultyName, length, height, mines));
        }

        // Metodo che verifica la correttezza dei dati da inserire nella lista delle difficoltà
        private bool DifficultyIsCorrect(string difficultyName, int length, int height, int mines)
        {
            // se almeno una caratteristica del campo minato è errata si comunica che la difficoltà non è corretta
            if (length < Minefield.MinSide || length > Minefield.MaxSide ||
                height < Minefield.MinSide || height > Minefield.MaxSide ||
                mines < Minefield.MinSide || mines > Minefield.GetMaxMines(length, height))
                return false;

            // se nella lista è già presente una difficoltà con lo stesso nome oppure con i medesimi valori
            // dei parametri del campo minato allora si comunica che la difficoltà non è corretta
            foreach (Tuple<string, int, int, int> entry in _difficultiesList)
                if (difficultyName == entry.Item1 ||
                   (length == entry.Item2 && height == entry.Item3 && mines == entry.Item4))
                    return false;

            // se i controlli precendenti sono stati superati si comunica che la difficoltà è corretta
            return true;
        }

        // Metodo che configura i controlli del form delle impostazioni
        private void SetFormControls()
        {
            // si inseriscono i nomi univoci dei tipi di campo minato nell'appropriata casella combinata
            _settings.MinefieldComboBox.Items.AddRange(_helper.GetMinefieldsNames());
            // si inseriscono i nomi delle modalità di gioco nell'appropriata casella combinata
            _settings.ModalityComboBox.Items.AddRange(Enum.GetNames(typeof(MinesweeperGame.GameModality)));
            // si inserisce il nome di ogni difficoltà di gioco nell'appropriata casella combinata
            foreach (Tuple<string, int, int, int> entry in _difficultiesList)
                _settings.DifficultyComboBox.Items.Add(entry.Item1);

            // viene impostato il valore minino e massimo assumibile dai controlli di scorrimento
            _settings.MinefieldLength.Minimum = Minefield.MinSide;
            _settings.MinefieldLength.Maximum = Minefield.MaxSide;
            _settings.MinefieldHeight.Minimum = Minefield.MinSide;
            _settings.MinefieldHeight.Maximum = Minefield.MaxSide;
            _settings.MinefieldMines.Minimum = Minefield.MinMines;
            // si impostano opportunamente le etichette informative associate ai controlli di scorrimento
            _settings.MinefieldLengthMinMaxInfo = "[ Min: " + Minefield.MinSide + " - Max: " + Minefield.MaxSide + " ]";
            _settings.MinefieldHeightMinMaxInfo = "[ Min: " + Minefield.MinSide + " - Max: " + Minefield.MaxSide + " ]";
        }

        // Metodo che inizializza gli eventi del form delle impostazioni
        private void InitializeFormEvents()
        {
            // si crea un gestore eventi per la selezione di una tipologia di campo minato
            _settings.MinefieldComboBox.SelectedValueChanged += UpdateMinefieldInfo;
            // si crea un gestore eventi per la selezione di una modalità di gioco
            _settings.ModalityComboBox.SelectedValueChanged += UpdateModalityInfo;
            // si crea un gestore eventi per la selezione di una difficoltà di gioco
            _settings.DifficultyComboBox.SelectedValueChanged += UpdateMinefieldCharacteristics;

            // si crea un gestore eventi per la variazione di lunghezza e altezza nel form
            _settings.MinefieldLength.ValueChanged += UpdateMaxMines;
            _settings.MinefieldHeight.ValueChanged += UpdateMaxMines;
            // si creano dei gestori eventi per il click dei bottoni
            _settings.ConfirmButtonClick += OkButtonClicked;
            _settings.RefuteButtonClick += CancelButtonClicked;
        }

        // Metodo che aggiorna la descrizione del tipo di campo minato selezionato dall'utente
        private void UpdateMinefieldInfo(object sender, EventArgs e)
        {
            // si acquisisce la descrizione del tipo di campo minato selezionato dall'utente
            string minefieldInfo = _helper.GetMinefieldInfo(_settings.MinefieldComboBox.Text);
            // si aggiorna l'etichetta informativa che descrive il tipo di campo minato selezionato 
            if (minefieldInfo != null)
                _settings.MinefieldInfo = minefieldInfo;
            else
                _settings.MinefieldInfo = "* Descrizione non disponibile *";
        }

        // Metodo che aggiorna la descrizione della modalità di gioco selezionata dall'utente
        private void UpdateModalityInfo(object sender, EventArgs e)
        {
            // si acquisisce la modalità di gioco selezionata dall'utente
            string stringModality = _settings.ModalityComboBox.Text;
            Object objectModality = Enum.Parse(typeof(MinesweeperGame.GameModality), stringModality);
            MinesweeperGame.GameModality selectedModality = (MinesweeperGame.GameModality)objectModality;

            // si acquisisce la descrizione della modalità di gioco selezionata
            string modalityInfo = MinesweeperGame.DescribeModality(selectedModality);
            // si aggiorna l'etichetta informativa che descrive la modalità di gioco selezionata
            if (modalityInfo != null)
                _settings.ModalityInfo = modalityInfo;
            else
                _settings.ModalityInfo = "* Descrizione non disponibile *";
        }

        // Metodo che aggiorna i controlli di scorrimento e le relative etichette
        private void UpdateMinefieldCharacteristics(object sender, EventArgs e)
        {
            // si acquisisce il nome della difficoltà di gioco selezionata dall'utente
            string selectedDifficulty = _settings.DifficultyComboBox.Text;

            // se la difficoltà selezionata corrisponde a quella 'personalizzata' allora si rendono
            // interagibili i controlli di scorrimento e visibili le correlate etichette informative
            if (selectedDifficulty == CustomDifficulty)
            {
                _settings.MinefieldLength.Enabled = true;
                _settings.MinefieldHeight.Enabled = true;
                _settings.MinefieldMines.Enabled = true;
                _settings.MinefieldLengthMinMaxInfoVisibility = true;
                _settings.MinefieldHeightMinMaxInfoVisibility = true;
                _settings.MinefieldMinesMinMaxInfoVisibility = true;
            }
            else
            {
                _settings.MinefieldLength.Enabled = false;
                _settings.MinefieldHeight.Enabled = false;
                _settings.MinefieldMines.Enabled = false;
                _settings.MinefieldLengthMinMaxInfoVisibility = false;
                _settings.MinefieldHeightMinMaxInfoVisibility = false;
                _settings.MinefieldMinesMinMaxInfoVisibility = false;
            }

            // si impostano i nuovi valori dei controlli di scorrimento
            foreach (Tuple<string, int, int, int> entry in _difficultiesList)
                if (selectedDifficulty.Equals(entry.Item1))
                {
                    _settings.MinefieldLength.Value = entry.Item2;
                    _settings.MinefieldHeight.Value = entry.Item3;
                    _settings.MinefieldMines.Value = entry.Item4;
                    break;
                }
        }

        // Metodo che aggiorna il massimo numero di mine inseribili dall'utente nel controllo di scorrimento
        private void UpdateMaxMines(object sender, EventArgs e)
        {
            // si acquisiscono lunghezza e altezza attuali del campo minato
            int actualLength = (int)_settings.MinefieldLength.Value;
            int actualHeight = (int)_settings.MinefieldHeight.Value;
            // si calcola il massimo numero di mine rispetto alle dimensioni indicate del campo minato
            int maxMine = Minefield.GetMaxMines(actualLength, actualHeight);
            // si aggiorna il massimo numero di mine inseribili dall'utente
            _settings.MinefieldMines.Maximum = maxMine;

            // si aggiorna l'etichetta informativa correlata al numero di mine inseribili dall'utente
            int minMine = (int)_settings.MinefieldMines.Minimum;
            _settings.MinefieldMinesMinMaxInfo = "[ Min: " + minMine + " - Max: " + maxMine + " ]";
        }

        // Metodo eseguito quando si clicca sul bottone "OK"
        private void OkButtonClicked(object sender, EventArgs e)
        {
            // si segnala che la configurazione di gioco è stata impostata dall'utente
            if (ConfigurationEstablished != null)
                try { ConfigurationEstablished(this, EventArgs.Empty); } catch { }

            // si chiude il form
            _settings.Close();
        }

        // Metodo eseguito quando si clicca sul bottone "Annulla"
        private void CancelButtonClicked(object sender, EventArgs e)
        {
            // si chiude il form
            _settings.Close();
        }

        // Metodo che crea e restituisce un memento delle impostazioni
        internal IMemento SaveState()
        {
            // lista contenente le opzioni selezionate dall'utente
            List<int> settingsOptions = new List<int>
            {
                // si memorizzano gli indici degli elementi selezionati nelle caselle combinate
                _settings.MinefieldComboBox.SelectedIndex,
                _settings.ModalityComboBox.SelectedIndex,
                _settings.DifficultyComboBox.SelectedIndex,
                // si memorizzano i valori dei controlli di scorrimento
                (int)_settings.MinefieldLength.Value,
                (int)_settings.MinefieldHeight.Value,
                (int)_settings.MinefieldMines.Value
            };

            return new SettingsMemento(settingsOptions.ToArray());
        }

        // Metodo che ripristina le opzioni selezionate in precedenza dall'utente
        internal void RestoreState(IMemento memento)
        {
            if (memento != null)
                try
                {
                    // si converte il memento nel tipo opportuno per poi acquisirne lo stato memorizzato
                    SettingsMemento settingsMemento = (SettingsMemento)memento;
                    int[] settingsOptions = settingsMemento.GetState();

                    // viene selezionato l'opportuno elemento nelle caselle combinate
                    _settings.MinefieldComboBox.SelectedIndex = settingsOptions[0];
                    _settings.ModalityComboBox.SelectedIndex = settingsOptions[1];
                    _settings.DifficultyComboBox.SelectedIndex = settingsOptions[2];

                    // se il nome della difficoltà appena impostata corrisponde a quella 'personalizzata'
                    // allora si impostano i valori dei controlli di scorrimento
                    if (CustomDifficulty.Equals(_settings.DifficultyComboBox.Text))
                    {
                        _settings.MinefieldLength.Value = settingsOptions[3];
                        _settings.MinefieldHeight.Value = settingsOptions[4];
                        _settings.MinefieldMines.Value = settingsOptions[5];
                    }
                }
                catch
                {
                    // in caso di errore viene selezionato il primo elemento delle caselle combinate
                    _settings.MinefieldComboBox.SelectedIndex = 0;
                    _settings.ModalityComboBox.SelectedIndex = 0;
                    _settings.DifficultyComboBox.SelectedIndex = 0;
                }
        }

        // Metodo che consente di visualizzare il form delle impostazioni
        public void ShowForm()
        {
            // se non è stato selezionato alcun elemento nelle caselle combinate allora si seleziona il primo
            if (_settings.MinefieldComboBox.SelectedIndex == -1)
                _settings.MinefieldComboBox.SelectedIndex = 0;
            if (_settings.ModalityComboBox.SelectedIndex == -1)
                _settings.ModalityComboBox.SelectedIndex = 0;
            if (_settings.DifficultyComboBox.SelectedIndex == -1)
                _settings.DifficultyComboBox.SelectedIndex = 0;

            // viene visualizzato il form come finestra di dialogo modale
            _settings.ShowDialog();
        }

        // Metodo che restituisce le configurazioni di gioco impostate dall'utente
        public IMinefieldCreator GetGameConfiguration(out int length, out int height, out int mines,
                                                      out MinesweeperGame.GameModality modality)
        {
            // si acquisiscono le caratteristiche del campo minato dal form delle impostazioni
            length = (int)_settings.MinefieldLength.Value;
            height = (int)_settings.MinefieldHeight.Value;
            mines = (int)_settings.MinefieldMines.Value;

            // si acquisisce la modalità di gioco dal form delle impostazioni
            string stringModality = _settings.ModalityComboBox.Text;
            object objectModality = Enum.Parse(typeof(MinesweeperGame.GameModality), stringModality);
            modality = (MinesweeperGame.GameModality)objectModality;

            // viene restituito l'opportuno creatore di campi minati
            return _helper.GetMinefieldCreator(_settings.MinefieldComboBox.Text);
        }
    }
}
