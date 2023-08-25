using System.Runtime.InteropServices;

namespace asd.connect4.Players
{
    public class SmartPlayer : Connect4Player
    {
        public SmartPlayer() {
            this.Name = "SmartPlayer";
        }

        public override Int32 Move(Char moveRepresentationChar, String position)
        {
            [DllImport("rs")] static extern Int32 GetMove(String position, Boolean best);

            return GetMove(position, true);
        }
    }
}