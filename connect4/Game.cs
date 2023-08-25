using System.Text;
using ANSIConsole;

namespace asd.connect4
{
    public class Game
    {
        private Connect4Player _player1;
        private Connect4Player _player2;
        
        private Board _board;
        
        public Connect4Player Winner { get; private set; }
        public UInt32 Moves { get; private set; }

        public Game(Connect4Player player1, Connect4Player player2)
        {
            this._player1 = player1;
            this._player2 = player2;
            this.Winner = null!;

            this._board = new Board();

            this.Moves = 0;
        }

        private void ShowTable(String pName, bool showPname, Int32 turn) {
            var rows = 6;
            var cols = 7;

            Console.Clear(); // Clear the console before printing the updated board

            StringBuilder s = new StringBuilder();
            s.Append("Connect 4\n");

            s.Append("+---+---+---+---+---+---+---+\n");

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Int32 piece = _board.BoardArray[r, c];
                    
                    s.Append((piece == 1 || piece == 2) ? ((piece == 1) ? "| X " : "| O ") : "|   ");
                }

                s.Append($"|\n+---+---+---+---+---+---+---+\n");
            }
            s.Append("  0   1   2   3   4   5   6\n");

            s.Append(showPname ? pName + $": Your Turn. Turns left: {3 - turn}\n": $"Turns left. {3 - turn}");

            Console.WriteLine(s);
            
        }

        private void DoMove(Int32 col, Int32 player) {
            this._board.PlayCol(col);

            for (var row = 5; row >= 0; row--) {
                if (this._board.BoardArray[row, col] == 0) {
                    this._board.BoardArray[row, col] = player;
                    break;
                }
            }
        }
        
        public void Play() {
            String position = ""; 
            Int32 turns = 0;
            while (_board.Possible() != 0) {
                if ((turns != 0) && (turns % 3 == 0))
                {
                    Connect4Player temp = _player1;
                    _player1 = _player2;
                    _player2 = temp;

                    turns = 0;
                }
                
                this.ShowTable(_player1.Name, true, turns);
                var input = false;
                while(input == false)
                {
                    Int32 col = this._player1.Move('X', position);
                    if(col < 7 && this._board.CanPlay(col))
                    {
                        input = true;

                        if (this._board.IsWinningMove(col)) {
                            this.DoMove(col, 1);
                            this.ShowTable("", false, turns);
                            Console.WriteLine(this._player1.Name + " has won!");
                            Console.ReadLine();
                            this.Winner = this._player1;
                            return;
                        }
                        
                        this.DoMove(col, 1);
                        position += Convert.ToString(col + 1);
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("You supplied an invalid move, try again!");
                    }
                }

                this.ShowTable(_player2.Name, true, turns);
                input = false;
                while (input == false)
                {
                    Int32 col = this._player2.Move('O', position);
                    if (col < 7 && this._board.CanPlay(col))
                    {
                        input = true;
                        
                        if (this._board.IsWinningMove(col)) {
                            this.DoMove(col, 2);
                            this.ShowTable("", false, turns);
                            Console.WriteLine(this._player2.Name + " has won!");
                            Console.ReadLine();
                            this.Winner = this._player2;
                            return;
                        }
                        
                        this.DoMove(col, 2);
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("You supplied an invalid move, try again!");
                    }
                }

                turns++;
            }

            Winner = null!;
        }
    }
}