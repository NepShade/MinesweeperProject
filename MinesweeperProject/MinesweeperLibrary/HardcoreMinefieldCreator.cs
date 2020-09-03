namespace MinesweeperLibrary
{
    // Classe rappresentante un creatore di campi minati 'hardcore'
    public class HardcoreMinefieldCreator : IMinefieldCreator
    {
        // Metodo che restituisce il nome identificativo associato a un campo minato 'hardcore'
        public string GetMinefieldName()
        {
            return "Hardcore";
        }

        // Metodo che restituisce una breve descrizione di un campo minato 'hardcore'
        public string GetMinefieldInfo()
        {
            return "La prima zona scoperta può essere minata";
        }

        // Metodo che restituisce un campo minato 'hardcore' configurato con caratteristiche prestabilite
        public Minefield CreateMinefield()
        {
            return new HardcoreMinefield(Minefield.MinSide, Minefield.MinSide, Minefield.MinMines);
        }

        // Metodo che restituisce un campo minato 'hardcore' configurato con le caratteristiche specificate
        public Minefield CreateMinefield(int length, int height, int mines)
        {
            return new HardcoreMinefield(length, height, mines);
        }
    }
}
