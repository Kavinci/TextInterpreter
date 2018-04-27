using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Interactions
{
    public class Sass
    {
        public string Warning = "SASSY MODE ACTIVATED";
        private int HelpCount { get; set; }
        private int DefaultCount = 3;
        public Sass()
        {
            HelpCount = DefaultCount;
        }
        public void isSass(string phrase, out string sass)
        {
            if(HelpCount <= 0)
            {
                sass = Warning + CommonValues.NewLine + "Help yo damn self! I ain't yo momma!";
                HelpCount = DefaultCount;
            }
            else
            {
                sass = phrase;
                HelpCount--;
            }
        }
    }
}
