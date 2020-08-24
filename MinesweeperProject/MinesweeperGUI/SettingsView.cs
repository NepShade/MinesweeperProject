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
    // Classe rappresentante il form delle impostazioni di gioco
    public partial class SettingsView : Form
    {
        // Costruttore che inizializza i componenti del form
        public SettingsView()
        {
            InitializeComponent();
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
