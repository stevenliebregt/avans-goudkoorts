using System;
using System.Collections.Generic;
using System.Text;

namespace Goudkoorts.Models
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

        public SplitSwitchTrack(Orientation orientation) : base(orientation)
        {

        }

    }
}