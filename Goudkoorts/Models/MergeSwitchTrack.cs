using System;
using System.Collections.Generic;
using System.Text;

namespace Goudkoorts.Models
{
    public class MergeSwitchTrack : SwitchTrack
    {

        public MergeSwitchTrack(Orientation orientation) : base(orientation)
        {

        }

        public override bool ReceiveCart(Cart newCart)
        {
            // Check if cart can go into switch
            if (newCart.Location == Next)
            {
                Next.Occupant = newCart;
                return true;
            }
            return false;
        }
    }
}