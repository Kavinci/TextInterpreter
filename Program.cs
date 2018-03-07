using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TextInterpreter
{
    class Program
    {
        static string status = "start";
        static string newline = "\r\n";
        //static XElement response = XElement.Load("Response.xml");
        static IOData Data = new IOData();
        static Actions Act = new Actions();
        static Responses Res = new Responses();
        static PlayerCharacter PC = new PlayerCharacter(null, null, null, "hallway");
        static Interactions Interaction = new Interactions();
        static Locations Loc = new Locations();
        static Objects obj = new Objects();
        
        //Main game loop
        static void Main(string[] args)
        {
            GameLogic();
            Main(args);
        }

        //Buffer object to handle screen Input and Output as well as historical savings
        class IOData
        {
            public string ToWrite { get; set; }
            public string ToRead { get; set; }
            public string LastWrite { get; set; }
            public string LastRead { get; set; }
            public string RenderCommand { get; set; }
        }

        //Function to control screen actions
        private static void ScreenControl(string renderCommand)
        {
            switch (renderCommand)
            {
                case "exit":
                    Environment.Exit(0);
                    break;
                case "clear":
                    Console.Clear();
                    Data.ToWrite = Data.LastWrite;
                    ScreenControl("write");
                    break;
                case "read":
                    Data.ToRead = Console.ReadLine().ToLower();
                    break;
                case "write":
                    Console.WriteLine(Data.ToWrite);
                    Data.LastWrite = Data.ToWrite;
                    break;
            }
                
        }

        //Handle start and continue logic
        private static void GameLogic()
        {
            //start of each loop begins with a read from user 
            Data.RenderCommand = "read";
            //Handle game start here and first write to screen
            if (status == "start")
            {
                status = "continued";
                //TODO: load saved games to be handled here
                Data.ToWrite = Loc.GetDescription(PC.Location);
                Data.RenderCommand = "write";
                Res.sassyGreetingCount = 3;
                Interaction.sassyHelpCount = 3;
            }
            //Rest of the game loop
            else
            {
                ScreenControl(Data.RenderCommand);
                Data.LastRead = Data.ToRead;
                string[] cleanedInput = Data.ToRead.Trim().Split(Interaction.delimiters, StringSplitOptions.RemoveEmptyEntries);
                //Reset interactions for each input by user
                Interaction.Action = null;
                Interaction.Object1 = null;
                Interaction.Object2 = null;
                Interaction.Location = null;
                Interaction.Response = null;
                Interaction.Interact = false;
                //handle the input
                if(Data.ToRead == null || Data.ToRead == "")
                {
                    Data.ToWrite = Loc.GetDescription(PC.Location);
                    Data.RenderCommand = "write";
                }
                else
                {
                    foreach (string query in cleanedInput)
                    {
                        Query(query, Array.IndexOf(cleanedInput, query));
                    }
                }  
            }
            ScreenControl(Data.RenderCommand);
        }
        //Handle input from user
        static void Query(string queryIn, int index)
        {
            
            if (index == 0)
            {
                switch (queryIn)
                {
                    //clear screen handling
                    case "clear":
                    //exit program handling
                    case "exit":
                        Data.RenderCommand = queryIn;
                        break;
                    case "quit":
                        Data.RenderCommand = "exit";
                        break;
                    //Handle all of the other input types here
                    default:
                        Data.ToWrite = "I do not know how to do that.";
                        Data.RenderCommand = "write";
                        Interaction.IsHelp(queryIn);
                        Interaction.IsObject(queryIn);
                        Interaction.IsLocation(queryIn);
                        Interaction.IsAction(queryIn);
                        Interaction.IsResponse(queryIn);
                        break;
                }
            }
            else
            {
                Interaction.IsObject(queryIn);              
                Interaction.IsLocation(queryIn);
                Interaction.IsAction(queryIn);
                Interaction.IsResponse(queryIn);
            }
        }

        class Interactions
        {
            public bool Interact { get; set; }
            public string Action { get; set; }
            public string Object1 { get; set; }
            public string Object2 { get; set; }
            public string Location { get; set; }
            public string Response { get; set; }
            
            public string[] delimiters = { " ", "to", "in", "on", "with", "at", ".", ",", ";", "\"", ":"};
            public string[] actions = { "take", "put", "hit", "use", "throw", "look", "go", "drop"};
            public int sassyHelpCount { get; set; }
            private string sassyWarning = "SASSY MODE ACTIVATED";
            //If the help word is given, display help text
            public void IsHelp(string value)
            {
                if(value == "help")
                {
                    if (sassyHelpCount <= 0)
                    {
                        Data.ToWrite = Interaction.sassyWarning + newline + "Help yo damn self, I'm not yo momma!";
                        sassyHelpCount = 3;
                    }
                    else
                    {
                        Data.ToWrite = "Type simple sentences to communicate with Charlie or interact with objects in the room." + newline +
                             "Example: \"Look at Desk\" or \"Hello Charlie!\"";
                        sassyHelpCount -= 1;
                    }
                    Data.RenderCommand = "write";
                }  
            }
            //If the word is a recognized action, set it
            public void IsAction(string value)
            {
                if (actions.Contains(value))
                {
                    Action = value;
                }
                else if(value == "inventory" || value == "i")
                {
                    Action = "inventory";
                }    
                if(Action != null)
                {
                    switch (Action)
                    {
                        case "take":
                            if(Object1 != null)
                            {
                                Act.Take(Object1);
                            }
                            else if (PC.AllItems().Contains(value))
                            {
                                Act.Take(value);
                            }
                            break;
                        case "put":
                            if(Object1 != null && Object2 != null)
                            {
                                Act.Put(Object1, Object2);
                            }
                            else if(Object1 != null && Object2 == null)
                            {
                                Data.ToWrite = "Where would you like me to put the " + Object1 + "?";
                                Data.RenderCommand = "write";
                            }
                            break;
                        case "look":
                            if(Object1 != null)
                            {
                                Act.Look(Object1);
                            }
                            else if(Location != null)
                            {
                                Act.Look(Location);
                            }
                            break;
                        case "use":
                            if(Object1 != null && Object2 != null)
                            {
                                Act.Use(Object1, Object2);
                            }
                            else if(Object1 != null && Object2 == null)
                            {
                                Data.ToWrite = "What would you like me to use the " + Object1 + " on?";
                                Data.RenderCommand = "write";
                            }
                            break;
                        case "go":
                            if(Location != null)
                            {
                                Act.Go(Location);
                            }
                            break;
                        case "drop":
                            if (Object1 != null)
                            {
                                Act.Drop(Object1);
                            }
                            else if (PC.AllItems().Contains(value))
                            {
                                Act.Drop(value);
                            }
                            else if(!Loc.GetContents(PC.Location).Contains(value) && obj.AllObjects().Contains(value))
                            {
                                Data.ToWrite = "That item is nowhere to be found.";
                                Data.RenderCommand = "write";
                            }
                            break;
                        case "inventory":
                            Act.GetInventory();
                            break;
                    }
                }
            }
            //If the word is a recognized object, set it
            public void IsObject(string value)
            {
                if(Object1 == null && Object2 == null)
                {
                    foreach (string x in Loc.GetContents(PC.Location))
                    {
                        if (value == x)
                        {
                            Object1 = x;
                        }
                    }
                }
                else if(Object1 != null && Object2 == null)
                {
                    foreach (string x in Loc.GetContents(PC.Location))
                    {
                        if (value == x)
                        {
                            Object2 = x;
                        }
                    }
                }
                else
                {
                    Data.ToWrite = "I do not know how to do that.";
                    Data.RenderCommand = "write";
                }
                
            }
            //If the word is a recognized location, set it
            public void IsLocation(string value)
            {
                if(Location == null)
                {
                    if(value == "around")
                    {
                        Location = PC.Location;
                    }
                    else
                    {
                        foreach (string x in Loc.AllLocations())
                        {
                            if (value == x)
                            {
                                Location = x;
                            }
                        }
                    }  
                }
                else
                {
                    Data.ToWrite = "I do not know how to do that.";
                    Data.RenderCommand = "write";
                }
                
            }
            //Detect whether a response has been selected and decide how to respond to the user
            public void IsResponse(string value)
            {
                Res.IsResponse(value);
                if(Response != null)
                {
                    Data.ToWrite = Response;
                    Data.RenderCommand = "write";
                }
            }
            //It the exit command is given
            public void IsExit(string value)
            {
                //to do save on exit
            }
        }
        //Action functions
        class Actions
        {
            public void GetInventory()
            {
                string inventory = "";
                int i = 0;
                foreach (string x in PC.AllItems())
                {
                    if (x != null)
                    {
                        switch (i)
                        {
                            case 0:
                                inventory = x;
                                Data.ToWrite = "You have " + inventory + " in your inventory.";
                                Data.RenderCommand = "write";
                                i++;
                                break;
                            case 1:
                                inventory = inventory + " and " + x;
                                Data.ToWrite = "You have " + inventory + " in your inventory.";
                                Data.RenderCommand = "write";
                                i++;
                                break;
                            case 2:
                                inventory = x + "," + inventory.Replace("and", ", and");
                                Data.ToWrite = "You have " + inventory + " in your inventory.";
                                Data.RenderCommand = "write";
                                i++;
                                break;
                        }
                    }
                }
                if (inventory == "")
                {
                    Data.ToWrite = "You have no items in your inventory.";
                    Data.RenderCommand = "write";
                }
                else
                {

                }
            }
            public void Take(string item)
            {
                PC.AddItem(item);
            }
            public void Put(string item, string location)
            {
                //remove item from inventory and place it in the location
            }
            public void Use(string item, string target)
            {
                //if item exists in inventory and target exists in location then perform action, depends on item used
            }
            public void Look(string item)
            {
                if (Loc.GetContents(PC.Location).Contains(item))
                {
                    Data.ToWrite = obj.GetDescription(item);
                    Data.RenderCommand = "write";
                }
                else if(PC.AllItems().Contains(item))
                {
                    Data.ToWrite = obj.GetDescription(item);
                    Data.RenderCommand = "write";
                }
                else
                {
                    Data.ToWrite = "That item is nowhere to be found.";
                    Data.RenderCommand = "write";
                }
            }
            public void Go(string location)
            {
                if(location != null && location != PC.Location)
                {
                    foreach(string x in Loc.Directions(PC.Location))
                    {
                        if(location == x)
                        {
                            Data.ToWrite = "You went " + x + " direction";
                            Data.RenderCommand = "write";
                        }
                    }
                    foreach(string x in Loc.LocationObjects())
                    {
                        if(location == x)
                        {
                            PC.Location = x;
                            Data.ToWrite = Loc.GetDescription(PC.Location);
                            Data.RenderCommand = "write";
                        }
                    }
                }
            }
            public void Drop(string item)
            {
                PC.RemoveItem(item);
            }
        }
        //Location object definition
        class Locations
        {
            static OfficeObject Office = new OfficeObject();
            static HallwayObject Hallway = new HallwayObject();
            //Returns the description of a location object
            public string GetDescription(string location)
            {
                switch (location)
                {
                    case "office":
                        return Office.Description;
                    case "hallway":
                        return Hallway.Description; 
                }
                return "Error in Location.GetDescription";
            }
            //Returns the contents of a location object
            public string[] GetContents(string location)
            {
                string[] contents = { };
                switch (location)
                {
                    case "office":
                        contents = Office.Contains.ToArray();
                        break;
                    case "hallway":
                        contents = Hallway.Contains.ToArray();
                        break;
                }
                return contents;
            }
            public void AddContents(string location, string item)
            {
                switch (location)
                {
                    case "office":
                        Office.Contains.Add(item);
                        break;
                    case "hallway":
                        Hallway.Contains.Add(item);
                        break;
                }
            }
            public void RemoveContents(string location, string item)
            {
                switch (location)
                {
                    case "office":
                        if(Office.Contains.Contains(item))
                        {
                            Office.Contains.Remove(item);
                        }
                        break;
                    case "hallway":
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
            public string[] Directions(string currentLocation)
            {
                string[] pathways = { };
                switch (currentLocation)
                {
                    case "office":
                        pathways = Office.Pathways();
                        break;
                    case "hallway":
                        pathways = Hallway.Pathways();
                        break;
                }
                return pathways;
            }
            public string[] AllLocations()
            {
                string[] all = {"office", "hallway", "n", "ne", "nw", "s", "se", "sw", "e", "w", "north", "west", "east", "south", "northwest", "northeast", "southeast", "southwest"};
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
        //Game objects definition and data
        class Objects
        {
            static Cup cup = new Cup();
            static Chair chair = new Chair();
            static Charlie charlie = new Charlie();
            static Desk desk = new Desk();
            static Note note = new Note();
            static Pen pen = new Pen();
            //Return the descriptions of objects
            public string GetDescription(string item)
            {
                string description = "";
                switch (item)
                {
                    case "desk":
                        description = desk.Description;
                        break;
                    case "note":
                        description = note.Description;
                        break;
                    case "cup":
                        description = cup.Description;
                        break;
                    case "charlie":
                        description = charlie.Description;
                        break;
                    case "chair":
                        description = chair.Description;
                        break;
                    case "pen":
                        description = pen.Description;
                        break;
                }
                return description;
            }
            //Get a list of all the objects to parse
            public string[] AllObjects()
            {
                string[] all = {"desk", "note", "cup", "charlie", "chair", "pen" };
                return all;
            }
            public class Desk
            {
                public string Name = "desk";
                public bool canPickup = false;
                public bool canPut = true;
                public string[] Contains { get; set; }
                public string[] Objects { get; set; }
                public string Description = "A normal wooden desk that has a coffee cup and a note on it.";
                
            }
            public class Note
            {
                public string Name = "note";
                public bool canPickup = true;
                public bool canPut = false;
                public string[] Contains { get; set; }
                public string[] Objects { get; set; }
                public string Description = "Seems to be some scribbling but Charlie's hand writing is so bad you cannot make out the words.";
            }
            public class Cup
            {
                public string Name = "cup";
                public bool canPickup = true;
                public bool canPut = true;
                public string[] Contains { get; set; }
                public string[] Objects { get; set; }
                public string Description = "Normally used for holding coffee but is currently empty.";
            }
            public class Charlie
            {
                public string Name = "charlie";
                public bool canPickup = false;
                public bool canPut = false;
                public string[] Contains { get; set; }
                public string[] Objects { get; set; }
                public string Description = "Charlie Chaplan is a well dressed man. He sits in a chair with a welcoming smile.";
            }
            public class Chair
            {
                public string Name = "chair";
                public bool canPickup = false;
                public bool canPut = false;
                public string[] Contains { get; set; }
                public string[] Objects { get; set; }
                public string Description = "An old office chair worn from being sat in for 8 hours a day.";
            }
            public class Pen
            {
                public string Name = "pen";
                public bool canPickup = false;
                public bool canPut = false;
                public string[] Contains { get; set; }
                public string[] Objects { get; set; }
                public string Description = "A pen of blue ink, good for writing notes.";
            }
        }
        //Response Object
        class Responses
        {
            public bool Greeting { get; set; }
            public string[] greetings = { "hi", "hello", "hey", "howdy", "hola", "sup", "greetings" };
            public string sassyWarning = "SASSY MODE ACTIVATED";

            public int sassyGreetingCount { get; set; }
            public bool sassyMode { get; set; }
            public void IsResponse(string input)
            {
                IsGreeting(input);
                

            }
            public void IsGreeting(string input)
            {
                foreach (string x in greetings)
                {
                    if (x == input)
                    {
                        if (Res.sassyGreetingCount <= 0)
                        {
                            Interaction.Response = Greetings(1);
                        }
                        else
                        {
                            Interaction.Response = Greetings(0);
                        }  
                    }
                }
            }
            public string Greetings(int x)
            {
                string res = "";
                switch (x)
                {
                    case 0:
                        res = "Hello, welcome to my office. How may I help you?";
                        Res.sassyGreetingCount -= 1;
                        break;
                    case 1:
                        res = sassyWarning + newline + "Charlie: BYE FELICIA!";
                        Res.sassyGreetingCount = 3;
                        break;
                }
                return res;
            }
        }
        //Player Character object
        class PlayerCharacter
        {
        //Object constructor
            public PlayerCharacter(string item1, string item2, string item3, string startLocation)
            {
                Slot1 = item1;
                Slot2 = item2;
                Slot3 = item3;
                Location = startLocation;
            }

            //Read and write individual inventory slots
            public string Slot1 { get; set; }
            public string Slot2 { get; set; }
            public string Slot3 { get; set; }
            public string Location { get; set; }

            //Inventory count of all items
            public int ItemCount()
            {
                int count = 0;
                if (Slot1 != null)
                {
                    count += 1;
                }
                if (Slot2 != null)
                {
                    count += 1;
                }
                if (Slot3 != null)
                {
                    count += 1;
                }
                return count;
            }

            //Bulk export of all items in inventory
            public string[] AllItems()
            {
                string[] items = { null, null, null };
                if (Slot1 != null)
                {
                    items[0] = Slot1;
                }
                if (Slot2 != null)
                {
                    items[1] = Slot2;
                }
                if (Slot3 != null)
                {
                    items[2] = Slot3;
                }
                return items;
            }

            public void AddItem(string item)
            {
                bool itemExists = false;
                if (AllItems().Contains(item))
                {
                    Data.ToWrite = "That item already exists in your inventory.";
                    Data.RenderCommand = "write";
                    itemExists = true;
                }   
                if (!itemExists)
                {
                    if(Slot1 == null)
                    {
                        Slot1 = item;
                        Data.ToWrite = item + " has been added to your inventory.";
                        Data.RenderCommand = "write";
                        Loc.RemoveContents(PC.Location, item);
                    }
                    else
                    {
                        if(Slot2 == null)
                        {
                            Slot2 = item;
                            Data.ToWrite = item + " has been added to your inventory.";
                            Data.RenderCommand = "write";
                            Loc.RemoveContents(PC.Location, item);
                        }
                        else
                        {
                            if(Slot3 == null)
                            {
                                Slot3 = item;
                                Data.ToWrite = item + " has been added to your inventory.";
                                Data.RenderCommand = "write";
                                Loc.RemoveContents(PC.Location, item);
                            }
                            else
                            {
                                Data.ToWrite = "Your inventory is full. Please drop an item to pick up this item.";
                                Data.RenderCommand = "write";
                            }
                        }
                    }
                }
            }

            public void RemoveItem(string item)
            {
                bool itemRemoved = false;
                if (AllItems().Contains(item))
                {
                    if (Slot1 == item)
                    {
                        Data.ToWrite = item + " has been dropped.";
                        Data.RenderCommand = "write";
                        Loc.AddContents(PC.Location, item);
                        Slot1 = null;
                        itemRemoved = true;
                    }
                    if (Slot2 == item)
                    {
                        Data.ToWrite = item + " has been dropped.";
                        Data.RenderCommand = "write";
                        Loc.AddContents(PC.Location, item);
                        Slot2 = null;
                        itemRemoved = true;
                    }
                    if (Slot3 == item)
                    {
                        Data.ToWrite = item + " has been dropped.";
                        Data.RenderCommand = "write";
                        Loc.AddContents(PC.Location, item);
                        Slot3 = null;
                        itemRemoved = true;
                    }
                }
                if (!itemRemoved)
                {
                    Data.ToWrite = "You are not carrying that item.";
                    Data.RenderCommand = "write";
                }
            }
        }
    }
}
