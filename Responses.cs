using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    //Response Object
    class Responses
    {
        Interactions Interaction;
        public Responses(Interactions Interact)
        {
            Interaction = Interact;
        }
        public const string newline = "\r\n";
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
                    if (sassyGreetingCount <= 0)
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
                    sassyGreetingCount -= 1;
                    break;
                case 1:
                    res = sassyWarning + newline + "Charlie: BYE FELICIA!";
                    sassyGreetingCount = 3;
                    break;
            }
            return res;
        }
    }
}
