namespace Goudkoorts.Models
{
    public abstract class SwitchTrack : Track
    {
        public override Orientation Orientation => ActiveConnection == TrackOption1 ? _orientationOption1 : _orientationOption2;

        private readonly Orientation _orientationOption1;
        private readonly Orientation _orientationOption2;

        // Created this because i needed to add something to setter
        private Track _trackOption1;
        public Track TrackOption1
        {
            get => _trackOption1;
            set
            {
                _trackOption1 = value;
                ActiveConnection = _trackOption1;
            }
        }
        public Track TrackOption2 { get; set; }
        public Track ActiveConnection { get; set; }

        public SwitchTrack(Orientation orientationOption1, Orientation orientationOption2) : base(orientationOption1)
        {
            _orientationOption1 = orientationOption1;
            _orientationOption2 = orientationOption2;
        }

        public virtual void Switch()
        {
            ActiveConnection = TrackOption1 == ActiveConnection ? TrackOption2 : TrackOption1;
        }
    }
}