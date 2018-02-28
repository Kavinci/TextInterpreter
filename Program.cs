using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TextInterpreter
{
    class Program
    {
        static string status = "start";
        static public XElement response = XElement.Load("Response.xml");
        static public IOData Data = new IOData();

        static void Main(string[] args)
        {
            
            if(status == "start")
            {
                status = "continued";
                ScreenControl("start", Data);
            }
            else if (status == "continued")
            {
                ScreenControl("read", Data);
                Parse(Data.toRead, Data);
            }
            Main(args);
        }

        private static void ScreenControl(string renderCommand, IOData Data)
        {
            switch (renderCommand)
            {
                case "start":
                    Console.WriteLine(Data.defaultStartMessage);
                    Data.lastOutput = Data.defaultStartMessage;
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                case "clear":
                    Console.Clear();
                    Data.toWrite = Data.lastOutput;
                    ScreenControl("write", Data);
                    break;
                case "read":
                    Data.toRead = Console.ReadLine();
                    break;
                case "write":
                    Console.WriteLine(Data.toWrite);
                    Data.lastOutput = Data.toWrite;
                    break;
            }
                
        }

        private static void Parse(string input, IOData Data)
        {
            input = input.Trim();
            Data.lastInput = input;
            string output = "";
            string[] delimiters = {" ", "to", "in", "on", "with"};
            string[] cleanedInput = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            
            //clear screen handling
            if(cleanedInput.Count() == 1 && cleanedInput[0] == "clear")
            {
                ScreenControl(cleanedInput[0], Data); 
            }
            //exit application handling
            else if (cleanedInput.Count() == 1 && (cleanedInput[0] == "exit" || cleanedInput[0] == "quit"))
            {
                ScreenControl(cleanedInput[0], Data);
            }
            //all other inputs are assumed to be communication with application
            else
            {
                foreach (string w in cleanedInput)
                {
                    output = Query(w);
                }
            }
        }

        static string Query(string queryIn)
        {
            string output = "";
            
            return output;
        }
    }

    class IOData
    {
        public string defaultStartMessage = "Charlie is here for you to talk to. Why not say Hello?";
        public string toWrite = "";
        public string toRead = "";
        public string lastOutput = "";
        public string lastInput = "";
    }
}
