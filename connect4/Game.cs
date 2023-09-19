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

        private void ShowTable(String pName, Boolean showPname, Int32 turns, Boolean showTables, Boolean playersSwitched)
        {
            if (!showTables) return;

            const Int32 cols = 7;
            const Int32 rows = 6;

            Console.Clear();

            var s = new StringBuilder();
            s.Append("Connect 4\n");

            s.Append("+---+---+---+---+---+---+---+\n");

            for (var r = rows - 1; r >= 0; r--)
            {
                for (var c = 0; c < cols; c++)
                {
                    char playerSymbol = this._board.yellow ? 'X' : 'O';
                    char opponentSymbol = this._board.yellow ? 'O' : 'X';

                    s.Append((this._board._currentPosition & ((UInt64)1 << (c * 7 + r))) != 0
                        ? $"| {playerSymbol} "
                        : ((this._board._mask & ((UInt64)1 << (c * 7 + r))) != 0
                            ? $"| {opponentSymbol} "
                            : "|   "));

                }

                s.Append("|\n+---+---+---+---+---+---+---+\n");
            }

            s.Append("  0   1   2   3   4   5   6\n");

            s.Append(showPname ? pName + $": Your Turn. Turns left: {3 - turns}, Players switched: {playersSwitched}\n" : $"Turns left. {3 - turns}, Players switched: {playersSwitched}");

            Console.WriteLine(s);
        }

        public void Play()
        {
            var turns = 0;
            this.Winner = null;

            String pos = "";

            Boolean showTables = _player1 is HumanPlayer || _player2 is HumanPlayer;

            while (this._board.Possible() != 0)
            {
                /*if ((turns != 0) && ((turns % 3) == 0))
                {
                    (this._player1, this._player2) = (this._player2, this._player1);

                    this._board.switched = !this._board.switched;

                    turns = 0;
                }*/
                
                for (int i = 1; i < 3; i++)
                {
                    Char playerChar = i == 1 ? 'X' : 'O';
                    Connect4Player playerPlayer = i == 1 ? _player1 : _player2;

                    this.ShowTable(playerPlayer.Name, true, turns, showTables, _board.switched);

                    var input = false;
                    while (input == false)
                    {
                        Int32 col = playerPlayer.Move(playerChar, pos);
                        if ((col < 7) && this._board.CanPlay(col))
                        {
                            input = true;

                            if (this._board.IsWinningMove(col))
                            {
                                this._board.PlayCol(col, this._board);

                                this.ShowTable("", false, turns, showTables, _board.switched);

                                if (showTables)
                                {
                                    Console.WriteLine(playerPlayer.Name + " has won!");
                                    Console.ReadLine();
                                }

                                this.Winner = playerPlayer;
                                return;
                            }

                            this._board.PlayCol(col, this._board);
                            pos += Convert.ToChar(col);
                        }
                        else
                        {
                            if (!showTables) Console.WriteLine($"You supplied an invalid move, try again! {col}");
                        }
                    }
                }

                turns++;
            }

            this.Winner = null;
        }
    }
}