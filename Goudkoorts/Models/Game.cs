using System;
using System.Threading;

namespace Goudkoorts.Models
{
    public class Game
    {
        private const int MinimumIntervalMilliSeconds = 100;
        private const int DefaultIntervalMilliseconds = 1000;
        
        private readonly int _intervalMilliseconds;

        private readonly IGameObserver _gameObserver;
        
        private bool _isOver = false;

        public int Score { get; private set; } = 0;
        
        public Game(IGameObserver gameObserver, int intervalMilliseconds)
        {
            _gameObserver = gameObserver;
            _intervalMilliseconds = intervalMilliseconds < MinimumIntervalMilliSeconds ? DefaultIntervalMilliseconds : intervalMilliseconds;
        }
        
        public void Run()
        {
            var autoEvent = new AutoResetEvent(false);
            var timer = new Timer(Tick, autoEvent, 0, _intervalMilliseconds);

            // Start the loop, and throw away the timer once it is done.
            autoEvent.WaitOne();
            timer.Dispose();
        }

        private void Tick(object stateInfo)
        {
            Score += 10; // TODO: Dit is example zodat we iets zien gebeuren
            
            // TODO: Tick the game state, move carts, spawn carts, etc.
           
            // Notify our observer.
            _gameObserver.OnTick();
            
            // If it is a game over state, signal the event that we are done.
            if (!_isOver) return;
            
            var autoEvent = (AutoResetEvent) stateInfo;
            autoEvent.Set();
        }

        public void OnTick(Action<Game> callback)
        {
            callback.Invoke(this);
        }
    }
}