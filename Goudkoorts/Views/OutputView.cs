using System;
using System.Text;
using Goudkoorts.Models;

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
            var stringBuilder = new StringBuilder();
            
            // Append score part. TODO: Make nicer
            stringBuilder.AppendLine($"Game score = {game.Score}");

            // Append rail part.
            for (int row = 0; row < game.Field.Tiles.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < game.Field.Tiles.GetLength(1); col++)
                {
                    line += GetDrawChar(game.Field.Tiles[row, col]);
                }
                stringBuilder.AppendLine(line);
            }
            
            // Append log part. TODO: Make nicer
            foreach (var log in game.Logger)
            {
                stringBuilder.AppendLine(log.ToString());
            }
            
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
                    
                    return 'c';
                }
                else
                {
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
            }
            else if (tile.Placable is Warehouse warehouse)
            {
                return warehouse.Letter;
            }
            return ' ';
        }
    }
}