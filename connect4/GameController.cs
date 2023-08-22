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

        public void Start() {
            UInt32 p1Wins = 0;
            UInt32 p2Wins = 0;
            
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
            }

            Console.ReadKey();
        }
    }
}