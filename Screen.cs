using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter
{
    //Buffer object to handle screen Input and Output as well as historical savings
    public class Screen
    {
        private string[] Delimiters = { " ", ".", ",", ";", "\"", ":" };
        private string WriteBuffer { get; set; }
        private List<string> ReadBuffer { get; set; }
        private string WriteMemory { get; set; }
        private List<string> ReadMemory { get; set; }
        public List<string> Input
        {
            get { return ReadBuffer; }
        }
        public Screen(string StartingText)
        {
            WriteToBuffer(StartingText);
            ReadMemory = new List<string>() { "NEW" };
            ReadBuffer = new List<string>() { "NEW" };
        }
        public void WriteToBuffer(string value)
        {
            WriteBuffer = value;
        }
        public void Render()
        {
            WriteMemory = null;
            Console.WriteLine(WriteBuffer);
            WriteMemory = WriteBuffer;
        }
        public void ReadToBuffer()
        {
            ReadMemory.Clear();
            ReadMemory = ReadBuffer;
            ReadBuffer.Clear();
            string[] cleanedInput = Console.ReadLine().ToLower().Trim().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
            foreach(string x in cleanedInput)
            {
                ReadBuffer.Add(x);
            }
        }
        public void Clear()
        {
            Console.Clear();
            WriteBuffer = WriteMemory;
            Render();
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
