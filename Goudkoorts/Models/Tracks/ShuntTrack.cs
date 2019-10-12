namespace Goudkoorts.Models
{
    public class ShuntTrack : Track
    {
        public ShuntTrack(Orientation orientation, Track next = null) : base(orientation, next) { }

        public override Track MoveCart()
        {
            //If there is no next shunttrack or if it is occupied, keep same track
            if (Next == null || Next.IsOccupied) return this;

            //Try to let next track receive cart
            if (Next.ReceiveCart(Occupant))
            {
                Occupant = null;
                return Next;
            }
            return this;
        }
    }
}