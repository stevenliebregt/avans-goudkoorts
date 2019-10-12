namespace Goudkoorts.Models
{
    public class Cart
    {
        public bool Empty { get; set; } = false;
        public bool Crashed { get; set; } = false;
        public bool Retired { get => Location == null; }
        public Track Location { get; set; }

        public void Move()
        {
            Location = Location.MoveCart();
        }
    }
}