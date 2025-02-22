﻿using System;
using System.Collections.Generic;
using System.Threading;
using Goudkoorts.Models.Events;

namespace Goudkoorts.Models
{
    public class Game
    {
        private const int MinimumIntervalMilliSeconds = 100;
        private const int DefaultIntervalMilliseconds = 1000;

        private const int CartSpawnChancePercentage = 15;
        private const int ShipSpawnChancePercentage = 40;
        
        private int _intervalMilliseconds;

        private readonly List<Action<Game>> _gameTickObservers = new List<Action<Game>>();
        
        private AutoResetEvent _autoResetEvent;
        private Timer _timer;

        private readonly Random _random;
        
        private bool _isOver = false;

        private bool _shipSpawnCooldown = false;

        public EventLogger Logger { get; } = new EventLogger(5);
        public Field Field { get; }

        public int Score { get; set; } = 0;
        
        public Game(int intervalMilliseconds)
        {
            _intervalMilliseconds = intervalMilliseconds < MinimumIntervalMilliSeconds ? DefaultIntervalMilliseconds : intervalMilliseconds;

            _random = new Random();
            Field = new Field(this);
        }
        
        public void RegisterGameTickObserver(Action<Game> gameTickObserver)
        {
            _gameTickObservers.Add(gameTickObserver);
        }

        public void SwitchTrack(int trackId)
        {
            // TODO: Can be disabled
            
            if (!Field.SwitchTracks.ContainsKey(trackId)) return;

            Field.SwitchTracks[trackId].Switch();
            Logger.Log(new TrackSwitchEvent(trackId));
            
            NotifyObservers();
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
            MoveCarts();

            if (_random.Next(0, 100) < CartSpawnChancePercentage) SpawnCart();

            if (Field.Ship != null && Field.Ship.Full) HandleFullShip();
            
            if (!_shipSpawnCooldown && Field.Ship == null && _random.Next(0, 100) < ShipSpawnChancePercentage) SpawnShip();

            NotifyObservers();

            _shipSpawnCooldown = false; // Remove cooldown at end of turn.

            RedefineDifficulty();
            
            if (!_isOver) return;
            
            // If it is a game over state, signal the event that we are done.
            var autoEvent = (AutoResetEvent) stateInfo;
            autoEvent.Set();
        }

        private void MoveCarts()
        {
            if (Field.MoveCarts()) return;
            
            Logger.Log(new CartCrashedEvent());
            _isOver = true;
        }

        private void SpawnCart()
        {
            Logger.Log(new CartSpawnedEvent(Field.SpawnCart().Letter));
        }

        private void SpawnShip()
        {
            Field.SpawnShip();
            Logger.Log(new ShipArrivedEvent());
        }

        private void HandleFullShip()
        {
            Field.Ship = null;
            Score += 10;
            Logger.Log(new ShipLeftEvent());

            _shipSpawnCooldown = true;
        }

        private void RedefineDifficulty()
        {
            var newInterval = 1000 - (Score * 10); // *10 because otherwise it is really slow to speed up.

            if (newInterval == _intervalMilliseconds || newInterval < MinimumIntervalMilliSeconds) return;

            _timer.Change(0, newInterval);
            _intervalMilliseconds = newInterval;
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