using asd.connect4.Players;

namespace asd.connect4
{
    public class GameController
    {
        private Connect4Player _p1;
        private Connect4Player _p2;

        private UInt32 _games;
        
        public void Setup() {
            var players = new Connect4Player[] {
                new HumanPlayer("Malte"),
                new HumanPlayer("Jabok"),
                new RandomPlayer()
            };

            Console.WriteLine("Players: ");
            
            for (var i = 0; i < players.Length; i++) {
                Console.WriteLine($"{i}: {players[i].Name}");
            }
            
            Console.Write("\n");
            
            Console.Write("Chose a player for p1: ");
            this._p1 = players[UInt32.Parse(Console.ReadLine() ?? String.Empty)];
            
            Console.Write("Chose a player for p2: ");
            this._p2 = players[UInt32.Parse(Console.ReadLine() ?? String.Empty)];
            
            Console.Write("\n");
            
            Console.Write("Enter number of games to play: ");
            this._games = UInt32.Parse(Console.ReadLine() ?? String.Empty);
        }

        public void Fillout(Connect4Player PlayerOne, Connect4Player PlayerTwo, Int32 p1Wins, Int32 p2Wins, Int32 games)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 3);
            Console.Write(PlayerOne.Name + ":");
            Console.ForegroundColor = ConsoleColor.Green;
            if (true)
            {
                Console.SetCursorPosition(15, 3);
                int x = 0;
                while (x < p1Wins * 100 / games)
                {
                    Console.Write("█");
                    x++;
                }
            }
            Console.SetCursorPosition((p1Wins * 100 / games) + 15, 3);
            Console.Write("█");
            Console.SetCursorPosition((p1Wins * 100 / games) + 16, 3);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(p1Wins);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 5);
            Console.Write(PlayerTwo.Name + ":");
            Console.ForegroundColor = ConsoleColor.Red;
            if (true)
            {
                Console.SetCursorPosition(15, 5);
                int x = 0;
                while (x < p2Wins * 100 / games)
                {
                    Console.Write("█");
                    x++;
                }
            }
            Console.SetCursorPosition((p2Wins * 100 / games) + 15, 5);
            Console.Write("█");
            Console.SetCursorPosition((p2Wins * 100 / games) + 16, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(p2Wins);
            Console.SetCursorPosition(25, 7);
            Console.Write(PlayerOne.Name);
            Console.SetCursorPosition(45, 7);
            Console.WriteLine(PlayerTwo.Name);
            Console.WriteLine("          Vunna spel:");
            Console.WriteLine("            Skillnad:                                        ");
            Console.SetCursorPosition(25 + PlayerOne.Name.Length / 2, 8);
            Console.Write(p1Wins);
            Console.SetCursorPosition(25 + PlayerOne.Name.Length / 2, 9);
            int diff = p1Wins - p2Wins;
            if (diff > 0)
            {
                Console.Write("+" + diff);
            }
            else
            {
                Console.Write("      ");
            }
            Console.SetCursorPosition(45 + PlayerTwo.Name.Length / 2, 8);
            Console.Write(p2Wins);
            Console.SetCursorPosition(45 + PlayerTwo.Name.Length / 2, 9);
            diff = p2Wins - p1Wins;
            if (diff > 0)
            {
                Console.Write("+" + diff);
            }
            else
            {
                Console.Write("      ");
            }
        }

        public void Start() {
            UInt32 p1Wins = 0;
            UInt32 p2Wins = 0;
            UInt32 draws = 0;
            
            for (UInt32 i = 0; i < this._games; i++) {
                var game = new Game(this._p1, this._p2);
                game.Play();
                
                if (game.Winner == this._p1)
                {
                    p1Wins++;
                }
                else if (game.Winner == this._p2)
                {
                    p2Wins++;
                }
                else if (game.Winner is null)
                {
                    draws++;
                }
            }



            Console.ReadKey();
        }
    }
}