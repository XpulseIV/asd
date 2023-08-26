using System;

using System.Runtime.InteropServices;

namespace asd.connect4.Players
{
    internal sealed class SmartPlayer : Connect4Player
    {
        public SmartPlayer() {
            this.Name = "SmartPlayer";
        }

        internal override Int32 Move(Char moveRepresentationChar, String position) {
            return GetMove(position, true);

            [DllImport("rs")]
            static extern Int32 GetMove(String position, Int32 posLen, Boolean best);
        }
    }
}