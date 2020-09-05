using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    // Classe rappresentante il form delle impostazioni di gioco
    public partial class SettingsView : Form
    {
        // Costruttore che inizializza i componenti del form
        public SettingsView()
        {
            InitializeComponent();
        }

        // Metodo che crea e restituisce un memento delle opzioni selezionate
        internal IMemento SaveState()
        {
            // lista contenente le opzioni selezionate
            List<int> settingsOptions = new List<int>
            {
                // si memorizzano gli indici degli elementi selezionati nelle caselle combinate
                MinefieldComboBox.SelectedIndex,
                ModalityComboBox.SelectedIndex,
                DifficultyComboBox.SelectedIndex,
                // si memorizzano i valori dei controlli di scorrimento
                (int)MinefieldLength.Value,
                (int)MinefieldHeight.Value,
                (int)MinefieldMines.Value
            };

            return new SettingsMemento(settingsOptions.ToArray());
        }

        // Metodo che ripristina le opzioni selezionate in precedenza
        internal void RestoreState(IMemento memento)
        {
            if (memento != null)
                try
                {
                    // si converte il memento nel tipo opportuno per poi acquisirne lo stato memorizzato
                    SettingsMemento settingsMemento = (SettingsMemento)memento;
                    int[] settingsOptions = settingsMemento.GetState();

                    // si seleziona l'opportuno elemento nelle caselle combinate
                    MinefieldComboBox.SelectedIndex = settingsOptions[0];
                    ModalityComboBox.SelectedIndex = settingsOptions[1];
                    DifficultyComboBox.SelectedIndex = settingsOptions[2];
                    // si impostano i valori dei controlli di scorrimento
                    MinefieldLength.Value = settingsOptions[3];
                    MinefieldHeight.Value = settingsOptions[4];
                    MinefieldMines.Value = settingsOptions[5];
                }
                catch
                {
                    // in caso di errore viene selezionato il primo elemento delle caselle combinate
                    MinefieldComboBox.SelectedIndex = 0;
                    ModalityComboBox.SelectedIndex = 0;
                    DifficultyComboBox.SelectedIndex = 0;
                }
        }

        // Proprietà che restituisce la casella combinata dei tipi di campo minato selezionabili
        public ComboBox MinefieldComboBox
        {
            get { return _minefieldComboBox; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che descrive il tipo di campo minato
        public string MinefieldInfo
        {
            get { return _minefieldInfoLabel.Text; }
            set { _minefieldInfoLabel.Text = value; }
        }

        // Proprietà che restituisce la casella combinata delle modalità di gioco selezionabili
        public ComboBox ModalityComboBox
        {
            get { return _modalityComboBox; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che descrive la modalità di gioco
        public string ModalityInfo
        {
            get { return _modalityInfoLabel.Text; }
            set { _modalityInfoLabel.Text = value; }
        }

        // Proprietà che restituisce la casella combinata delle difficoltà di gioco selezionabili
        public ComboBox DifficultyComboBox
        {
            get { return _difficultyComboBox; }
        }

        // Proprietà che restituisce il controllo di scorrimento rappresentante la lunghezza del campo minato
        public NumericUpDown MinefieldLength
        {
            get { return _lengthNumericUpDown; }
        }

        // Proprietà che restituisce il controllo di scorrimento rappresentante l'altezza del campo minato
        public NumericUpDown MinefieldHeight
        {
            get { return _heightNumericUpDown; }
        }

        // Proprietà che restituisce il controllo di scorrimento rappresentante il numero di mine del campo minato
        public NumericUpDown MinefieldMines
        {
            get { return _minesNumericUpDown; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che descrive il minimo e il massimo
        // valore assumibile dal controllo di scorrimento relativo alla lunghezza del campo minato
        public string MinefieldLengthMinMaxInfo
        {
            get { return _minMaxLengthLabel.Text; }
            set { _minMaxLengthLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta la visibilità dell'etichetta che descrive il minimo e il massimo
        // valore assumibile dal controllo di scorrimento relativo alla lunghezza del campo minato
        public bool MinefieldLengthMinMaxInfoVisibility
        {
            get { return _minMaxLengthLabel.Visible; }
            set { _minMaxLengthLabel.Visible = value; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che descrive il minimo e il massimo
        // valore assumibile dal controllo di scorrimento relativo all'altezza del campo minato
        public string MinefieldHeightMinMaxInfo
        {
            get { return _minMaxHeightLabel.Text; }
            set { _minMaxHeightLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta la visibilità dell'etichetta che descrive il minimo e il massimo
        // valore assumibile dal controllo di scorrimento relativo all'altezza del campo minato
        public bool MinefieldHeightMinMaxInfoVisibility
        {
            get { return _minMaxHeightLabel.Visible; }
            set { _minMaxHeightLabel.Visible = value; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che descrive il minimo e il massimo
        // valore assumibile dal controllo di scorrimento relativo al numero di mine del campo minato
        public string MinefieldMinesMinMaxInfo
        {
            get { return _minMaxMinesLabel.Text; }
            set { _minMaxMinesLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta la visibilità dell'etichetta che descrive il minimo e il massimo
        // valore assumibile dal controllo di scorrimento relativo al numero di mine del campo minato
        public bool MinefieldMinesMinMaxInfoVisibility
        {
            get { return _minMaxMinesLabel.Visible; }
            set { _minMaxMinesLabel.Visible = value; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al click del 'pulsante di conferma'
        public event EventHandler ConfirmButtonClick
        {
            add { _okButton.Click += value; }
            remove { _okButton.Click -= value; }
        }

        // Proprietà che aggiunge e rimuove un gestore evento al click del 'pulsante di rifiuto'
        public event EventHandler RefuteButtonClick
        {
            add { _cancelButton.Click += value; }
            remove { _cancelButton.Click -= value; }
        }
    }
}
