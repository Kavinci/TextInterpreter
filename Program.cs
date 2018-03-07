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
        static Inventory PCI = new Inventory(null, null, null);
        static Interactions Interaction = new Interactions();
        static Locations.Office O = new Locations.Office();
        static Locations.Hallway H = new Locations.Hallway();

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
                Interaction.Object = null;
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
            public string Object { get; set; }
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
                        Action = value;
                    }
                }
                if(Action != null)
                {
                    switch (value)
                    {
                        case "take":
                            break;
                        case "put":
                            break;
                        case "look":
                            break;
                        case "use":
                            break;
                        case "go":
                            break;
                        case "drop":
                            break;
                    }
                }
            }
            public void IsObject(string value)
            {


            }
            public void IsLocation(string value)
            {

            }
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
            public void Take(string item)
            {
                PCI.AddItem(item);
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
                //make sure item exists in location then display info on item
            }
            public void Go(string location)
            {
                //If location is adjacent with no obstacles then move location
                //Possible addition for "fast travel"
            }
            public void Drop(string item)
            {
                PCI.RemoveItem(item);
            }
        }    
        class Locations
        {
            public class Office
            {
                public string[] Contains { get; set; }
                public string[] Objects = { "desk", "note", "cup", "chair", "charlie", "pen" };
                public string Description = "";
            }
            public class Hallway
            {
                public string[] Contains { get; set; }
                public string[] Objects = { };
                public string Description = "";
            }
        }
        
        
        class Objects
        {
         
        }
        class Responses
        {
            public string defaultStartMessage = "You are in a small room. In front of you is a plain looking wooden desk. \r\n" +
                "Charlie sits at the desk. Feel free to say hello.";
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
        class Inventory
        {
        //Object constructor
            public Inventory(string item1, string item2, string item3)
            {
                Slot1 = item1;
                Slot2 = item2;
                Slot3 = item3;
            }

            //Read and write individual inventory slots
            public string Slot1 { get; set; }
            public string Slot2 { get; set; }
            public string Slot3 { get; set; }

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
