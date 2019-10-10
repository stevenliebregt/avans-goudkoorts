namespace Goudkoorts.Models.Events
{
    public class CartSpawnedEvent : IEvent
    {
        public char Warehouse { get; }
        
        public CartSpawnedEvent(char warehouse)
        {
            Warehouse = warehouse;
        }

        public override string ToString()
        {
            return $"Er is een karretje verschenen bij warenhuis '{Warehouse}'";
        }
    }
}