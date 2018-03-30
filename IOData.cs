using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    //Buffer object to handle screen Input and Output as well as historical savings
    class IOData
    {
        public string ToWrite { get; set; }
        public string ToRead { get; set; }
        public string LastWrite { get; set; }
        public string LastRead { get; set; }
        public string RenderCommand { get; set; }
    }
}
