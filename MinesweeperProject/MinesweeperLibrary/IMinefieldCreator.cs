using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary
{
    // Interfaccia di un creatore di campi minati
    public interface IMinefieldCreator
    {
        // Metodo che restituisce un'istanza di un campo minato
        Minefield CreateMinefield(int length, int height, int mines);
    }
}
