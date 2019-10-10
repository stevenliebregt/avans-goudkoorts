﻿using System;
using Goudkoorts.Models;
using Goudkoorts.Views;

namespace Goudkoorts.Controllers
{
    public class Controller : IGameObserver
    {
        private readonly InputView _inputView = new InputView();
        private readonly OutputView _outputView = new OutputView();

        private readonly int _intervalMilliseconds;

        private Game _game;
        
        public Controller(int intervalMilliseconds)
        {
            _intervalMilliseconds = intervalMilliseconds;
        }
        
        public void Start()
        {
            _outputView.DrawMenu();
            // TODO: Ask "press to start"
            
            // TODO: Setup for switching tracks

            _game = new Game(this, _intervalMilliseconds);
            _game.OnTick(_outputView.DrawGame);
            _game.Run(); // This runs, blocking, until the game is over.

            _outputView.DrawGameOver(_game.Score);
            
            // TODO: Ask "play again?"
        }

        public void OnTick()
        {
            _outputView.DrawGame(_game);
        }
    }
}