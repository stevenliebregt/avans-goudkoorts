namespace Goudkoorts.Models
{
    public class Ship : TilePlacable
    {
        public int FreeSpaces { get; set; } = 8;

        public bool Full => FreeSpaces == 0;
    }
}