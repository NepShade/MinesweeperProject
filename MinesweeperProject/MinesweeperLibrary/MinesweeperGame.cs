using System;

namespace MinesweeperLibrary
{
    // Classe rappresentante la logica di gioco dietro una partita a "Campo Minato"
    public class MinesweeperGame
    {
        // modalità di gioco che determinano il numero di tentativi disponibili durante una partita
        public enum GameModality { Classica, Agevolata, Semplificata, Sicura };

        // Eventi della classe
        // evento che segnala che una zona del campo minato è stata scoperta
        public event EventHandler<ZoneEventArgs> ZoneUncovered;

        // Attributi della classe
        // numero di tentativi associati alla modalità classica e agevolata
        private const int AttemptsClassica = 1;
        private const int AttemptsAgevolata = 3;
        // rapporto tra mine e tentativi stabilito per la modalità semplificata
        private const double AttemptsRatioSemplificata = 0.1;
        // campo minato a cui la partita fa riferimento
        private Minefield _minefield;
        // modalità di gioco selezionata per la partita
        private GameModality _modality;
        // variabile che indica se almeno una zona del campo minato è stata scoperta
        private bool _minefieldZoneUncovered;
        // numero di zone sicure scoperte del campo minato
        private int _uncoveredSafeZones;
        // numero di tentativi disponibili prima di perdere la partita
        private int? _attempts;
        // variabile che indica se la partita è stata vinta
        private bool? _victory;

        // Costruttore che genera una partita rispetto al campo minato fornito e alla modalità di gioco scelta
        public MinesweeperGame(Minefield minefield, GameModality modality)
        {
            NewGame(minefield, modality);
        }

        // Metodo che crea una nuova partita con le impostazioni specificate
        public void NewGame(Minefield minefield, GameModality modality)
        {
            if (minefield == null)
                throw new ArgumentNullException
                    ("Riferimento nullo al campo minato!");

            if (!minefield.AreZonesCoveredAndUnflagged())
                throw new ArgumentException
                    ("Il campo minato ha uno stato iniziale invalido: sono presenti zone scoperte e/o contrassegnate!");

            // acquisizione del campo minato e della modalità di gioco
            _minefield = minefield;
            _modality = modality;
            // creazione di una nuova partita
            NewGame();
        }

        // Metodo che crea una nuova partita con le medesime impostazioni memorizzate
        public void NewGame()
        {
            // si determina il numero di tentativi disponibili per la partita
            _attempts = SetAttempts(_modality);
            // si annota che nessuna zona del campo minato è stata ancora scoperta
            _minefieldZoneUncovered = false;
            // si azzera il numero di zone sicure scoperte
            _uncoveredSafeZones = 0;
            // si imposta come "non definito" l'esito della partita
            _victory = null;
        }

        // Metodo che determina il numero di tentativi disponibili per la partita
        private int? SetAttempts(GameModality modality)
        {
            // numero totale di tentativi disponibili per la partita
            int? totalAttempts;

            // a seconda della modalità scelta si determina il numero totale di tentativi disponibili
            if (modality == GameModality.Classica)
            {
                totalAttempts = AttemptsClassica;
            }
            else if (modality == GameModality.Agevolata)
            {
                totalAttempts = AttemptsAgevolata;
            }
            else if (modality == GameModality.Semplificata)
            {
                totalAttempts = (int)(_minefield.Mines * AttemptsRatioSemplificata);

                // se il numero di tentativi appena calcolato è inferiore al numero di tentativi associato alla
                // modalità 'agevolata' allora si utilizza quest'ultimo numero per i tentativi disponibili
                if (totalAttempts < AttemptsAgevolata)
                    totalAttempts = AttemptsAgevolata;
            }
            else if (modality == GameModality.Sicura)
            {
                totalAttempts = null;
            }
            else
            {
                throw new ArgumentOutOfRangeException
                    ("Selezionata modalità di gioco sconosciuta!");
            }

            return totalAttempts;
        }

        // Metodo che prova a contrassegnare o "ripulire" la zona dalle coordinate specificate
        public bool FlagUnflagZone(int x, int y, out bool? zoneFlagged)
        {
            // se la partita non è terminata e almeno una zona del campo minato è stata scoperta...
            if (!EndGame && _minefieldZoneUncovered)
            {
                // ...allora si prova a contrassegnare o "ripulire" la zona...
                return _minefield.FlagUnflagZone(x, y, out zoneFlagged);
            }
            else
            {
                // ...altrimenti si comunica che l'operazione è fallita
                zoneFlagged = null;
                return false;
            }
        }

        // Metodo che prova a scoprire la zona dalle coordinate specificate
        public bool UncoverZone(int x, int y)
        {
            // se la partita non è terminata...
            if (!EndGame)
            {
                // ...allora si prova a scoprire la zona dalle coordinate specificate
                if (_minefield.UncoverZone(x, y, out bool? zoneMined, out int? adjacentMines))
                {
                    // si annota che almeno una zona del campo minato è stata scoperta
                    _minefieldZoneUncovered = true;

                    // si verifica se la zona è minata o sicura per stabilire quali azioni intraprendere
                    if (zoneMined == true)
                    {
                        UncoveredMinedZone();
                    }
                    else
                    {
                        // si aumenta il numero di zone sicure scoperte e se esse combaciano con il numero di
                        // zone sicure presenti nel campo minato allora si termina la partita con una vittoria
                        if (++_uncoveredSafeZones == _minefield.SafeZones)
                            _victory = true;

                        // se la partita non è terminata si valuta il numero di mine rilevate attorno
                        // alla zona sicura scoperta per stabilire quali azioni intraprendere
                        if (!EndGame)
                        {
                            if (adjacentMines == 0)
                                UncoveredEmptyZone(x, y);
                            else
                                UncoveredSafeZone(x, y);
                        }
                    }

                    // si segnala che una zona del campo minato è stata scoperta
                    bool covered = false;
                    bool flagged = false;
                    bool mined = (bool)zoneMined;
                    int mines = (int)adjacentMines;
                    if (ZoneUncovered != null)
                        try { ZoneUncovered(this, new ZoneEventArgs(x, y, covered, flagged, mined, mines)); } catch { }

                    // si comunica che l'operazione è riuscita
                    return true;
                }
            }

            // si comunica che l'operazione è fallita
            return false;
        }

        // Metodo che riassume le conseguenze derivanti dallo scoprire una zona minata
        private void UncoveredMinedZone()
        {
            // se il numero di tentativi disponibili per la partita è definito...
            if (_attempts != null)
            {
                // ...allora se ne riduce il numero, dopodiché se esso è minore o
                // uguale a zero allora si termina la partita con una sconfitta
                if (--_attempts <= 0)
                    _victory = false;
            }
        }

        // Metodo che riassume le conseguenze derivanti dallo scoprire una zona sicura e senza mine attorno ad essa
        private void UncoveredEmptyZone(int x, int y)
        {
            // si prova a scoprire la zona a Nord-Ovest rispetto alla zona dalle coordinate specificate
            UncoverZone(x - 1, y - 1);

            // si prova a scoprire la zona a Nord rispetto alla zona dalle coordinate specificate
            UncoverZone(x, y - 1);

            // si prova a scoprire la zona a Nord-Est rispetto alla zona dalle coordinate specificate
            UncoverZone(x + 1, y - 1);

            // si prova a scoprire la zona a Ovest rispetto alla zona dalle coordinate specificate
            UncoverZone(x - 1, y);

            // si prova a scoprire la zona a Est rispetto alla zona dalle coordinate specificate
            UncoverZone(x + 1, y);

            // si prova a scoprire la zona a Sud-Ovest rispetto alla zona dalle coordinate specificate
            UncoverZone(x - 1, y + 1);

            // si prova a scoprire la zona a Sud rispetto alla zona dalle coordinate specificate
            UncoverZone(x, y + 1);

            // si prova a scoprire la zona a Sud-Est rispetto alla zona dalle coordinate specificate
            UncoverZone(x + 1, y + 1);
        }

        // Metodo che riassume le conseguenze derivanti dallo scoprire una zona sicura e con mine attorno ad essa
        private void UncoveredSafeZone(int x, int y)
        {
            // si controlla se le zone contrassegnate attorno alla zona scoperta sono tutte minate
            if (FlaggedEqualsMined(x, y, out int UncoveredOrFlaggedMinedZones))
            {
                // se il numero di zone minate (scoperte oppure contrassegnate) coincide
                // con il numero di mine rilevate attorno alla zona scoperta...
                if (_minefield.GetZoneAdjacentMines(x, y, out int? adjacentMines))
                    if (UncoveredOrFlaggedMinedZones == adjacentMines)
                    {
                        // ...allora si scoprono automaticamente tutte le rimanenti zone adiacenti alla
                        // zona specificata che ancora sono coperte e non sono contrassegnata come minate
                        UncoveredEmptyZone(x, y);
                    }
            }
        }

        // Metodo che indica se attorno a una zona sono state contrassegnate solamente zone minate
        private bool FlaggedEqualsMined(int x, int y, out int UncoveredOrFlaggedMinedZones)
        {
            // numero di zone adiacenti alla zona specificata che sono contrassegnate
            int flaggedZones = 0;
            // numero di zone adiacenti alla zona specificata che sono minate e contrassegnate
            int flaggedMinedZones = 0;
            // numero di zone adiacenti alla zona specificata che sono minate e scoperte
            int uncoveredMinedZones = 0;
            // variabili che indicano se la zona esaminata è coperta, contrassegnata, minata
            bool? zoneCovered, zoneFlagged, zoneMined;

            // si controlla la zona a Nord-Ovest rispetto alla zona specificata
            if (_minefield.IsZoneFlagged(x - 1, y - 1, out zoneFlagged))
            {
                // se tale zona è contrassegnata si aumenta il numero di zone contrassegnate come minate
                if (zoneFlagged == true)
                {
                    flaggedZones++;
                    // se tale zona è anche minata si aumenta il numero di zone contrassegnate e minate
                    if (_minefield.IsZoneMined(x - 1, y - 1, out zoneMined) && zoneMined == true)
                        flaggedMinedZones++;
                }
                // se tale zona è scoperta e minata si aumenta il numero di zone scoperte e minate
                else if (_minefield.IsZoneCovered(x - 1, y - 1, out zoneCovered) && zoneCovered == false &&
                         _minefield.IsZoneMined(x - 1, y - 1, out zoneMined) && zoneMined == true)
                    uncoveredMinedZones++;
            }

            // si controlla la zona a Nord rispetto alla zona specificata
            if (_minefield.IsZoneFlagged(x, y - 1, out zoneFlagged))
            {
                // se tale zona è contrassegnata si aumenta il numero di zone contrassegnate come minate
                if (zoneFlagged == true)
                {
                    flaggedZones++;
                    // se tale zona è anche minata si aumenta il numero di zone contrassegnate e minate
                    if (_minefield.IsZoneMined(x, y - 1, out zoneMined) && zoneMined == true)
                        flaggedMinedZones++;
                }
                // se tale zona è scoperta e minata si aumenta il numero di zone scoperte e minate
                else if (_minefield.IsZoneCovered(x, y - 1, out zoneCovered) && zoneCovered == false &&
                         _minefield.IsZoneMined(x, y - 1, out zoneMined) && zoneMined == true)
                    uncoveredMinedZones++;
            }

            // si controlla la zona a Nord-Est rispetto alla zona specificata
            if (_minefield.IsZoneFlagged(x + 1, y - 1, out zoneFlagged))
            {
                // se tale zona è contrassegnata si aumenta il numero di zone contrassegnate come minate
                if (zoneFlagged == true)
                {
                    flaggedZones++;
                    // se tale zona è anche minata si aumenta il numero di zone contrassegnate e minate
                    if (_minefield.IsZoneMined(x + 1, y - 1, out zoneMined) && zoneMined == true)
                        flaggedMinedZones++;
                }
                // se tale zona è scoperta e minata si aumenta il numero di zone scoperte e minate
                else if (_minefield.IsZoneCovered(x + 1, y - 1, out zoneCovered) && zoneCovered == false &&
                         _minefield.IsZoneMined(x + 1, y - 1, out zoneMined) && zoneMined == true)
                    uncoveredMinedZones++;
            }

            // si controlla la zona a Ovest rispetto alla zona specificata
            if (_minefield.IsZoneFlagged(x - 1, y, out zoneFlagged))
            {
                // se tale zona è contrassegnata si aumenta il numero di zone contrassegnate come minate
                if (zoneFlagged == true)
                {
                    flaggedZones++;
                    // se tale zona è anche minata si aumenta il numero di zone contrassegnate e minate
                    if (_minefield.IsZoneMined(x - 1, y, out zoneMined) && zoneMined == true)
                        flaggedMinedZones++;
                }
                // se tale zona è scoperta e minata si aumenta il numero di zone scoperte e minate
                else if (_minefield.IsZoneCovered(x - 1, y, out zoneCovered) && zoneCovered == false &&
                         _minefield.IsZoneMined(x - 1, y, out zoneMined) && zoneMined == true)
                    uncoveredMinedZones++;
            }

            // si controlla la zona a Est rispetto alla zona specificata
            if (_minefield.IsZoneFlagged(x + 1, y, out zoneFlagged))
            {
                // se tale zona è contrassegnata si aumenta il numero di zone contrassegnate come minate
                if (zoneFlagged == true)
                {
                    flaggedZones++;
                    // se tale zona è anche minata si aumenta il numero di zone contrassegnate e minate
                    if (_minefield.IsZoneMined(x + 1, y, out zoneMined) && zoneMined == true)
                        flaggedMinedZones++;
                }
                // se tale zona è scoperta e minata si aumenta il numero di zone scoperte e minate
                else if (_minefield.IsZoneCovered(x + 1, y, out zoneCovered) && zoneCovered == false &&
                         _minefield.IsZoneMined(x + 1, y, out zoneMined) && zoneMined == true)
                    uncoveredMinedZones++;
            }

            // si controlla la zona a Sud-Ovest rispetto alla zona specificata
            if (_minefield.IsZoneFlagged(x - 1, y + 1, out zoneFlagged))
            {
                // se tale zona è contrassegnata si aumenta il numero di zone contrassegnate come minate
                if (zoneFlagged == true)
                {
                    flaggedZones++;
                    // se tale zona è anche minata si aumenta il numero di zone contrassegnate e minate
                    if (_minefield.IsZoneMined(x - 1, y + 1, out zoneMined) && zoneMined == true)
                        flaggedMinedZones++;
                }
                // se tale zona è scoperta e minata si aumenta il numero di zone scoperte e minate
                else if (_minefield.IsZoneCovered(x - 1, y + 1, out zoneCovered) && zoneCovered == false &&
                         _minefield.IsZoneMined(x - 1, y + 1, out zoneMined) && zoneMined == true)
                    uncoveredMinedZones++;
            }

            // si controlla la zona a Sud rispetto alla zona specificata
            if (_minefield.IsZoneFlagged(x, y + 1, out zoneFlagged))
            {
                // se tale zona è contrassegnata si aumenta il numero di zone contrassegnate come minate
                if (zoneFlagged == true)
                {
                    flaggedZones++;
                    // se tale zona è anche minata si aumenta il numero di zone contrassegnate e minate
                    if (_minefield.IsZoneMined(x, y + 1, out zoneMined) && zoneMined == true)
                        flaggedMinedZones++;
                }
                // se tale zona è scoperta e minata si aumenta il numero di zone scoperte e minate
                else if (_minefield.IsZoneCovered(x, y + 1, out zoneCovered) && zoneCovered == false &&
                         _minefield.IsZoneMined(x, y + 1, out zoneMined) && zoneMined == true)
                    uncoveredMinedZones++;
            }

            // si controlla la zona a Sud-Est rispetto alla zona specificata
            if (_minefield.IsZoneFlagged(x + 1, y + 1, out zoneFlagged))
            {
                // se tale zona è contrassegnata si aumenta il numero di zone contrassegnate come minate
                if (zoneFlagged == true)
                {
                    flaggedZones++;
                    // se tale zona è anche minata si aumenta il numero di zone contrassegnate e minate
                    if (_minefield.IsZoneMined(x + 1, y + 1, out zoneMined) && zoneMined == true)
                        flaggedMinedZones++;
                }
                // se tale zona è scoperta e minata si aumenta il numero di zone scoperte e minate
                else if (_minefield.IsZoneCovered(x + 1, y + 1, out zoneCovered) && zoneCovered == false &&
                         _minefield.IsZoneMined(x + 1, y + 1, out zoneMined) && zoneMined == true)
                    uncoveredMinedZones++;
            }

            // viene restituito il numero di zone minate attorno alla zona specificata
            // che sono state scoperte oppure che sono state contrassegnate come minate
            UncoveredOrFlaggedMinedZones = uncoveredMinedZones + flaggedMinedZones;

            // si controlla se sono state contrassegnate come minate unicamente zone minate
            return flaggedZones == flaggedMinedZones;
        }

        // Metodo che restituisce una breve descrizione della modalità di gioco specificata
        public static string DescribeModality(GameModality modality)
        {
            // descrizione della modalità di gioco
            string modalityDescription = null;

            // si stabilisce quale descrizione restituire
            switch (modality)
            {
                case GameModality.Classica:
                    modalityDescription = "Alla prima mina colpita si perde la partita";
                    break;
                case GameModality.Agevolata:
                    modalityDescription = "Dopo aver colpito " + AttemptsAgevolata + " mine si perde la partita";
                    break;
                case GameModality.Semplificata:
                    modalityDescription = "Dopo aver colpito più mine si perde la partita";
                    break;
                case GameModality.Sicura:
                    modalityDescription = "Le mine colpite non fanno perdere la partita";
                    break;
            }

            return modalityDescription;
        }

        // Proprietà che restituisce la modalità di gioco scelta
        public GameModality SelectedModality
        {
            get { return _modality; }
        }

        // Proprietà che indica se la partita è terminata
        public bool EndGame
        {
            get { return _victory != null; }
        }

        // Proprietà che indica se la partita è terminata con una vittoria
        public bool? Victory
        {
            get { return _victory; }
        }

        // Proprietà che restituisce il numero totale di tentativi disponibili per la partita
        public int? TotalAttempts
        {
            get { return SetAttempts(_modality); }
        }

        // Proprietà che restituisce il numero di tentativi rimanenti prima di perdere la partita
        public int? RemainingAttempts
        {
            get { return _attempts; }
        }
    }
}
