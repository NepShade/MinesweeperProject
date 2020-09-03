using System.Windows.Forms;

namespace MinesweeperGUI
{
    // Classe rappresentante il form delle informazioni sulla partita
    public partial class InfoView : Form
    {
        // Costruttore che inizializza i componenti del form
        public InfoView()
        {
            InitializeComponent();
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che dichiara il tipo di campo minato
        public string MinefieldName
        {
            get { return _minefieldTypeNameLabel.Text; }
            set { _minefieldTypeNameLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che dichiara la lunghezza del campo minato
        public string MinefieldLength
        {
            get { return _lengthValueLabel.Text; }
            set { _lengthValueLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che dichiara l'altezza del campo minato
        public string MinefieldHeight
        {
            get { return _heightValueLabel.Text; }
            set { _heightValueLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che dichiara il numero di mine del campo minato
        public string MinefieldMines
        {
            get { return _minesValueLabel.Text; }
            set { _minesValueLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che dichiara la modalità di gioco
        public string ModalityName
        {
            get { return _modalityNameLabel.Text; }
            set { _modalityNameLabel.Text = value; }
        }

        // Proprietà che restituisce e imposta il testo dell'etichetta che dichiara i tentativi della partita
        public string Attempts
        {
            get { return _attemptsValueLabel.Text; }
            set { _attemptsValueLabel.Text = value; }
        }
    }
}
