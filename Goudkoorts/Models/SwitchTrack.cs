namespace Goudkoorts.Models
{
    public abstract class SwitchTrack : Track
    {
        private Track _trackOption1;
        public Track ActiveConnection { get; set; }
        public Track TrackOption1
        {
            get 
            { 
                return _trackOption1; 
            }
            set
            {
                _trackOption1 = value;
                ActiveConnection = _trackOption1;
            }
        }
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