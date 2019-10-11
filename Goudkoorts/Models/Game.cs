using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Goudkoorts.Models.Events;

namespace Goudkoorts.Models
{
    public class Game
    {
        private const int MinimumIntervalMilliSeconds = 100;
        private const int DefaultIntervalMilliseconds = 1000;

        private const int CartSpawnChancePercentage = 10;
        private const int ShipSpawnChancePercentage = 40;
        
        private readonly int _intervalMilliseconds;

        private readonly List<Action<Game>> _gameTickObservers = new List<Action<Game>>();
        
        private AutoResetEvent _autoResetEvent;
        private Timer _timer;

        private readonly Random _random;
        
        private bool _isOver = false;
        
        public EventLogger Logger { get; } = new EventLogger(5);
        public Field Field { get; private set; }

        public int Score { get; private set; } = 0;
        
        public Game(int intervalMilliseconds)
        {
            _intervalMilliseconds = intervalMilliseconds < MinimumIntervalMilliSeconds ? DefaultIntervalMilliseconds : intervalMilliseconds;

            _random = new Random();
            Field = new Field();
        }
        
        public void RegisterGameTickObserver(Action<Game> gameTickObserver)
        {
            _gameTickObservers.Add(gameTickObserver);
        }

        public void SwitchTrack(int trackId)
        {
            if (!Field.SwitchTracks.ContainsKey(trackId)) return;

            Field.SwitchTracks[trackId].Switch();
            Logger.Log(new TrackSwitchEvent(trackId));
        }
        
        public void Run()
        {
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Tick, _autoResetEvent, 0, _intervalMilliseconds);

            // Start the loop, and throw away the timer once it is done.
            _autoResetEvent.WaitOne();
            _timer.Dispose();
        }

        /// <summary>
        /// Signals the timer so it stops, and disposes of it.
        /// </summary>
        public void Stop()
        {
            _autoResetEvent.Set();
            _timer.Dispose();
        }

        private void Tick(object stateInfo)
        {
            // TODO: Tick the game state, move carts, spawn carts, boats etc.

            MoveCarts();
            
            if (_random.Next(0, 100) < CartSpawnChancePercentage) SpawnCart();

            // Check for a full ship.
            // TODO: Implement
            
            // if (_field.Quay.Empty && _random.Next(0, 100) < ShipSpawnChancePercentage) SpawnShip(); // TODO: Enable (alleen als schip niet deze beurt geleegd is (denk ik))

            NotifyObservers();
            
            if (!_isOver) return;
            
            // If it is a game over state, signal the event that we are done.
            var autoEvent = (AutoResetEvent) stateInfo;
            autoEvent.Set();
        }

        private void MoveCarts()
        {
            // TODO: Move voorste karretjes eerst, of check na deze functie pas op botsing
            // TODO: Also check for collision
            
            throw new NotImplementedException();
        }
        
        private void SpawnCart()
        {
            var targetWarehouse = Field.Warehouses.ElementAt(_random.Next(0, Field.Warehouses.Count)).Value;
            
            // TODO: Spawn
            
            throw new NotImplementedException();
        }

        private void SpawnShip()
        {
            // TODO field heeft de tile property waar die op moet
            throw new NotImplementedException();
        }
        
        private void NotifyObservers()
        {
            foreach (var gameTickObserver in _gameTickObservers)
            {
                gameTickObserver.Invoke(this);
            }
        }
    }
}