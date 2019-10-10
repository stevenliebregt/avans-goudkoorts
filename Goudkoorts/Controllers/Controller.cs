using System;
using System.ComponentModel;
using Goudkoorts.Models;
using Goudkoorts.Views;

namespace Goudkoorts.Controllers
{
    public class Controller
    {
        private readonly InputView _inputView = new InputView();

        private readonly int _intervalMilliseconds;
        private readonly OutputView _outputView = new OutputView();

        private Game _game;

        public Controller(int intervalMilliseconds)
        {
            _intervalMilliseconds = intervalMilliseconds;
        }

        public void Start()
        {
            _outputView.DrawMenu();
            // TODO: Ask "press to start"
            _outputView.Clear();            
            
            _game = new Game(_intervalMilliseconds);

            // Connect input for switching tracks.
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += delegate { _inputView.OnSwitchTrackIdPressed(1, 5, (exit, trackId) =>
            {
                if (exit) _game.Stop();
                
                _game.SwitchTrack(trackId);
            }); };
            backgroundWorker.RunWorkerAsync();

            _game.RegisterGameTickObserver(_outputView.DrawGame);
            _game.Run(); // This runs, blocking, until the game is over.

            _outputView.DrawGameOver(_game.Score);

            // TODO: Ask "play again?"
        }
    }
}