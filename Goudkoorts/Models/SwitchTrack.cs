namespace Goudkoorts.Models
{
    public abstract class SwitchTrack : Track
    {
        public Track ActiveConnection { get; set; }
        public Track TrackOption1 { get; set; }
        public Track TrackOption2 { get; set; }

        public SwitchTrack(Orientation orientation) : base(orientation)
        {
        }

        public virtual void Switch()
        {
            ActiveConnection = TrackOption1 == ActiveConnection ? TrackOption2 : TrackOption1;
        }
    }
}