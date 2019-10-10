using System.Collections;
using System.Collections.Generic;

namespace Goudkoorts.Models.Events
{
    public class EventLogger : IEnumerable<IEvent>
    {
        private readonly int _maxAmountOfLogs;
        private readonly Queue<IEvent> _queue;

        public EventLogger(int maxAmountOfLogs)
        {
            _maxAmountOfLogs = maxAmountOfLogs;
            _queue = new Queue<IEvent>(_maxAmountOfLogs);
        }
        
        public void Log(IEvent item)
        {
            if (_queue.Count == _maxAmountOfLogs) _queue.Dequeue();
            
            _queue.Enqueue(item);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IEvent> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }
    }
}