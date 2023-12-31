namespace asd.connect4
{
    internal sealed class Board
    {
        public UInt64 _currentPosition;
        public UInt64 _mask;

        private UInt32 Moves { get; set; }

        public Boolean switched = false;
        public Boolean yellow = true;

        /**
        * @param position, a bitmap of the player to evaluate the winning pos
        * @param mask, a mask of the already played spots
        *
        * @return a bitmap of all the winning free spots making an alignment
        */
        private static UInt64 winning_position(UInt64 position, UInt64 mask) {
            // vertical;
            UInt64 r = (position << 1) & (position << 2) & (position << 3);

            //horizontal
            UInt64 p = (position << 7) & (position << 14);
            r |= p & (position << 21);
            r |= p & (position >> 7);
            p = (position >> 7) & (position >> 14);
            r |= p & (position >> 21);
            r |= p & (position << 7);

            //diagonal 1
            p = (position << 6) & (position << 12);
            r |= p & (position << 18);
            r |= p & (position >> 6);
            p = (position >> 6) & (position >> 12);
            r |= p & (position >> 18);
            r |= p & (position << 6);

            //diagonal 2
            p = (position << 8) & (position << 16);
            r |= p & (position << 24);
            r |= p & (position >> 8);
            p = (position >> 8) & (position >> 16);
            r |= p & (position >> 24);
            r |= p & (position << 8);

            return r & (BoardMask ^ mask);
        }

        /**
        * Return a bitmask of the possible winning positions for the current player
        */
        private UInt64 winning_position() => winning_position(this._currentPosition, this._mask);

        /**
        * Indicates whether the current player wins by playing a given column.
        * This function should never be called on a non-playable column.
        * @param col: 0-based index of a playable column.
        * @return true if current player makes an alignment by playing the corresponding column col.
        */
        public Boolean IsWinningMove(Int32 col) => (this.winning_position() & this.Possible() & column_mask(col)) != 0;

        // Static bitmaps
        private const UInt64 BottomMask = 4432676798593;

        private const UInt64 BoardMask = 279258638311359;

        /**
        * Bitmap of the next possible valid moves for the current player
        * Including losing moves.
        */
        public UInt64 Possible() => (this._mask + BottomMask) & BoardMask;

        // return a bitmask containg a single 1 corresponding to the top cel of a given column
        private static UInt64 top_mask_col(Int32 col) => (UInt64)1 << (5 + (col * 7));

        // return a bitmask containg a single 1 corresponding to the bottom cell of a given column
        private static UInt64 bottom_mask_col(Int32 col) => (UInt64)1 << (col * 7);

        /**
        * Indicates whether a column is playable.
        * @param col: 0-based index of column to play
        * @return true if the column is playable, false if the column is already full.
        */
        public Boolean CanPlay(Int32 col) => (this._mask & top_mask_col(col)) == 0;

        // return a bitmask 1 on all the cells of a given column
        private static UInt64 column_mask(Int32 col) => (((UInt64)1 << 6) - 1) << (col * 7);

        /**
        * Plays a possible move given by its bitmap representation
        *
        * @param move: a possible move given by its bitmap representation
        *        only one bit of the bitmap should be set to 1
        *        the move should be a valid possible move for the current player
        */
        private void Play(UInt64 move) {
            this._currentPosition ^= this._mask;
            this._mask |= move;
            this.Moves++;
        }

        /**
        * Plays a playable column.
        * This function should not be called on a non-playable column or a column making an alignment.
        *
        * @param col: 0-based index of a playable column.
        */
        public void PlayCol(Int32 col, Board b) {
            b.yellow = !b.yellow;
            this.Play((this._mask + bottom_mask_col(col)) & column_mask(col));
        }
    }
}