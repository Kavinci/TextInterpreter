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
        public string Respond(string input)
        {
            return input;
        }
        public string Greetings(int x)
        {
            string res = "Hello, welcome to my office. How may I help you?";
            return res;
        }
    }
}
