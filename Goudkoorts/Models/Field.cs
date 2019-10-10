using System;
using System.Collections.Generic;

namespace Goudkoorts.Models
{
    public class Field
    {
        private const int Width = 12;
        private const int Height = 8;
        private Track InitialTrack; //Reference to linked list structure
        public Dictionary<char, Warehouse> Warehouses = new Dictionary<char, Warehouse>();

        public Field()
        {
            InitializeField(); // TODO: Build the field
        }

        // TODO Maybe move below functions to somewhere else

        private void InitializeField()
        {
            Warehouses.Add('A', new Warehouse());
            Warehouses.Add('B', new Warehouse());
            Warehouses.Add('C', new Warehouse());

            // TODO use the right track types

            // -- First 8 bottom tracks -- //
            InitialTrack = new Track();
            Track currentTrack = InitialTrack;
            currentTrack = AddTrack(currentTrack, typeof(Track));
        }
        private Track AddTrack(Track currentTrack, Type type)
        {
            object newTrack = Activator.CreateInstance(type);
            return null;
            //currentTrack.Next = (type)newTrack;
        }


    }
}