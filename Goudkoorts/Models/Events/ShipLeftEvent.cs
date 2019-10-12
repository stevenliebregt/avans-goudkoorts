namespace Goudkoorts.Models.Events
{
    public class ShipLeftEvent : IEvent
    {
        public override string ToString()
        {
            return "Het schip is vol en weggevaren";
        }
    }
}