namespace asd.connect4
{
    using Move = System.Int32;

    public class Connect4State
    {
        public static Move no_move = -1;

        public static Char[] player_markers = { '.', 'X', 'O' };

        public Connect4State(int num_rows_ = 6, int num_cols_ = 7)
        {
            player_to_move = 1;
            num_rows = num_rows_;
            num_cols = num_cols_;
            last_col = -1;
            last_row = -1;

            board = new();

            for (int i = 0; i < num_rows; i++)
            {
                List<char> row = new List<char>();

                // Initialize each row with num_cols columns and player_marker.
                for (int j = 0; j < num_cols; j++)
                {
                    row.Add(player_markers[0]);
                }

                board.Add(row);
            }
        }

        public void do_move(Move move)
        {
            int row = num_rows - 1;
            while (board[row][move] != player_markers[0]) row--;
            board[row][move] = player_markers[player_to_move];
            last_col = move;
            last_row = row;

            player_to_move = 3 - player_to_move;
        }

        public void do_random_move()
        {
            var random = new Random();

            while (true)
            {
                var move = random.Next(0, 7);
                if (board[0][move] == player_markers[0])
                {
                    do_move(move);
                    return;
                }
            }
        }

        public bool has_moves()
        {
            char winner = get_winner();
            if (winner != player_markers[0])
            {
                return false;
            }

            for (int col = 0; col < num_cols; ++col)
            {
                if (board[0][col] == player_markers[0])
                {
                    return true;
                }
            }
            return false;
        }

        public List<Move> get_moves()
        {
            List<Move> moves = new();
            if (get_winner() != player_markers[0])
            {
                return moves;
            }

            for (int col = 0; col < num_cols; ++col)
            {
                if (board[0][col] == player_markers[0])
                {
                    moves.Add(col);
                }
            }
            return moves;
        }

        public char get_winner()
        {
            if (last_col < 0)
            {
                return player_markers[0];
            }

            // We only need to check around the last piece played.
            var piece = board[last_row][last_col];

            // X X X X
            int left = 0, right = 0;
            for (int col = last_col - 1; col >= 0 && board[last_row][col] == piece; --col) left++;
            for (int col = last_col + 1; col < num_cols && board[last_row][col] == piece; ++col)
                right++;
            if (left + 1 + right >= 4)
            {
                return piece;
            }

            // X
            // X
            // X
            // X
            int up = 0, down = 0;
            for (int row = last_row - 1; row >= 0 && board[row][last_col] == piece; --row) up++;
            for (int row = last_row + 1; row < num_rows && board[row][last_col] == piece; ++row) down++;
            if (up + 1 + down >= 4)
            {
                return piece;
            }

            // X
            //  X
            //   X
            //    X
            up = 0;
            down = 0;
            for (int row = last_row - 1, col = last_col - 1;
                 row >= 0 && col >= 0 && board[row][col] == piece;
                 --row, --col)
                up++;
            for (int row = last_row + 1, col = last_col + 1;
                 row < num_rows && col < num_cols && board[row][col] == piece;
                 ++row, ++col)
                down++;
            if (up + 1 + down >= 4)
            {
                return piece;
            }

            //    X
            //   X
            //  X
            // X
            up = 0;
            down = 0;
            for (int row = last_row + 1, col = last_col - 1;
                 row < num_rows && col >= 0 && board[row][col] == piece;
                 ++row, --col)
                up++;
            for (int row = last_row - 1, col = last_col + 1;
                 row >= 0 && col < num_cols && board[row][col] == piece;
                 --row, ++col)
                down++;
            if (up + 1 + down >= 4)
            {
                return piece;
            }

            return player_markers[0];
        }

        public double get_result(int current_player_to_move)
        {
            var winner = get_winner();
            if (winner == player_markers[0])
            {
                return 0.5;
            }

            if (winner == player_markers[current_player_to_move])
            {
                return 0.0;
            }
            else
            {
                return 1.0;
            }
        }

        public int player_to_move;

        private int num_rows, num_cols;
        private List<List<Char>> board;
        private int last_col;
        private int last_row;
    }
}