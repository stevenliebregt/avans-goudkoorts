namespace Goudkoorts.Models.Events
{
    public class ShipArrivedEvent : IEvent
    {
        public override string ToString()
        {
            return "Er is een schip aan de kade verschenen";
        }
    }
}