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
        
        private readonly int _intervalMilliseconds;

        private readonly List<Action<Game>> _gameTickObservers = new List<Action<Game>>();
        
        private AutoResetEvent _autoResetEvent;
        private Timer _timer;

        private Field _field;
        
        private bool _isOver = false;
        
        public EventLogger Logger { get; } = new EventLogger(5);

        public int Score { get; private set; } = 0;
        
        public Game(int intervalMilliseconds)
        {
            _intervalMilliseconds = intervalMilliseconds < MinimumIntervalMilliSeconds ? DefaultIntervalMilliseconds : intervalMilliseconds;
            
            _field = new Field();
        }
        
        public void RegisterGameTickObserver(Action<Game> gameTickObserver)
        {
            _gameTickObservers.Add(gameTickObserver);
        }

        public void SwitchTrack(int trackId)
        {
            if (!_field.SwitchTracks.ContainsKey(trackId)) return;
            
            _field.SwitchTracks[trackId].Switch();
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

        public void Stop()
        {
            // TODO: Does AutoResetEvent.Dispose() need to be called?
            
            _autoResetEvent.Set();
            _timer.Dispose();
        }

        private void Tick(object stateInfo)
        {
            Score += 10; // TODO: Dit is example zodat we iets zien gebeuren

            // TODO: Hieronder is test
            var random = new Random();

            var warehouse = '?';
            switch (random.Next(0, 2))
            {
                case 0:
                    warehouse = 'A';
                    break;
                case 1:
                    warehouse = 'B';
                    break;
                case 2:
                    warehouse = 'C';
                    break;
            }
            
            if (random.Next(0, 10) == 4) Logger.Log(new CartSpawnedEvent(warehouse));
            
            // TODO: Tick the game state, move carts, spawn carts, etc.
            
            Notify(); // Notify our observers.
            
            if (!_isOver) return;
            
            // If it is a game over state, signal the event that we are done.
            var autoEvent = (AutoResetEvent) stateInfo;
            autoEvent.Set();
        }

        private void Notify()
        {
            foreach (var gameTickObserver in _gameTickObservers)
            {
                gameTickObserver.Invoke(this);
            }
        }
    }
}