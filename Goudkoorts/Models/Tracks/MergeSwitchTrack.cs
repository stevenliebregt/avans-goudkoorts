namespace Goudkoorts.Models.Tracks
{
    public class MergeSwitchTrack : SwitchTrack
    {
        public MergeSwitchTrack(Orientation orientationOption1, Orientation orientationOption2) : base(orientationOption1, orientationOption2) { }

        public override bool ReceiveCart(Cart newCart)
        {
            // Check if cart can go into switch
            if (CanReceiveCart(newCart))
            {
                Occupant = newCart;
                return true;
            }
            return false;
        }

        public override bool CanReceiveCart(Cart newCart)
        {
            return newCart.Location == ActiveConnection;
        }

    }
}