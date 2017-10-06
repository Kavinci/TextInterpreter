using System;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

namespace TextInterpreter
{
    class Program
    {
        static bool start = true;
        

        static void Main(string[] args)
        {
            
            if(start == true)
            {
                Console.Clear();
                Console.WriteLine("Charlie is here for you to talk to. Why not say Hello?");
                Console.WriteLine(Parse(Console.ReadLine().ToLower()));
                start = false;
            }
            else
            {
                Console.WriteLine(Parse(Console.ReadLine()));
            }
            Main(args);
        }

        static string Parse(string input)
        {
            string output = "";
            string[] delimiters = {" ", "to", "in", "on", "with"};
            string[] cleanedInput = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            if(input == "clear")
            {
                Console.Clear();
                foreach (string w in cleanedInput)
                {
                    output = Query(w);
                }
                
            }
            else if (input == "exit" || input == "quit")
            {
                output = "Exiting Application";
                Environment.Exit(0);
            }
            else
            {
                foreach (string w in cleanedInput)
                {
                    output = Query(w);
                }
            }
            return output;
        }

        static string Query(string queryIn)
        {
            string output = "";
            XElement response = XElement.Load("Response.xml");
            IEnumerable<XElement> lookup = from el in response.Elements("greeting") where (string)el.Element("keywords") == queryIn select el;
            foreach(string l in lookup)
            {
                output = l;
            }
            return output;
        }
    }
}
