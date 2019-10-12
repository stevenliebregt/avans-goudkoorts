namespace Goudkoorts.Models
{
    public class Cart
    {
        public bool Empty { get; set; } = false;
        public Track Location { get; set; }

        public void Move()
        {
            Location = Location.MoveCart();
        }
    }
}