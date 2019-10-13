using Goudkoorts.Models.Tracks;

namespace Goudkoorts.Models
{
    public class Warehouse : TilePlacable
    {
        public Track StartTrack { get; set; }
        public char Letter { get; }
        public Warehouse(char letter)
        {
            Letter = letter;
        }
    }
}