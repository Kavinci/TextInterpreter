using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter.Interactions
{
    public class Help
    {
        Sass Sass;
        private string Text = "Help Text";
        public Help()
        {
            Sass = new Sass();
        }
        public string HelpMessage()
        {
            string Message = null;
            Sass.isSass(Text, out Message);
            return Message;
        }
    }
}
