using System;

namespace asd.connect4.Players
{
    internal sealed class HumanPlayer : Connect4Player
    {
        public HumanPlayer(String name) {
            this.Name = name;
        }

        internal override Int32 Move(Char moveRepresentationChar, String position) {
            Console.WriteLine($"{this.Name}: {moveRepresentationChar}, Make a move!");
            return Int32.Parse(Console.ReadLine() ?? String.Empty);
        }
    }
}