using System;
using asd.connect4.Players;

namespace asd.connect4
{
    internal sealed class GameController
    {
        private Connect4Player? _p1;
        private Connect4Player? _p2;

        private Int32 _games;

        public void Setup()
        {
            var players = new Connect4Player[] {
                new HumanPlayer("Malte"),
                new HumanPlayer("Jabok"),
                new RandomPlayer(),
                new SmurfPlayer()
            };

            Console.WriteLine("Players: ");

            for (var i = 0; i < players.Length; i++)
            {
                Console.WriteLine($"{i}: {players[i].Name}");
            }

            Console.Write("\n");

            Console.Write("Chose a player for p1: ");
            this._p1 = players[UInt32.Parse(Console.ReadLine() ?? String.Empty)];

            Console.Write("Chose a player for p2: ");
            this._p2 = players[UInt32.Parse(Console.ReadLine() ?? String.Empty)];

            Console.Write("\n");

            Console.Write("Enter number of games to play: ");
            this._games = Int32.Parse(Console.ReadLine() ?? String.Empty);
        }

        private static void Fillout(Connect4Player playerOne, Connect4Player playerTwo, Int32 p1Wins, Int32 p2Wins, Int32 draws, Int32 games)
        {
            Console.Clear();

            // Player 1
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 3);
            Console.Write(playerOne.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Green;

            Int32 winP1Percentage = (p1Wins * 10) / games;
            Console.Write(new String('█', winP1Percentage));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" " + p1Wins);

            // Player 2
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 5);
            Console.Write(playerTwo.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Red;

            Int32 winP2Percentage = (p2Wins * 100) / games;
            Console.Write(new String('█', winP2Percentage));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" " + p2Wins);

            // Draws
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 7);
            Console.Write("Draws: ");
            Console.ForegroundColor = ConsoleColor.Blue;

            Int32 drawPercentage = (draws * 100) / games;
            Console.Write(new String('█', drawPercentage));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" " + draws);

            Console.SetCursorPosition(25, 9);
            Console.Write(playerOne.Name);
            Console.SetCursorPosition(45, 9);
            Console.WriteLine(playerTwo.Name);
            Console.WriteLine("          Won games:");
            Console.WriteLine("          Difference:");
            Console.SetCursorPosition(25 + (playerOne.Name.Length / 2), 10);
            Console.Write(p1Wins);
            Console.SetCursorPosition(25 + (playerOne.Name.Length / 2), 11);
            Int32 diff = p1Wins - p2Wins;

            Console.Write(diff != 0 ? diff : "");

            Console.SetCursorPosition(45 + (playerTwo.Name.Length / 2), 10);
            Console.Write(p2Wins);
            Console.SetCursorPosition(44 + (playerTwo.Name.Length / 2), 11);
            diff = p2Wins - p1Wins;

            Console.Write(diff != 0 ? diff : "");
        }

        public void Start()
        {
            var p1Wins = 0;
            var p2Wins = 0;
            var draws = 0;

            for (var i = 0; i < this._games; i++)
            {
                var game = new Game(this._p1!, this._p2!);
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

                Console.Clear();
                Fillout(this._p1!, this._p2!, p1Wins, p2Wins, draws, this._games);
            }
        }
    }
}