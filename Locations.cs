using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    //Location object definition
    class Locations
    {
        public const string newline = "\r\n";
        OfficeObject Office = new OfficeObject();
        HallwayObject Hallway = new HallwayObject();
        public enum LocationType
        {
            Office,
            Hallway
        }
        public enum Direction
        {
            n = 0, north = 0,
            ne = 1, northeast = 1,
            nw = 2, northwest = 2,
            s = 3, south = 3,
            se = 4, southeast = 4,
            sw = 5, southwest = 5,
            e = 6, east = 6,
            w = 7, west = 7      
        }
        //Returns the description of a location object
        public string GetDescription(LocationType location)
        {
            switch (location)
            {
                case LocationType.Office:
                    return Office.Description;
                case LocationType.Hallway:
                    return Hallway.Description;
            }
            return "Error in Location.GetDescription";
        }
        //Returns the contents of a location object
        public string[] GetContents(LocationType location)
        {
            string[] contents = { };
            switch (location)
            {
                case LocationType.Office:
                    contents = Office.Contains.ToArray();
                    break;
                case LocationType.Hallway:
                    contents = Hallway.Contains.ToArray();
                    break;
            }
            return contents;
        }
        public void AddContents(LocationType location, string item)
        {
            switch (location)
            {
                case LocationType.Office:
                    Office.Contains.Add(item);
                    break;
                case LocationType.Hallway:
                    Hallway.Contains.Add(item);
                    break;
            }
        }
        public void RemoveContents(LocationType location, string item)
        {
            switch (location)
            {
                case LocationType.Office:
                    if (Office.Contains.Contains(item))
                    {
                        Office.Contains.Remove(item);
                    }
                    break;
                case LocationType.Hallway:
                    if (Hallway.Contains.Contains(item))
                    {
                        Hallway.Contains.Remove(item);
                    }
                    break;
            }
        }
        public string[] LocationObjects()
        {
            string[] LocationObjects = { "hallway", "office" };
            return LocationObjects;
        }
        public string[] Directions(LocationType currentLocation)
        {
            string[] pathways = { };
            switch (currentLocation)
            {
                case LocationType.Office:
                    pathways = Office.Pathways();
                    break;
                case LocationType.Hallway:
                    pathways = Hallway.Pathways();
                    break;
            }
            return pathways;
        }
        public string[] AllLocations()
        {
            
            return all;
        }
        public class OfficeObject
        {
            public OfficeObject()
            {
                foreach (string x in Objects)
                {
                    Contains.Add(x);
                }
            }
            public string Name = "office";
            public List<string> Contains { get; set; } //Objects collected during the game
            public string[] Objects = { "desk", "note", "cup", "chair", "charlie", "pen" };//Objects to start the game with
            public string Description = "You are in a small room. In front of you is a plain looking wooden desk." + newline +
                "Charlie sits at the desk. Feel free to say hello or to look around the room.";
            //Navigation data
            private string GoNorth = null;
            private string GoNorthEast = null;
            private string GoEast = null;
            private string GoSouthEast = null;
            private string GoSouth = null;
            private string GoSouthWest = null;
            private string GoWest = "hallway";
            private string GoNorthWest = null;
            public string[] Pathways()
            {
                List<string> possible = new List<string>();

                if (GoNorth != null)
                {
                    possible.Add(GoNorth);
                }
                if (GoNorthEast != null)
                {
                    possible.Add(GoNorthEast);
                }
                if (GoEast != null)
                {
                    possible.Add(GoEast);
                }
                if (GoSouthEast != null)
                {
                    possible.Add(GoSouthEast);
                }
                if (GoSouth != null)
                {
                    possible.Add(GoSouth);
                }
                if (GoSouthWest != null)
                {
                    possible.Add(GoSouthWest);
                }
                if (GoWest != null)
                {
                    possible.Add(GoWest);
                }
                if (GoNorthWest != null)
                {
                    possible.Add(GoNorthWest);
                }
                return possible.ToArray();
            }
        }
        public class HallwayObject
        {
            public HallwayObject()
            {
                foreach (string x in Objects)
                {
                    Contains.Add(x);
                }
            }
            public string Name = "hallway";
            public List<string> Contains { get; set; } //Objects collected during the game
            public string[] Objects = { }; //Objects to start the game with
            public string Description = "It's a hallway. It runs North to South and seems to lead to nowhere." + newline +
                "Behind you to the East is a door with the name Charlie Chaplon in vinyl lettering.";
            //Navigation data
            private string GoNorth = null;
            private string GoNorthEast = null;
            private string GoEast = "office";
            private string GoSouthEast = null;
            private string GoSouth = null;
            private string GoSouthWest = null;
            private string GoWest = null;
            private string GoNorthWest = null;
            public string[] Pathways()
            {
                List<string> possible = new List<string>();

                if (GoNorth != null)
                {
                    possible.Add(GoNorth);
                }
                if (GoNorthEast != null)
                {
                    possible.Add(GoNorthEast);
                }
                if (GoEast != null)
                {
                    possible.Add(GoEast);
                }
                if (GoSouthEast != null)
                {
                    possible.Add(GoSouthEast);
                }
                if (GoSouth != null)
                {
                    possible.Add(GoSouth);
                }
                if (GoSouthWest != null)
                {
                    possible.Add(GoSouthWest);
                }
                if (GoWest != null)
                {
                    possible.Add(GoWest);
                }
                if (GoNorthWest != null)
                {
                    possible.Add(GoNorthWest);
                }
                return possible.ToArray();
            }
        }
    }
}
