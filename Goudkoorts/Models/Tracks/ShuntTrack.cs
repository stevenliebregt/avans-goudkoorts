namespace Goudkoorts.Models
{
    public class ShuntTrack : Track
    {
        public ShuntTrack(Orientation orientation, Track next = null) : base(orientation, next) { }

        public override Track MoveCart()
        {
            if (Next != null && Next.IsOccupied)
            {
                return this;
            }

            //Try to let next track receive cart
            if (Next != null && Next.ReceiveCart(Occupant))
            {
                Occupant = null;
                return Next;
            }
            return this;
        }
    }
}