using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    //Location object definition
    public class Locations
    {
        #region Constant Declaration
        public const string newline = "\r\n";
        public const Dictionary<Direction, bool> OffPathways = {<Direction.West, true>};

        private const string[] OffObjects = { "desk", "note", "cup", "chair", "charlie", "pen" };
        private const string OffDescription = "You are in a small room. In front of you is a plain looking wooden desk." + newline +
                "Charlie sits at the desk. Feel free to say hello or to look around the room.";
        #endregion

        public LocationObject Office = new LocationObject(OffObjects, OffDescription, "office", OffPathways);
        public LocationObject Hallway = new LocationObject(HallObjects, HallDescription, "hallway", HallPathways);

        #region Location Object Template Class
        public class LocationObject
        {
            public LocationObject(string[] Objects, string Desc, string HumanName, Dictionary<Direction, bool> Paths)
            {
                Description = Desc;
                Name = HumanName;
                foreach (string x in Objects)
                {
                    Contains.Add(x);
                }
                Pathways = Paths;
            }
            //Name of location object
            public string Name { get; set; }
            //Objects collected during the game
            public List<string> Contains { get; set; } 
            public string Description { get; set; }
            //Navigation data
            private Dictionary<Direction, bool> Pathways { get; set; }
            public bool Navigation(Direction direction)
            {
                bool value = false;
                if (Pathways.ContainsKey(direction))
                {
                    Pathways.TryGetValue(direction, out value);
                    return value;
                }
                else
                {
                    return value;
                }
            }
        }
        #endregion

        #region Enums
        public enum LocationType
        {
            Office,
            Hallway
        }

        public enum Direction
        {
            N = 0, North = 0,
            NE = 1, NorthEast = 1,
            NW = 2, NorthWest = 2,
            S = 3, South = 3,
            SE = 4, SouthEast = 4,
            SW = 5, SouthWest = 5,
            E = 6, East = 6,
            W = 7, West = 7      
        }
        #endregion

        #region Get Set Functions
        public string[] AllLocations()
        {
            return (string[])Enum.GetValues(typeof(LocationType));
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

        public bool isDirection(LocationType currentLocation, Direction direction)
        {
            bool isPath = false;
            switch (currentLocation)
            {
                case LocationType.Office:
                    isPath = Office.Navigation(direction);
                    break;
                case LocationType.Hallway:
                    isPath = Hallway.Navigation(direction);
                    break;
            }
            return isPath;
        }
        #endregion

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
            private Dictionary<Direction, bool> Pathways { get; set; }
            public bool Navigation(Direction direction)
            {
                bool value = false;
                if (Pathways.ContainsKey(direction))
                {
                    Pathways.TryGetValue(direction, out value);
                    return value;
                }
                else
                {
                    return value;
                }
            }
        }
    }
}
