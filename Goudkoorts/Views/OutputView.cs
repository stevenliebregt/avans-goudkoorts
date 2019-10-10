using System;
using System.Text;
using Goudkoorts.Models;

namespace Goudkoorts.Views
{
    public class OutputView
    {
        private ComparingConsoleWriter _gameWriter = new ComparingConsoleWriter();
        
        public void DrawMenu()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Welkom bij Goudkoorts!");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.AppendLine("Deze applicatie is gemaakt door:");
            stringBuilder.AppendLine(" - Rick Berkers");
            stringBuilder.AppendLine(" - Steven Liebregt");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.AppendLine("Druk op een willekeurige knop om het programma te starten");
            
            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }

        public void DrawGame(Game game)
        {
            var score = game.Score.ToString().PadLeft(5, '0');
            
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine( "╔═════════════════════════════╗");
            stringBuilder.AppendLine($"║       Score : {score}         ║");
            stringBuilder.AppendLine( "╚═════════════════════════════╝");
            
            // We don't clear, since that might be expensive to redraw everything.
            //Console.WriteLine(stringBuilder.ToString());
            _gameWriter.Write(stringBuilder.ToString());
        }

        public void DrawGameOver(int score)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Game over! Je score was: {score}");
            
            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}