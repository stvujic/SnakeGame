using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>(); //Kolekcija krugova koji predstavljaju zmiju
        private Circle food = new Circle(); //Krug koji predstavlja hranu
        public Form1()
        {
            InitializeComponent();
 //implementacija za gameTimer objekat, Tajmer sluzi da generise event na svaki sekund, te da se odradi abdejtovanje pictureBox - a svaki put kada sekunda prodje, kako bismo imali pravo stanje na ekranu.
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            StartGame(); //Postaviti da se prilikom ucitavanja forme pozove odmah StartGame metoda.
            GenerateFood();// kod za generisanje kuglice koja predstavlja hranu, GenerateFood() metoda, Pozvati je prilikom ucitavanja forme
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if(Settings.GameOver == true) // Ako je GameOver i ako je stisnut ENTER neka krene igra
            {
                if (Input.KeyPresed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.KeyPresed(Keys.Right) && Settings.direction != Direction.Left) //ako je igra u toku, ako je stisnut taster za desno, a zmijica ne ide u levo onda neka skrene u desno.
                {
                    Settings.direction = Direction.Right;
                }
                else if (Input.KeyPresed(Keys.Left) && Settings.direction != Direction.Right) //ako je igra u toku, ako je stisnut taster za levo, a zmijica ne ide u desno onda neka skrene u levo.
                {
                    Settings.direction = Direction.Left;
                }
                else if (Input.KeyPresed(Keys.Up) && Settings.direction != Direction.Down) //ako je igra u toku, ako je stisnut taster za gore, a zmijica ne ide u dole onda neka skrene u gore.
                {
                    Settings.direction = Direction.Up;
                }
                else if (Input.KeyPresed(Keys.Down) && Settings.direction != Direction.Up) //ako je igra u toku, ako je stisnut taster za dole, a zmijica ne ide u gore onda neka skrene u dole.
                {
                    Settings.direction = Direction.Down;
                }
                MovePlayer(); // Staviti da se metoda MovePlayer poziva prilikom abdejtovanja ekrana
            }
            pbCanvas.Invalidate(); // uradi update celog PictureBox-a sa tekucim podacima
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;
            new Settings(); //Ucitace ono sto smo podesili u Settings
            Snake.Clear();// deo koda da obrise sve sto je bilo u kolekciji krugova za zmiju, kako bi moglo da se igra od samog pocetka.
            Circle head = new Circle(); //Napravice glavu zmije sa koordinatama 5,5
            head.X = 5;
            head.Y = 5;
            Snake.Add(head); // Dodace u kolekciju Snake glavu zmije "head"
            lblScore.Text = Settings.Score.ToString(); //Prikazace trenutni skor na tabli, ucitan iz Settings podesavanja; tamo je podeseno da krece od 0
        }

        //kod za generisanje kuglice koja predstavlja hranu
        Random random = new Random();
        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width - 5; //Maximalna X koordinata na kojoj hrana moze da se napravi je maksimalna sirina pictureBox umanjena za -5
            int maxYPos = pbCanvas.Size.Height - 5; //Maximalna Y koordinata na kojoj hrana moze da se napravi je maksimalna duzina pictureBox umanjena za -5
            food = new Circle(); // pravi se objekat food koji ce da predstavlja hranu; gore sskroz imas food objekat
            //sa ovim dole mu se setuju random koordinate
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true); //event KeyDown osluskuje (pretplacen) da li je neki taster na tastaturi stisnut; ovo je sve povezano sa ChangeState u Input klasi
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false); // event KeyUp osluskuje da li je podignut prst sa dugmenta na tastaturi koji je ranije bio pritisnut; ovo je sve povezano sa ChangeState u Input klasi
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e) // Dodati event Paint koji ce da nam sluzi da svaki put kada se uradi update ekrana, oboji glavu zmije crnom bojom, a hranu crvenom bojom, kako bi se videli na ekranu.
        {
            Graphics canvas = e.Graphics;// Koristi platno za koje je generisan event za bojenje
            if (!Settings.GameOver) // ukoliko je igra u toku
            {
                Brush snakeColor;
                for (int i = 0; i < Snake.Count; i++) // uzmi zmiju
                {
                    if (i == 0) // ukoliko je u pitanju glava zmije tj nulti element u kolekciji
                    {
                        snakeColor = Brushes.Black; // oboji glavu crnom bojom
                    }
                    else
                    {
                        snakeColor = Brushes.Green; // ako je telo zmije, oboji ga zelenom bojom
                    }
                    // Oboji svaku tacku zmijice datom bojom kako je gore navedeno, i oko nje oboji kvadratic velicine 10x10: Settings.Width smo setovali na 10, kao i Settings.Height
                    canvas.FillEllipse(snakeColor,
                        new Rectangle(Snake[i].X, Snake[i].Y, Settings.Width, Settings.Height));
                    // isto uradi i sa hranom
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X, food.Y, Settings.Width, Settings.Height));
                }
            }
            else
            {
                string gameOver = "Game Over \n Your final score is:"
                    + Settings.Score + ".\n Press Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }
        private void MovePlayer()
        {
            for(int i=Snake.Count -1; i>=0;i--) // prolazim kroz celu zmiju
            {
                if (i == 0) // ukoliko je u pitanju glava
                {
                    switch (Settings.direction) 
                    {
                        //Ako ide na gore, pomeraj je i dalje na gore za 10 pozicija(Settings.Speed)
                        case Direction.Up:
                            Snake[0].Y -= Settings.Speed;
                            break;
                        case Direction.Down:
                            Snake[0].Y += Settings.Speed;
                            break;
                        case Direction.Left:
                            Snake[0].X -= Settings.Speed;
                            break;
                        case Direction.Right:
                            Snake[0].X += Settings.Speed;
                            break;
                        default:
                            break;
                    }
   // Staviti da se metoda Die poziva u okviru metode MovePlayer, ali paziti da bude u for petlji i unutar upita if (i == 0), odmah posle switchcase-a zato sto se odnosi na glavu zmije.
                    if (Snake[0].X < 0 || Snake[0].Y <0 || 
                        Snake[0].X >= pbCanvas.Size.Width || 
                        Snake[0].Y >= pbCanvas.Size.Height)
                    {
                        Die();
                    }
                    //Metoda Eat() se poziva u slucaju kada se koordinate glave zmije poklope sa koordinatama kuglice za hranu.
                    if (Difference(Snake[0].X, food.X) && Difference (Snake[0].Y, food.Y)) 
                    {
                        Eat();
                    }
                }
                else //implementacija pomeranja tela zmije, ovo je nakon if(i==0)
                     //If(i==0) upit je vazio za prvu kuglicu koja predstavlja glavu zmije, dok ovaj else upit vazi za sve ostale kuglice, tj. telo zmije
                {
                    //U ovom upitu svaku kuglicu stavljamo na mesto gde je prethodna bila i tako nam se sve kuglice pomeraju zajedno.
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }
        private void Die()
        {
            Settings.GameOver = true; // Dodati metodu Die(). Kraj igre je ukoliko glava zmije izadje iz okvira pictureBoxa, tj.ako udari u ivicu

        }

        private void Eat() //metoda Eat() u kojoj zmija jede hranu
        {   
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;
            Snake.Add(food); //Kada se hrana pojede, kuglica treba da se doda u listu koja predstavlja kolekciju kuglica tela zmije
            Settings.Score += Settings.Points; //Takodje treba da se score poveca za 1
            lblScore.Text = Settings.Score.ToString(); //i da se to ispise na labeli za rezultat.
            GenerateFood();
        }
        private bool Difference (int i,int j) // Posto coordinate predstavljaju samo jednu tacku a mi imamo citavu kuglicu od 10 tacaka okolo,
                                              // onda cemo da dodamo i razliku (ovo se odnosi na metodu Eat() i IF deo u okviru Eat() metode
        {
            if(((i-j)<9 && (i-j)>=0) || ((j-i)<9 && (j - i) >= 0))
            {
                return true;
            }
            return false;
        }
    }
}
