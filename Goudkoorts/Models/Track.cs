﻿namespace Goudkoorts.Models
{
    public class Track
    {
        public Cart Occupant { get; set; }
        public virtual Track Next { get; set; }
        private readonly Orientation _orientation;
        public Track(Orientation orientation, Track next = null)
        {
            _orientation = orientation;
            Next = next;
        }

        public virtual Track MoveCart()
        {
            if (Next != null && Next.ReceiveCart(Occupant))
            {
                Occupant = null;
                return Next;
            }
            return this;
        }

        public virtual bool ReceiveCart(Cart newCart)
        {
            Next.Occupant = newCart;
            return true;
        }
    }
}