using System;

namespace asd.connect4.Players
{
    internal sealed class HumanPlayer : Connect4Player
    {
        public HumanPlayer(String name) {
            this.Name = name;
        }

        internal override Int32 Move(Char moveRepresentationChar) {
            while (true) {
                Console.SetCursorPosition(0, 17);

                Console.WriteLine($"{this.Name}: {moveRepresentationChar}, Make a move!");
                String input = Console.ReadLine() ?? String.Empty;

                if (input != String.Empty) {
                    return Int32.Parse(input);
                }
            }
        }
    }
}