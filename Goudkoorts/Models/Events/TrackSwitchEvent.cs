namespace Goudkoorts.Models.Events
{
    public class TrackSwitchEvent : IEvent
    {
        public int TrackId { get; }
        
        public TrackSwitchEvent(int trackId)
        {
            TrackId = trackId;
        }

        public override string ToString()
        { // TODO: This might not go here but on a View class
            return $"Wissel '{TrackId}' is omgezet";
        }
    }
}