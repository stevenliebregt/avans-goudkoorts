namespace Goudkoorts.Models
{
    public class Ship : TilePlacable
    {
        private int _freeSpaces = 8;

        public bool Full => _freeSpaces == 0;
        
        private readonly Game _game;

        public Ship(Game game)
        {
            _game = game;
        }

        public void AddCargo()
        {
            _freeSpaces--;
            _game.Score += 1;
        }
    }
}