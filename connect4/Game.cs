using System;
using System.Text;
using asd.connect4.Players;

namespace asd.connect4
{
    internal sealed class Game
    {
        private Connect4Player _player1;
        private Connect4Player _player2;

        private readonly Board _board;

        public Connect4Player? Winner { get; private set; }

        public Game(Connect4Player player1, Connect4Player player2)
        {
            this._player1 = player1;
            this._player2 = player2;
            this.Winner = null;

            this._board = new Board();
        }

        private void ShowTable(String pName, Boolean showPname, Int32 turn, Boolean showTables)
        {
            if (!showTables) return;

            const Int32 rows = 6;
            const Int32 cols = 7;

            Console.Clear();

            var s = new StringBuilder();
            s.Append("Connect 4\n");

            s.Append("+---+---+---+---+---+---+---+\n");

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    Int32 piece = this._board.BoardArray[r, c];

                    s.Append(piece is 1 or 2 ? piece == 1 ? "| X " : "| O " : "|   ");
                }

                s.Append("|\n+---+---+---+---+---+---+---+\n");
            }

            s.Append("  0   1   2   3   4   5   6\n");

            s.Append(showPname ? pName + $": Your Turn. Turns left: {3 - turn}\n" : $"Turns left. {3 - turn}");

            Console.WriteLine(s);
        }

        private void DoMove(Int32 col, Int32 player)
        {
            this._board.PlayCol(col);

            for (var row = 5; row >= 0; row--)
            {
                if (this._board.BoardArray[row, col] != 0) continue;

                this._board.BoardArray[row, col] = player;
                break;
            }
        }

        public void Play()
        {
            var turns = 0;
            this.Winner = null;

            Boolean showTables = _player1 is HumanPlayer || _player2 is HumanPlayer;

            while (this._board.Possible() != 0)
            {
                if ((turns != 0) && ((turns % 3) == 0))
                {
                    (this._player1, this._player2) = (this._player2, this._player1);

                    turns = 0;
                }

                for (int i = 1; i < 3; i++)
                {
                    Char playerChar = i == 1 ? 'X' : 'O';
                    Connect4Player playerPlayer = i == 1 ? _player1 : _player2;

                    this.ShowTable(playerPlayer.Name, true, turns, showTables);

                    var input = false;
                    while (input == false)
                    {
                        Int32 col = playerPlayer.Move(playerChar);
                        if ((col < 7) && this._board.CanPlay(col))
                        {
                            input = true;

                            if (this._board.IsWinningMove(col))
                            {
                                this.DoMove(col, i);

                                this.ShowTable("", false, turns, showTables);

                                if (showTables)
                                {
                                    Console.WriteLine(playerPlayer.Name + " has won!");
                                    Console.ReadLine();
                                }

                                this.Winner = playerPlayer;
                                return;
                            }

                            this.DoMove(col, i);
                        }
                        else
                        {
                            if (showTables) Console.WriteLine("You supplied an invalid move, try again!");
                        }
                    }
                }

                turns++;
            }

            this.Winner = null;
        }
    }
}