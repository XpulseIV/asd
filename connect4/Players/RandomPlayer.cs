using System;

namespace asd.connect4.Players
{
    internal sealed class RandomPlayer : Connect4Player
    {
        private readonly Random _random;

        public RandomPlayer() {
            this.Name = "RandomPlayer";

            this._random = new Random();
        }

        internal override Int32 Move(Char moveRepresentationChar, String position) => this._random.Next(0, 7);
    }
}