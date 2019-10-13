using System;
using System.Text;
using Goudkoorts.Models;
using Goudkoorts.Models.Tracks;

namespace Goudkoorts.Views
{
    public class OutputView
    {
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
            const int lineLength = 40;
            
            var stringBuilder = new StringBuilder();
            
            // Append score part.
            stringBuilder.AppendLine("═══ Score ".PadRight(lineLength, '═'));
            stringBuilder.AppendLine(game.Score.ToString().PadLeft(7, '0'));

            // Append rail part.
            stringBuilder.AppendLine("═══ Speelveld ".PadRight(lineLength, '═'));
            
            for (var row = 0; row < game.Field.Tiles.GetLength(0); row++)
            {
                var line = "";
                for (var col = 0; col < game.Field.Tiles.GetLength(1); col++)
                {
                    line += GetDrawChar(game.Field.Tiles[row, col]);
                }
                stringBuilder.AppendLine(line);
            }
            
            // Append log part.
            stringBuilder.AppendLine("═══ Logs ".PadRight(lineLength, '═'));

            var logCount = 0;
            foreach (var log in game.Logger)
            {
                stringBuilder.AppendLine(log.ToString());
                logCount++;
            }

            while (logCount < 5) // Reserve the space for 5 logs.
            {
                stringBuilder.AppendLine("");
                logCount++;
            }

            // Append controls.
            stringBuilder.AppendLine("═══ Bediening ".PadRight(lineLength, '═'));
            stringBuilder.AppendLine("Druk op de toetsen 1 - 5 om de wissels om te zetten");
            stringBuilder.AppendLine("Druk op <ESCAPE> om te stoppen");
            
            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }

        public void DrawGameOver(int score)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Game over! Je score was: {score}");
            
            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }

        //TODO finish this
        //TODO think of good symbols
        private char GetDrawChar(Tile tile)
        {
            if (tile.Placable == null)
            {
                if (tile is WaterTile)
                    return '≈';
                if (tile is Tile)
                    return ' ';
            }
            else if (tile.Placable is Track track)
            {
                if (track.Occupant != null)
                {
                    return track.Occupant.Empty ? 'o' : 'ô';
                }

                if (track is QuayTrack)
                {
                    return 'K';
                }

                if (track is ShuntTrack)
                {
                    return '_';
                }
                
                switch (track.Orientation)
                {
                    case Orientation.LEFT_RIGHT:
                        return '─';
                    case Orientation.TOP_BOTTOM:
                        return '│';
                    case Orientation.BOTTOM_LEFT:
                        return '┐';
                    case Orientation.BOTTOM_RIGHT:
                        return '┌';
                    case Orientation.TOP_LEFT:
                        return '┘';
                    case Orientation.TOP_RIGHT:
                        return '└';
                }
            }
            else if (tile.Placable is Warehouse warehouse)
            {
                return warehouse.Letter;
            }
            else if (tile.Placable is Ship)
            {
                return 'S';
            }
            
            return ' ';
        }
    }
}