namespace Goudkoorts.Models.Tracks
{
    public class QuayTrack : Track
    {
        private WaterTile _shipSpawnTile;
        
        public QuayTrack(Orientation orientation, WaterTile shipSpawnTile, Track next = null) : base(orientation, next)
        {
            _shipSpawnTile = shipSpawnTile;
        }

        public override Track MoveCart()
        {
            if (_shipSpawnTile.Placable == null || !(_shipSpawnTile.Placable is Ship ship)) return base.MoveCart();

            ship.AddCargo();
            Occupant.Empty = true;

            return base.MoveCart();
        }
    }
}