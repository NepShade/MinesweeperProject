using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary
{
    // Classe rappresentante un creatore di campi minati 'hardcore'
    public class HardcoreMinefieldCreator : IMinefieldCreator
    {
        // Metodo che restituisce un'istanza di un campo minato 'hardcore'
        public Minefield CreateMinefield(int length, int height, int mines)
        {
            return new HardcoreMinefield(length, height, mines);
        }
    }
}
