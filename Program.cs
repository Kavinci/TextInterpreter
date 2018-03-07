using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TextInterpreter
{
    class Program
    {
        static string status = "start";
        //static XElement response = XElement.Load("Response.xml");
        static IOData Data = new IOData();
        static Locations Loc = new Locations();
        static Actions Act = new Actions();
        static Responses Res = new Responses();
        static PlayerCharacter PC = new PlayerCharacter(null, null, null);
        static Interactions Interaction = new Interactions();
        static Locations.Office O = new Locations.Office();
        static Locations.Hallway H = new Locations.Hallway();
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

        //
        private static void GameLogic()
        {
            //start of each loop begins with a read from user 
            Data.RenderCommand = "read";
            //Handle game start here and first write to screen
            if (status == "start")
            {
                status = "continued";
                //TODO: load saved games to be handled here
                Data.ToWrite = Res.defaultStartMessage;
                Data.RenderCommand = "write";
                Res.sassyGreetingCount = 3;
                Res.sassyHelpCount = 3;
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
                Interaction.Interact = false;
                Interaction.Greeting = false;
                //handle the input
                foreach (string query in cleanedInput)
                {
                    Query(query, Array.IndexOf(cleanedInput, query));
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
            public string Action { get; set; }
            public string Object1 { get; set; }
            public string Object2 { get; set; }
            public string Location { get; set; }
            public bool Greeting { get; set; }
            public string[] greetings = { "hi", "hello", "hey", "howdy", "hola", "sup", "greetings"};
            public string[] delimiters = { " ", "to", "in", "on", "with", "at", ".", ",", ";", "\"", ":"};
            public string[] actions = { "take", "put", "hit", "use", "throw", "look", "go", "drop"};
            public string[] objects = { "cup", "desk", "note", "charlie", "chair"};
            public bool Interact { get; set; }
            public void IsGreeting(string value)
            {
                foreach(string x in greetings)
                {
                    if(x == value)
                    {
                        Greeting = true;
                    }
                }  
            }
            public void IsHelp(string value)
            {
                if(value == "help")
                {
                    if (Res.sassyHelpCount <= 0)
                    {
                        Res.sassyMode = true;
                    }
                    else
                    {
                        Res.sassyMode = false;
                    }
                    if (Res.sassyMode)
                    {
                        Data.ToWrite = Res.sassyWarning + "\r\n" + Res.sassyHelp;
                        Res.sassyMode = false;
                        Res.sassyHelpCount = 4;
                    }
                    else
                    {
                        Data.ToWrite = Res.help;
                    }
                    Res.sassyHelpCount -= 1;
                    Data.RenderCommand = "write";
                }  
            }
            public void IsAction(string value)
            {
                foreach(string x in actions)
                {
                    if(value == x)
                    {
                        Action = x;
                    }
                }
                if(Action != null)
                {
                    switch (Action)
                    {
                        case "take":
                            Act.Take(Object1);
                            break;
                        case "put":
                            Act.Put(Object1, Object2);
                            break;
                        case "look":
                            Act.Look(Object1);
                            break;
                        case "use":
                            Act.Use(Object1, Object2);
                            break;
                        case "go":
                            Act.Go(Location);
                            break;
                        case "drop":
                            Act.Take(Object1);
                            break;
                        case "inventory":
                        case "i":
                            Act.GetInventory();
                            break;
                    }
                }
            }
            //If the word is a recognized object, add it.
            public void IsObject(string value)
            {
                if(Object1 == null && Object2 == null)
                {
                    foreach (string x in objects)
                    {
                        if (value == x)
                        {
                            Object1 = x;
                        }
                    }
                }
                else if(Object1 != null && Object2 == null)
                {
                    foreach (string x in objects)
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
            public void IsLocation(string value)
            {
                if(Location == null)
                {
                    foreach (string x in Loc.AllLocations())
                    {
                        if (value == x)
                        {
                            Location = x;
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
                IsGreeting(value);
                if (Greeting)
                {
                    if (Res.sassyGreetingCount <= 0)
                    {
                        Res.sassyMode = true;
                    }
                    else
                    {
                        Res.sassyMode = false;
                    }
                    if (Res.sassyMode)
                    {
                        Data.ToWrite = Res.sassyWarning + "\r\n" + Res.sassyGreeting;
                        Res.sassyMode = false;
                        Res.sassyGreetingCount = 4;
                    }
                    else
                    {
                        Data.ToWrite = Res.greeting;

                    }
                    Res.sassyGreetingCount -= 1;
                    Data.RenderCommand = "write";
                }
                
            }
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
                    if(x != null && i == 0)
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
                if(inventory == "")
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
                foreach(string x in obj.AllObjects())
                {
                    if(item == x)
                    {
                        Data.ToWrite = obj.GetDescription(item);
                        Data.RenderCommand = "write";
                    }
                }
                foreach(string x in Loc.AllLocations())
                {
                    if (item == x)
                    {
                        Data.ToWrite = Loc.GetDescription(item);
                        Data.RenderCommand = "write";
                    }
                }
            }
            public void Go(string location)
            {
                if(location != null && location != PC.Location)
                {
                    foreach(string x in Loc.Directions)
                    {
                        if(location == x)
                        {
                            Data.ToWrite = "You went " + x + " direction";
                            Data.RenderCommand = "write";
                        }
                    }
                    foreach(string x in Loc.AllLocations())
                    {
                        if(location == x)
                        {
                            Data.ToWrite = "You went to the " + x;
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
            public string GetDescription(string location)
            {
                string description = "";
                switch (location)
                {
                    case "office":
                        description = O.Description;
                        break;
                    case "hallway":
                        description = H.Description;
                        break; 
                }
                return description;
            }
            public string[] Directions = { "n", "ne", "nw", "s", "se", "sw", "e", "w", "north", "west", "east", "south" };
            public string[] AllLocations()
            {
                string[] all = {"office", "hallway", "n", "ne", "nw", "s", "se", "sw", "e", "w", "north", "west", "east", "south", "northwest", "northeast", "southeast",
                    "southwest", };
                return all;
            }
            public class Office
            {
                public Office()
                {
                    string[] items = { };
                    foreach(string x in Objects)
                    {
                        items.Append(x);
                    }
                    Contains = items;
                }
                public string Name = "office";
                public string[] Contains { get; set; }//Objects collected during the game
                public string[] Objects = { "desk", "note", "cup", "chair", "charlie", "pen" };//Objects to start the game with
                public string Description = "You are in a small room. In front of you is a plain looking wooden desk. \r\n" +
                    "Charlie sits at the desk. Feel free to say hello or to look around the room.";
                //Navigation data
                public string GoNorth = null;
                public string GoNorthEast = null;
                public string GoEast = null;
                public string GoSouthEast = null;
                public string GoSouth = null;
                public string GoSouthWest = null;
                public string GoWest = "hallway";
                public string GoNorthWest = null;
                public string[,] Pathways()
                {
                    string[,] possible = new string[8, 2];
                    possible[1, 0] = "north";
                    possible[2, 0] = "northeast";
                    possible[3, 0] = "east";
                    possible[4, 0] = "southeast";
                    possible[5, 0] = "south";
                    possible[6, 0] = "southwest";
                    possible[7, 0] = "west";
                    possible[8, 0] = "northwest";
                    possible[1, 1] = null;
                    possible[2, 1] = null;
                    possible[3, 1] = null;
                    possible[4, 1] = null;
                    possible[5, 1] = null;
                    possible[6, 1] = null;
                    possible[7, 1] = null;
                    possible[8, 1] = null;
                    if (GoNorth != null)
                    {
                        possible[1, 1] = GoNorth;
                    }
                    if (GoNorthEast != null)
                    {
                        possible[2, 1] = GoNorthEast;
                    }
                    if (GoEast != null)
                    {
                        possible[3, 1] = GoEast;
                    }
                    if (GoSouthEast != null)
                    {
                        possible[4, 1] = GoSouthEast;
                    }
                    if (GoSouth != null)
                    {
                        possible[5, 1] = GoSouth;
                    }
                    if (GoSouthWest != null)
                    {
                        possible[6, 1] = GoSouthWest;
                    }
                    if (GoWest != null)
                    {
                        possible[7, 1] = GoWest;
                    }
                    if (GoNorthWest != null)
                    {
                        possible[8, 1] = GoNorthWest;
                    }
                    return possible;
                }
            }
            public class Hallway
            {
                public Hallway()
                {
                    string[] items = { };
                    foreach (string x in Objects)
                    {
                        items.Append(x);
                    }
                    Contains = items;
                }
                public string Name = "hallway";
                public string[] Contains { get; set; }//Objects collected during the game
                public string[] Objects = { };//Objects to start the game with
                public string Description = "It's a hallway. It runs North to South and seems to lead to nowhere. \r\n Behind" +
                    "you to the East is a door with the name Charlie Chaplon in vinyl lettering.";
                //Navigation data
                public string GoNorth = null;
                public string GoNorthEast = null;
                public string GoEast = "office";
                public string GoSouthEast = null;
                public string GoSouth = null;
                public string GoSouthWest = null;
                public string GoWest = null;
                public string GoNorthWest = null;
                public string[,] Pathways()
                {
                    string[,] possible = new string[8, 2];
                    possible[1, 0] = "north";
                    possible[2, 0] = "northeast";
                    possible[3, 0] = "east";
                    possible[4, 0] = "southeast";
                    possible[5, 0] = "south";
                    possible[6, 0] = "southwest";
                    possible[7, 0] = "west";
                    possible[8, 0] = "northwest";
                    possible[1, 1] = null;
                    possible[2, 1] = null;
                    possible[3, 1] = null;
                    possible[4, 1] = null;
                    possible[5, 1] = null;
                    possible[6, 1] = null;
                    possible[7, 1] = null;
                    possible[8, 1] = null;
                    if (GoNorth != null)
                    {
                        possible[1, 1] = GoNorth;
                    }
                    if (GoNorthEast != null)
                    {
                        possible[2, 1] = GoNorthEast;
                    }
                    if (GoEast != null)
                    {
                        possible[3, 1] = GoEast;
                    }
                    if (GoSouthEast != null)
                    {
                        possible[4, 1] = GoSouthEast;
                    }
                    if (GoSouth != null)
                    {
                        possible[5, 1] = GoSouth;
                    }
                    if (GoSouthWest != null)
                    {
                        possible[6, 1] = GoSouthWest;
                    }
                    if (GoWest != null)
                    {
                        possible[7, 1] = GoWest;
                    }
                    if (GoNorthWest != null)
                    {
                        possible[8, 1] = GoNorthWest;
                    }
                    return possible;
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
        class Responses
        {
            public string defaultStartMessage = "You are in a small room. In front of you is a plain looking wooden desk. \r\n" +
                "Charlie sits at the desk. Feel free to say hello or look around the room.";
            public string greeting = "Hello, welcome to my office. How may I help you?";
            public string help = "Type simple sentences to communicate with Charlie or interact with objects in the room. \r\n" +
                "Example: \"Look at Desk\" or \"Hello Charlie!\"";
            public string sassyWarning = "SASSY MODE ACTIVATED";
            public string sassyGreeting = "Charlie: BYE FELICIA!";
            public string sassyHelp = "Help yo damn self, I'm not yo momma!";
            public int sassyHelpCount { get; set; }
            public int sassyGreetingCount { get; set; }
            public bool sassyMode { get; set; }
            private string SpeechType { get; set; }
        
            public void IsSpeechType(string type)
            {
                switch (type)
                {
                    case "Noun":
                        break;
                    case "Verb":
                        break;
                    case "Conjunction":
                        break;
                    case "Preposition":
                        break;
                    case "Adjective":
                        break;
                }
                
            }
       
        }
        class PlayerCharacter
        {
        //Object constructor
            public PlayerCharacter(string item1, string item2, string item3)
            {
                Slot1 = item1;
                Slot2 = item2;
                Slot3 = item3;
                Location = "office";
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
                foreach(string x in AllItems())
                {
                    if(x == item)
                    {
                        Data.ToWrite = "That item already exists in your inventory.";
                        Data.RenderCommand = "write";
                        itemExists = true;
                    }
                }
                if (!itemExists)
                {
                    foreach(string x in AllItems())
                    {
                        if(x == null)
                        {
                            switch(Array.IndexOf(AllItems(), x))
                            {
                                case 0:
                                    Slot1 = item;
                                    Data.ToWrite = item + " has been added to your inventory.";
                                    Data.RenderCommand = "write";
                                    continue;
                                case 1:
                                    Slot2 = item;
                                    Data.ToWrite = item + " has been added to your inventory.";
                                    Data.RenderCommand = "write";
                                    continue;
                                case 2:
                                    Slot3 =item;
                                    Data.ToWrite = item + " has been added to your inventory.";
                                    Data.RenderCommand = "write";
                                    continue;
                            }
                        }
                        else
                        {
                            Data.ToWrite = "Your inventory is full. Please drop an item to pick up this item.";
                            Data.RenderCommand = "write";
                        }
                    }
                }
            }

            public void RemoveItem(string item)
            {
                bool itemRemoved = false;
                foreach(string x in AllItems())
                {
                    if(x == item)
                    {
                        switch(Array.IndexOf(AllItems(), x))
                        {
                            case 0:
                                Slot1 = null;
                                itemRemoved = true;
                                Data.ToWrite = item + " has been dropped.";
                                Data.RenderCommand = "write";
                                break;
                            case 1:
                                Slot1 = null;
                                itemRemoved = true;
                                Data.ToWrite = item + " has been dropped.";
                                Data.RenderCommand = "write";
                                break;
                            case 2:
                                Slot1 = null;
                                itemRemoved = true;
                                Data.ToWrite = item + " has been dropped.";
                                Data.RenderCommand = "write";
                                break;
                        }
                    }
                }
                if (!itemRemoved)
                {
                    Data.ToWrite = "You are not carrying a " + item;
                    Data.RenderCommand = "write";
                }
            }
        }
    }
}
