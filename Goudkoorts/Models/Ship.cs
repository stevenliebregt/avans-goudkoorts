namespace Goudkoorts.Models
{
    public class Ship : TilePlacable
    {
        public int FreeSpaces { get; set; } = 8;

        public bool Full => FreeSpaces == 0;
        private Game _game;

        public Ship(Game game)
        {
            _game = game;
        }

        public void AddCargo()
        {
            FreeSpaces--;
            _game.Score += 2;
        }
    }
}