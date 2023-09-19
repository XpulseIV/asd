using System;

namespace asd.connect4
{
    internal abstract class Connect4Player
    {
        internal String Name { get; init; } = null!;

        internal abstract Int32 Move(Char moveRepresentationChar, String pos);
    }
}