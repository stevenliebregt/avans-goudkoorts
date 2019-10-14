using System;

namespace Goudkoorts.Views
{
    public class InputView
    {
        public void PressAnyKeyToStart()
        {
            Console.ReadKey();
        }
        
        /// <summary>
        /// Connect the user input for switching tracks.
        /// </summary>
        /// <param name="minimumTrackId">The lowest track id we allow to be pressed</param>
        /// <param name="maximumTrackId">The highest track id we allow to be pressed</param>
        /// <param name="callback">A method that takes a bool and an int, the bool tells if the 'exit' key was
        /// pressed, and the int is the id of the track to switch.</param>
        public void OnSwitchTrackIdPressed(int minimumTrackId, int maximumTrackId, Action<bool, int> callback)
        {
            while (true)
            {
                var consoleKey = Console.ReadKey();

                if (consoleKey.Key == ConsoleKey.Escape)
                {
                    callback(true, 0);
                    return;
                }

                if (char.IsDigit(consoleKey.KeyChar) 
                    && int.TryParse(consoleKey.KeyChar.ToString(), out var id) 
                    && id >= minimumTrackId 
                    && id <= maximumTrackId)
                {
                    callback(false, id);
                }
            }
        }
    }
}