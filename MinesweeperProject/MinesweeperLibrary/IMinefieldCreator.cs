namespace MinesweeperLibrary
{
    // Interfaccia di un creatore di campi minati
    public interface IMinefieldCreator
    {
        // Metodo che restituisce il nome identificativo associato al campo minato
        string GetMinefieldName();

        // Metodo che restituisce una breve descrizione del campo minato
        string GetMinefieldInfo();

        // Metodo che restituisce un campo minato configurato con caratteristiche prestabilite
        Minefield CreateMinefield();

        // Metodo che restituisce un campo minato configurato con le caratteristiche specificate
        Minefield CreateMinefield(int length, int height, int mines);
    }
}
