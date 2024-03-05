using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Direction { Up, Down, Left, Right }; // Definiše javni enumeracijski tip Direction sa četiri vrednosti: Up, Down, Left, Right.
                                                     //Direction je enumeracija koja predstavlja smerove u kojima zmija moze da ide: gore,dole, levo i desno

    public class Settings //Dodati klasu Settings koja ce da predstavlja osnovna podesavanja
    {
        public static int Width { get; set; } // Definiše javno statičko svojstvo Width, koje predstavlja širinu ekrana.
        public static int Height { get; set; } // Definiše javno statičko svojstvo Height, koje predstavlja visinu ekrana.
        public static int Score { get; set; } // Definiše javno statičko svojstvo Score, koje predstavlja trenutni rezultat.
        public static int Speed { get; set; } // Definiše javno statičko svojstvo Speed, koje predstavlja brzinu zmije.
        public static int Points { get; set; } // Definiše javno statičko svojstvo Points, koje predstavlja broj poena za svaki pojedeni deo hrane.
        public static bool GameOver { get; set; } // Definiše javno statičko svojstvo GameOver, koje označava da li je igra završena ili ne.
        public static Direction direction { get; set; } // Definiše javno statičko svojstvo direction, koje predstavlja trenutni pravac kretanja zmije.

        public Settings() // Definiše konstruktor klase Settings koji postavlja početne vrednosti
        {
            Width = 10; // Inicijalizuje širinu ekrana na 10.
            Height = 10; // Inicijalizuje visinu ekrana na 10.
            Score = 0; // Postavlja početni rezultat na 0.
            Speed = 10; // Postavlja početnu brzinu zmije na 10.
            Points = 1; // Postavlja početni broj poena na 1.
            GameOver = false; // Postavlja inicijalnu vrednost GameOver na false, što znači da igra nije završena.
            direction = Direction.Down; // Postavlja inicijalni pravac kretanja zmije na Down.
        }
    }
}
