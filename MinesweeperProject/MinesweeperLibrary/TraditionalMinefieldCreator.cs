using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary
{
    // Classe rappresentante un creatore di campi minati 'tradizionali'
    public class TraditionalMinefieldCreator : IMinefieldCreator
    {
        // Metodo che restituisce il nome identificativo associato a un campo minato 'tradizionale'
        public string GetMinefieldName()
        {
            return "Tradizionale";
        }

        // Metodo che restituisce una breve descrizione di un campo minato 'tradizionale'
        public string GetMinefieldInfo()
        {
            return "La prima zona scoperta è sempre sicura";
        }

        // Metodo che restituisce un campo minato 'tradizionale' configurato con caratteristiche prestabilite
        public Minefield CreateMinefield()
        {
            return new TraditionalMinefield(Minefield.MinSide, Minefield.MinSide, Minefield.MinMines);
        }

        // Metodo che restituisce un campo minato 'tradizionale' configurato con le caratteristiche specificate
        public Minefield CreateMinefield(int length, int height, int mines)
        {
            return new TraditionalMinefield(length, height, mines);
        }
    }
}
