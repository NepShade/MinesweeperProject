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
    // Classe rappresentante il form delle regole di gioco
    public partial class RulesView : Form
    {
        // Attributi della classe
        // immagine contenente le regole di gioco
        private readonly PictureBox _rulesImage;

        // Costruttore che inizializza i componenti del form
        public RulesView()
        {
            InitializeComponent();

            // si crea e si inizializza l'immagine per poi aggiungerla alla raccolta di controlli del form
            _rulesImage = new PictureBox
            {
                Location = new Point(0, 0),
                Visible = false
            };
            Controls.Add(_rulesImage);
        }

        // Metodo che visualizza nel form una nuova immagine delle regole di gioco
        public void SetRulesImage(Image rulesImage)
        {
            // si ridimensiona il form in base alle dimensioni dell'immagine acquisita
            ClientSize = new Size(rulesImage.Width, rulesImage.Height);
            // si memorizza l'immagine e la si rende visibile all'interno del form
            _rulesImage.Width = rulesImage.Width;
            _rulesImage.Height = rulesImage.Height;
            _rulesImage.Image = rulesImage;
            _rulesImage.Visible = true;
        }
    }
}
