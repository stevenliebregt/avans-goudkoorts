﻿namespace Goudkoorts.Models.Tracks
{
    public class SplitSwitchTrack : SwitchTrack
    {
        public override Track Next
        {
            get
            {
                return ActiveConnection;
            }
            set
            {
            }
        }

        public SplitSwitchTrack(Orientation orientationOption1, Orientation orientationOption2) : base(orientationOption1, orientationOption2){}
    }
}