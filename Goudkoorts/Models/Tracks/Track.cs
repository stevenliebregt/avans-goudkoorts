namespace Goudkoorts.Models.Tracks
{
    public class Track : TilePlacable
    {
        public Cart Occupant { get; set; }
        public bool IsOccupied { get => Occupant != null;  }
        public virtual Track Next { get; set; }
        public virtual Orientation Orientation => _orientation;

        private readonly Orientation _orientation;
        
        public Track(Orientation orientation, Track next = null)
        {
            _orientation = orientation;
            Next = next;
        }

        public virtual Track MoveCart()
        {
            //Retire cart if it reaches end of this regular track
            if (Next == null)
            {
                Occupant = null;
                return null;
            }

            //Crash if next tile is occupied
            if (Next.IsOccupied)
            {
                Occupant.Crashed = true;
                return this;
            }

            //Try to let next track receive cart
            else if (Next.ReceiveCart(Occupant))
            {
                Occupant = null;
                return Next;
            }
            return this;
        }

        public virtual bool ReceiveCart(Cart newCart)
        {
            Occupant = newCart;
            return true;
        }
    }
}