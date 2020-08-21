using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary
{
    // Classe rappresentante un campo minato 'hardcore', ossia un campo minato in grado
    // di generare mine anche all'interno della regione iniziale scoperta dall'utente
    public class HardcoreMinefield : Minefield
    {
        // Costruttore che genera un campo minato 'hardcore' dalle caratteristiche specificate
        public HardcoreMinefield(int length, int height, int mines) : base(length, height, mines)
        {
            // generazione delle mine e configurazione delle zone presenti nel campo minato
            CreateNewMinefield();
        }

        // Metodo che genera casualmente le coordinate delle mine all'interno del campo minato
        protected override List<Tuple<int, int>> GenerateMinesCoordinates(int minesToGenerate)
        {
            // insieme di elementi non uguali contenente la posizione delle mine
            HashSet<int> minesPosition = new HashSet<int>();
            // generatore di numeri pseudo-casuali
            Random positionsGenerator = new Random();
            // si generano posizioni tutte diverse fintanto che non si raggiunge il numero di mine da generare
            while (minesPosition.Count < minesToGenerate)
                minesPosition.Add(positionsGenerator.Next(TotalZones));

            // lista contenente le coordinate delle mine
            List<Tuple<int, int>> minesCoordinates = new List<Tuple<int, int>>();
            // si converte la posizione di ciascuna mina in coordinate per poi aggiungerle nella lista
            foreach (int position in minesPosition)
                minesCoordinates.Add(ConvertToCoordinates(position));

            return minesCoordinates;
        }
    }
}
