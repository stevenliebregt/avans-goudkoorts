using System;
using System.Collections.Generic;

namespace Goudkoorts.Models
{
    public class Field
    {
        private const int Width = 12;
        private const int Height = 8;
        public Dictionary<char, Warehouse> Warehouses = new Dictionary<char, Warehouse>();
        public SwitchTrack[] SwitchTracks = new SwitchTrack[5];

        public Field()
        {
            InitializeField(); // TODO: Build the field
        }

        // TODO Onderstaande functie misschien beter andere class

        private void InitializeField()
        {
            // TODO info voor steven: Op leesrichting geindexed zijn de switchtracks gebaseerd op afbeelding

            SwitchTracks[0] = new MergeSwitchTrack(Orientation.LEFT_RIGHT);
            SwitchTracks[1] = new SplitSwitchTrack(Orientation.LEFT_RIGHT);
            SwitchTracks[2] = new MergeSwitchTrack(Orientation.LEFT_RIGHT);
            SwitchTracks[3] = new MergeSwitchTrack(Orientation.LEFT_RIGHT);
            SwitchTracks[4] = new SplitSwitchTrack(Orientation.LEFT_RIGHT);

            Warehouses.Add('A', new Warehouse());
            Warehouses.Add('B', new Warehouse());
            Warehouses.Add('C', new Warehouse());

            // Start from top, initializes in opposite direction
            Track currentTrack = new Track(Orientation.LEFT_RIGHT);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);

            currentTrack = new Track(Orientation.BOTTOM_LEFT, currentTrack);

            currentTrack = new Track(Orientation.TOP_BOTTOM, currentTrack);
            currentTrack = new Track(Orientation.TOP_BOTTOM, currentTrack);

            currentTrack = new Track(Orientation.TOP_LEFT, currentTrack);

            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);

            // Switchtrack nr 3

            SwitchTracks[2].Next = currentTrack;
            currentTrack = SwitchTracks[2];

            // After switchtrack nr 3 do top row

            currentTrack = new Track(Orientation.BOTTOM_LEFT, currentTrack);

            SwitchTracks[2].TrackOption1 = currentTrack;

            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);

            currentTrack = new Track(Orientation.BOTTOM_RIGHT, currentTrack);

            //Switchtrack nr 2
            SwitchTracks[1].TrackOption1 = currentTrack;

            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);

            //Switchtrack nr 1 until A warehouse
            SwitchTracks[0].Next = currentTrack;
            currentTrack = SwitchTracks[0];
            currentTrack = new Track(Orientation.BOTTOM_LEFT, currentTrack);
            SwitchTracks[0].TrackOption2 = currentTrack;
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            Warehouses['A'].StartTrack = currentTrack;

            //Switchtrack nr 1 until B warehouse
            currentTrack = SwitchTracks[0];
            currentTrack = new Track(Orientation.TOP_LEFT, currentTrack);
            SwitchTracks[0].TrackOption1 = currentTrack;
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            Warehouses['B'].StartTrack = currentTrack;

            // parts between 2 and 4, starting from 4
            currentTrack = new Track(Orientation.BOTTOM_LEFT, SwitchTracks[3]);
            SwitchTracks[3].TrackOption2 = currentTrack;
            currentTrack = new Track(Orientation.TOP_RIGHT, currentTrack);
            SwitchTracks[1].TrackOption2 = currentTrack;

            // parts between 3 and 5, starting from 5
            currentTrack = new Track(Orientation.TOP_LEFT, SwitchTracks[2]);
            SwitchTracks[2].TrackOption2 = currentTrack;
            currentTrack = new Track(Orientation.BOTTOM_RIGHT, currentTrack);
            SwitchTracks[4].TrackOption2 = currentTrack;

            currentTrack = new ShuntTrack(Orientation.LEFT_RIGHT);
            currentTrack = new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new ShuntTrack(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.TOP_LEFT, currentTrack);
            currentTrack = new Track(Orientation.BOTTOM_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.TOP_RIGHT, currentTrack);
            SwitchTracks[4].TrackOption1 = currentTrack;
            currentTrack = new Track(Orientation.LEFT_RIGHT, SwitchTracks[4]);
            SwitchTracks[3].Next = currentTrack;
            currentTrack = new Track(Orientation.TOP_LEFT, currentTrack);
            SwitchTracks[3].TrackOption1 = currentTrack;
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            currentTrack = new Track(Orientation.LEFT_RIGHT, currentTrack);
            Warehouses['C'].StartTrack = currentTrack;
        }
    }
}