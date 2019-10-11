using System;
using System.Collections.Generic;

namespace Goudkoorts.Models
{
    public class Field
    {
        private const int Width = 12;
        private const int Height = 9;

        public Tile[,] Tiles = new Tile[Height, Width];

        public readonly Dictionary<char, Warehouse> Warehouses = new Dictionary<char, Warehouse>();
        public readonly Dictionary<int, SwitchTrack> SwitchTracks = new Dictionary<int, SwitchTrack>();
        public QuayTrack Quay;
        public Tile ShipTile { get => Tiles[0,9]; }

        public Field()
        {
            GenerateTiles();
            InitializeField();
        }

        private void InitializeField()
        {
            SwitchTracks.Add(1, (SwitchTrack)AddToTiles(new MergeSwitchTrack(Orientation.BOTTOM_RIGHT, Orientation.TOP_RIGHT), 4, 3));
            SwitchTracks.Add(2, (SwitchTrack)AddToTiles(new SplitSwitchTrack(Orientation.TOP_LEFT, Orientation.BOTTOM_LEFT), 4, 5));
            SwitchTracks.Add(3, (SwitchTrack)AddToTiles(new MergeSwitchTrack(Orientation.TOP_RIGHT, Orientation.BOTTOM_RIGHT), 4, 9));
            SwitchTracks.Add(4, (SwitchTrack)AddToTiles(new MergeSwitchTrack(Orientation.BOTTOM_RIGHT, Orientation.TOP_RIGHT), 6, 6));
            SwitchTracks.Add(5, (SwitchTrack)AddToTiles(new SplitSwitchTrack(Orientation.BOTTOM_LEFT, Orientation.TOP_LEFT), 6, 8));

            Warehouses.Add('A', (Warehouse)AddToTiles(new Warehouse(), 3, 0));
            Warehouses.Add('B', (Warehouse)AddToTiles(new Warehouse(), 5, 0));
            Warehouses.Add('C', (Warehouse)AddToTiles(new Warehouse(), 7, 0));

            // Start from top, initializes in opposite direction
            var currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT),1,0);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 1);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 2);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 3);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 4);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 5);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 6);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 7);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 8);

            Quay = (QuayTrack)AddToTiles(new QuayTrack(Orientation.LEFT_RIGHT, currentTrack), 1, 9);
            currentTrack = Quay;

            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 1, 10);
            currentTrack = (Track)AddToTiles(new Track(Orientation.BOTTOM_LEFT, currentTrack), 1, 11);

            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_BOTTOM, currentTrack), 2, 11);
            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_BOTTOM, currentTrack), 3, 11);

            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_LEFT, currentTrack), 4, 11);

            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 4, 10);

            // Switchtrack nr 3

            SwitchTracks[3].Next = currentTrack;
            currentTrack = SwitchTracks[3];

            // After switchtrack nr 3 do top row

            SwitchTracks[3].TrackOption1 = (Track)AddToTiles(new Track(Orientation.BOTTOM_LEFT, currentTrack),3,9);
            currentTrack = SwitchTracks[3].TrackOption1;

            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 3, 8);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 3, 7);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 3, 6);

            currentTrack = (Track)AddToTiles(new Track(Orientation.BOTTOM_RIGHT, currentTrack), 3, 5);

            //Switchtrack nr 2
            SwitchTracks[2].TrackOption1 = currentTrack;
            currentTrack = SwitchTracks[2];
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 4, 4);

            //Switchtrack nr 1 until A warehouse
            SwitchTracks[1].Next = currentTrack;
            currentTrack = SwitchTracks[1];
            currentTrack = (Track)AddToTiles(new Track(Orientation.BOTTOM_LEFT, currentTrack), 3, 3);
            SwitchTracks[1].TrackOption2 = currentTrack;
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 3, 2);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 3, 1);
            Warehouses['A'].StartTrack = currentTrack;

            //Switchtrack nr 1 until B warehouse
            currentTrack = SwitchTracks[1];
            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_LEFT, currentTrack), 5, 3);
            SwitchTracks[1].TrackOption1 = currentTrack;
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 5, 2);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 5, 1);
            Warehouses['B'].StartTrack = currentTrack;

            // parts between 2 and 4, starting from 4
            currentTrack = (Track)AddToTiles(new Track(Orientation.BOTTOM_LEFT, SwitchTracks[4]), 5, 6);
            SwitchTracks[4].TrackOption2 = currentTrack;
            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_RIGHT, currentTrack), 5, 5);
            SwitchTracks[2].TrackOption2 = currentTrack;

            // parts between 3 and 5, starting from 5
            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_LEFT, SwitchTracks[3]), 5, 9);
            SwitchTracks[3].TrackOption2 = currentTrack;
            currentTrack = (Track)AddToTiles(new Track(Orientation.BOTTOM_RIGHT, currentTrack), 5, 8);
            SwitchTracks[5].TrackOption2 = currentTrack;

            currentTrack = (ShuntTrack)AddToTiles(new ShuntTrack(Orientation.LEFT_RIGHT), 8, 1);
            currentTrack = (ShuntTrack)AddToTiles(new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack), 8, 2);
            currentTrack = (ShuntTrack)AddToTiles(new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack), 8, 3);
            currentTrack = (ShuntTrack)AddToTiles(new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack), 8, 4);
            currentTrack = (ShuntTrack)AddToTiles(new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack), 8, 5);
            currentTrack = (ShuntTrack)AddToTiles(new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack), 8, 6);
            currentTrack = (ShuntTrack)AddToTiles(new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack), 8, 7);
            currentTrack = (ShuntTrack)AddToTiles(new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack), 8, 8);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 8, 9);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 8, 10);
            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_LEFT, currentTrack), 8, 11);
            currentTrack = (Track)AddToTiles(new Track(Orientation.BOTTOM_LEFT, currentTrack), 7, 11);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 7, 10);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 7, 9);
            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_RIGHT, currentTrack), 7, 8);
            SwitchTracks[5].TrackOption1 = currentTrack;
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, SwitchTracks[5]), 6, 7);
            SwitchTracks[4].Next = currentTrack;
            currentTrack = (Track)AddToTiles(new Track(Orientation.TOP_LEFT, currentTrack), 7, 6);
            SwitchTracks[4].TrackOption1 = currentTrack;
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 7, 5);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 7, 4);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 7, 3);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 7, 2);
            currentTrack = (Track)AddToTiles(new Track(Orientation.LEFT_RIGHT, currentTrack), 7, 1);
            Warehouses['C'].StartTrack = currentTrack;
        }

        private void GenerateTiles()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < 1; x++)
                {
                    Tiles[y, x] = new WaterTile();
                }
                for (int x = 1; x < Width; x++)
                {
                    Tiles[y, x] = new Tile();
                }
            }
        }

        private TilePlacable AddToTiles(TilePlacable placable, int y, int x)
        {
            Tiles[y, x].Placable = placable;
            return placable;
        }
    }
}