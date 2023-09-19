using System;
using asd.connect4.MyLifeIsALie;

namespace asd.connect4.Players
{
    internal sealed class SmurfPlayer : Connect4Player
    {
        private readonly Random _random;

        public SmurfPlayer() {
            this.Name = "SmurfPlayer";

            this._random = new Random();
        }

        internal override Int32 Move(Char moveRepresentationChar, String pos)
        {
            return 0;
        }
    }
}