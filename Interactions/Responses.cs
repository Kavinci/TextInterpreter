using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter.Interactions
{
    public class Responses
    {

        public Responses()
        {

        }
        public string[] greetings = { "hi", "hello", "hey", "howdy", "hola", "sup", "greetings" };
        public string sassyWarning = "SASSY MODE ACTIVATED";  
        public string Greetings(int x)
        {
            string res = "";
            switch (x)
            {
                case 0:
                    res = "Hello, welcome to my office. How may I help you?";
                    break;
                case 1:
                    res = sassyWarning + "Charlie: BYE FELICIA!";
                    break;
            }
            return res;
        }
    }
}
