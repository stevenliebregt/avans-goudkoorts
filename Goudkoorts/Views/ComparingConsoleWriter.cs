using System;

namespace Goudkoorts.Views
{
    public class ComparingConsoleWriter
    {
        private string _previousText = "";
        
        public void Write(string text)
        {
            if (_previousText.Equals(text)) return;
            
            // TODO: Process
            //Console.WriteLine($"oud: {_previousText}");
            //Console.WriteLine($"new: {text}");

            
            
            _previousText = text;
        }
    }
}