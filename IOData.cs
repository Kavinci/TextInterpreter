using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    //Buffer object to handle screen Input and Output as well as historical savings
    public class IOData
    {
        private string ToWrite { get; set; }
        private string ToRead { get; set; }
        private string LastWrite { get; set; }
        private string LastRead { get; set; }
        private string RenderCommand { get; set; }
        public void SetWrite(string value)
        {

        }
    }
}
