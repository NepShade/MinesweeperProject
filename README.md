# MinesweeperProject
### Informazioni Studente
Nome: Simone<br/>
Cognome: Tonucci<br/>
Matricola: #228618<br/>
Nome progetto: MinesweeperProject<br/>

### Specifica Progetto
Si vuole realizzare un’applicazione desktop che rappresenti una variante di “*campo minato*”, classico videogioco a singolo giocatore per PC degli anni 90, in cui lo scopo del gioco è scoprire tutte le zone sicure del campo minato senza far esplodere le mine.

In particolare si vuole sviluppare una versione del gioco in grado di supportare due tipologie di campo minato: un **campo tradizionale** e un **campo hardcore**. Con il termine **campo tradizionale** si intende un classico campo minato in cui le mine sono generate successivamente la prima zona scoperta dal giocatore, mentre con il termine **campo hardcore** si intende un campo minato in cui le mine sono generate precedentemente la scoperta della prima zona.

L’applicazione da sviluppare si basa sulle seguenti caratteristiche e regole di gioco:
- Il campo di gioco consiste in un campo rettangolare (o quadrato) composto da molteplici zone quadrate con cui il giocatore può interagire cliccando su di esse con il tasto sinistro e destro del mouse.
- Il campo minato può avere delle dimensioni (lunghezza e altezza) minime di 8x8 e massime di 30x30, mentre per quanto riguarda le mine in esso contenute il loro numero può variare da un minimo di 10 a un massimo corrispondente all’80% delle zone costituenti il campo minato.
- Le zone del campo minato sono inizialmente tutte coperte, per poi essere progressivamente scoperte dal giocatore man mano che il gioco procede. Una volta che tutte le zone sicure sono state scoperte il giocatore vince la partita.
- Il giocatore può scoprire una zona cliccando su di essa con il tasto sinistro del mouse. Se la zona scoperta è minata si perde la partita, se invece è sicura viene visualizzato un numero (da 1 a 8) che indica la quantità di mine presenti attorno ad essa. Tale numero deve essere utilizzato dal giocatore per individuare le mine all’interno del campo minato e stabilire quali successive zone scoprire. Nell’eventualità che la zona sicura scoperta non abbia mine nelle sue immediate vicinanze il gioco scopre automaticamente le zone ad essa vicine fintanto che non vengono scoperte zone sicure che restituiscono un numero.
- Il giocatore può eventualmente anche contrassegnare (visivamente con una bandiera) una zona in cui crede sia presente una mina cliccando su di essa con il tasto destro del mouse. Fintanto che una zona è contrassegnata non può essere scoperta, di conseguenza premendo nuovamente il tasto destro su una zona già contrassegnata come minata questa viene “pulita” o riportata al suo stato originario.

L’applicazione deve inoltre prevedere quattro differenti modalità di partita, allo scopo di agevolare i neofiti e introdurre i più piccoli al gioco. Queste modalità fanno riferimento alle medesime regole di gioco e sono differenziate tra loro unicamente dal numero di tentativi disponibili, ossia dalla quantità di mine che è possibile far esplodere prima di perdere la partita. Nel dettaglio:
- la modalità **classica** prevede un singolo tentativo;
- la modalità **agevolata** prevede tre tentativi;
- la modalità **semplificata** prevede un numero di tentativi variabile (non inferiore a tre e corrispondente al 10% delle mine contenute nel campo minato);
- la modalità **sicura** non prevede un limite al numero di tentativi, impedendo di fatto di perdere la partita.

L’applicazione infine deve prevedere l’eventuale aggiunta in futuro di ulteriori tipologie di campo minato.
