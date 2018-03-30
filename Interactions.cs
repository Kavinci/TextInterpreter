using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class Interactions
    {
        Actions Act;
        IOData Data;
        PlayerCharacter PC;
        GameObjects obj;
        Locations Loc;
        Responses Res;
        public Interactions(Actions Action, IOData iO, PlayerCharacter Character, GameObjects Object, Locations Location, Responses Response)
        {
            Act = Action;
            Data = iO;
            PC = Character;
            obj = Object;
            Loc = Location;
            Res = Response;
        }
        public const string newline = "\r\n";
        public bool Interact { get; set; }
        public string Action { get; set; }
        public string Object1 { get; set; }
        public string Object2 { get; set; }
        public string Location { get; set; }
        public string Response { get; set; }

        public string[] delimiters = { " ", "to", "in", "on", "with", "at", ".", ",", ";", "\"", ":" };
        public enum actions
        {
            Take,
            Put,
            Hit,
            Use,
            Throw,
            Look,
            Go,
            Drop
        };
        public int sassyHelpCount { get; set; }
        private string sassyWarning = "SASSY MODE ACTIVATED";
        //If the help word is given, display help text
        public void IsHelp(string value)
        {
            if (value == "help")
            {
                if (sassyHelpCount <= 0)
                {
                    Data.ToWrite = sassyWarning + newline + "Help yo damn self, I'm not yo momma!";
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
            if (value == Action)
            {
                Action = value;
            }
            else if (value == "inventory" || value == "i")
            {
                Action = "inventory";
            }
            if (Action != null)
            {
                switch (Action)
                {
                    case "take":
                        if (Object1 != null)
                        {
                            Act.Take(Object1);
                        }
                        else if (PC.AllItems().Contains(value))
                        {
                            Act.Take(value);
                        }
                        break;
                    case "put":
                        if (Object1 != null && Object2 != null)
                        {
                            Act.Put(Object1, Object2);
                        }
                        else if (Object1 != null && Object2 == null)
                        {
                            Data.ToWrite = "Where would you like me to put the " + Object1 + "?";
                            Data.RenderCommand = "write";
                        }
                        break;
                    case "look":
                        if (Object1 != null)
                        {
                            Act.Look(Object1);
                        }
                        else if (Location != null)
                        {
                            Act.Look(Location);
                        }
                        break;
                    case "use":
                        if (Object1 != null && Object2 != null)
                        {
                            Act.Use(Object1, Object2);
                        }
                        else if (Object1 != null && Object2 == null)
                        {
                            Data.ToWrite = "What would you like me to use the " + Object1 + " on?";
                            Data.RenderCommand = "write";
                        }
                        break;
                    case "go":
                        if (Location != null)
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
                        else if (!Loc.GetContents(PC.Location).Contains(value) && obj.AllObjects().Contains(value))
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
            if (Object1 == null && Object2 == null)
            {
                foreach (string x in Loc.GetContents(PC.Location))
                {
                    if (value == x)
                    {
                        Object1 = x;
                    }
                }
            }
            else if (Object1 != null && Object2 == null)
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
            if (Location == null)
            {
                if (value == "around")
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
            if (Response != null)
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
}
