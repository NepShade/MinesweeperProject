using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary
{
    // Classe rappresentante un creatore di campi minati 'tradizionali'
    public class TraditionalMinefieldCreator : IMinefieldCreator
    {
        // Metodo che restituisce un'istanza di un campo minato 'tradizionale'
        public Minefield CreateMinefield(int length, int height, int mines)
        {
            return new TraditionalMinefield(length, height, mines);
        }
    }
}
