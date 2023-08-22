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

        private void ShowTable() {
            var rows = 6;
            var cols = 7;

            Console.Clear(); // Clear the console before printing the updated board

            Console.WriteLine("Connect 4");
            Console.WriteLine("  0 1 2 3 4 5 6");
            Console.WriteLine(" +-+-+-+-+-+-+-+");
    
            for (var row = 0; row < rows; row++)
            {
                Console.Write($"{row}|");
                for (var col = 0; col < cols; col++)
                {
                    var piece = ' ';
                    if (this._board.BoardArray[row, col] == 1)
                    {
                        piece = 'X';
                    }
                    else if (this._board.BoardArray[row, col] == 2)
                    {
                        piece = 'O';
                    }

                    Console.Write($"{piece}|");
                }
                Console.WriteLine();
            }

            Console.WriteLine(" +-+-+-+-+-+-+-+");
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
            while (true) {
                this.ShowTable();
                var input = false;
                while(input == false)
                {
                    Int32 col = this._player1.Move('X');
                    if(col < 7 && this._board.CanPlay(col))
                    {
                        input = true;

                        if (this._board.IsWinningMove(col)) {
                            this.DoMove(col, 1);
                            this.ShowTable();
                            Console.WriteLine(this._player1.Name + " has won!");
                            this.Winner = this._player1;
                            return;
                        }
                        
                        this.DoMove(col, 1);
                    }
                    else
                    {
                        Console.WriteLine("You supplied an invalid move, try again!");
                    }
                }

                this.ShowTable();
                input = false;
                while (input == false)
                {
                    Int32 col = this._player2.Move('O');
                    if (col < 7 && this._board.CanPlay(col))
                    {
                        input = true;
                        
                        if (this._board.IsWinningMove(col)) {
                            this.DoMove(col, 2);
                            this.ShowTable();
                            Console.WriteLine(this._player2.Name + " has won!");
                            this.Winner = this._player2;
                            return;
                        }
                        
                        this.DoMove(col, 2);
                    }
                    else
                    {
                        Console.WriteLine("You supplied an invalid move, try again!");
                    }
                }
            }
        }
    }
}