using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TextInterpreter
{
    class Program
    {
        static string status = "start";
        static XElement response = XElement.Load("Response.xml");
        static IOData Data = new IOData();
        static Responses Res = new Responses();
        static Inventory Inventory = new Inventory(null, null, null);

        static void Main(string[] args)
        {
            Parse();
            Main(args);
        }

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

        private static void Parse()
        {
            

            //Handle game start here TODO: saved games to be handled here
            if (status == "start")
            {
                status = "continued";
                Data.ToWrite = Res.defaultStartMessage;
                ScreenControl("write");
                Res.sassyCount = 3;
            }

            //Rest of the game loop
            else
            {
                ScreenControl("read");
                string input = Data.ToRead.Trim();
                string[] cleanedInput;
                Data.LastRead = input;
                cleanedInput = input.Split(Res.Delimiters(), StringSplitOptions.RemoveEmptyEntries);

                //clear screen handling
                if (cleanedInput.Count() == 1 && cleanedInput[0] == "clear")
                {
                    ScreenControl(cleanedInput[0]);
                }
                //exit application handling
                else if (cleanedInput.Count() == 1 && (cleanedInput[0] == "exit" || cleanedInput[0] == "quit"))
                {
                    ScreenControl(cleanedInput[0]);
                }
                //all other inputs are assumed to be communication with application
                else
                {
                    int i = 0;
                    foreach (string query in cleanedInput)
                    {
                        Query(query, i);
                        i++;
                    }
                }

            }
            
        }

        static void Query(string queryIn, int count)
        {
            switch (queryIn)
            {
                case "hi":
                case "hello":
                case "hey":
                    if(Res.sassyCount <= 0)
                    {
                        Res.sassyMode = true;
                    }
                    else
                    {
                        Res.sassyMode = false;
                    }
                    if (Res.sassyMode)
                    {
                        Data.ToWrite = Res.sassyWarning;
                        ScreenControl("write");
                        Data.ToWrite = Res.sassyGreeting;
                        ScreenControl("write");
                        Res.sassyMode = false;
                        Res.sassyCount = 3;
                    }
                    else
                    {
                        Data.ToWrite = Res.greeting;
                        ScreenControl("write");
                    }
                    Res.sassyCount -= 1;
                    break;
                case "help":
                    if (Res.sassyCount <= 0)
                    {
                        Res.sassyMode = true;
                    }
                    else
                    {
                        Res.sassyMode = false;
                    }
                    if (Res.sassyMode)
                    {
                        Data.ToWrite = Res.sassyWarning;
                        ScreenControl("write");
                        Data.ToWrite = Res.sassyHelp;
                        ScreenControl("write");
                        Res.sassyMode = false;
                        Res.sassyCount = 3;
                    }
                    else
                    {
                        Data.ToWrite = Res.help;
                        ScreenControl("write");
                    }
                    Res.sassyCount -= 1;
                    break;

            }
            
        }
    }

    //Buffer object to handle screen Input and Output
    class IOData
    {
        public string ToWrite { get; set; }
        public string ToRead { get; set; }
        public string LastWrite { get; set; }
        public string LastRead { get; set; }
    }

    class Responses
    {
        public string defaultStartMessage = "Charlie is here for you to talk to. Why not say Hello?";
        private enum SpeechTypes
        {
            Noun,
            Verb,
            Conjunction,
            Preposition,
            Adjective
        }

        private enum InteractionTypes
        {
            Greeting,
            Delimiter,
            Action,
            Response,
            Object
        }

        public string[] Delimiters()
        {
           string[] del = { " ", "to", "in", "on", "with" };
           return del;
        }

        public string greeting = "Hello, welcome to my realm. Do you have any questions for me?";
        public string help = "Type simple sentences to communicate with Charlie the AI.";
        public int sassyCount { get; set; }
        public bool sassyMode { get; set; }
        public string sassyWarning = "SASSY MODE ACTIVATED";
        public string sassyGreeting = "BYE FELICIA!";
        public string sassyHelp = "Help yo damn self, I'm not yo momma!";
    }
    public class Inventory
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

    }
}
