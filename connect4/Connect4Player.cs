namespace asd.connect4
{
    public abstract class Connect4Player
    {
        public String Name { get; protected set; }

        public abstract Int32 Move(Char moveRepresentationChar, String position);
    }
}