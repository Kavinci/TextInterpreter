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
        
        static PlayerCharacter PC = new PlayerCharacter(null, null, null, "office");
        static Interactions Interaction = new Interactions();
        static Responses Res = new Responses(Interaction);
        static Locations Loc = new Locations();


        //Main game loop
        static void Main(string[] args)
        {
            GameLogic();
            Main(args);
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
                if (Data.ToRead == null || Data.ToRead == "")
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
    }
}
