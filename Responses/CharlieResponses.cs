using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Responses
{
    public class CharlieResponses
    {
        public const string End = null;
        public const string _000 = "Charlie: Hello, welcome to my office. How may I help you?" + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) Can you give me some information?" + CommonValues.NewLine + //Jump to 001
                        "B) Where are we?" + CommonValues.NewLine + //Jump to 002
                        "C) Exit Conversation";
        public const string _001 = "Charlie: Sure, what would you like to know?" + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) Why are we here?" + CommonValues.NewLine + //Jump to 011
                        "B) What can I do here?" + CommonValues.NewLine + //Jump to 021
                        "C) Exit Conversation";
        public const string _002 = "Charlie: We are in a video game. A sort of text adventure if you will. " + CommonValues.NewLine +
                        "Currently there are only 2 rooms, my office and the hallway to the West." + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) Can I go to the hallway?" + CommonValues.NewLine + //Jump to 012
                        "B) Who created you?" + CommonValues.NewLine + //Jump to 022
                        "C) Exit Conversation";
        public const string _011 = "Charlie: We were put here by a brilliant programmer of video games." + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) What is their name?" + CommonValues.NewLine + //Jump to 111
                        "B) Brilliant, eh? Sound more like errogant to me." + CommonValues.NewLine + //Jump to 211
                        "C) Exit Conversation";
        public const string _012 = "Charlie: Of course! Just exit this conversation and type \"Go West\". " + CommonValues.NewLine +
                        "That should take you there but sadly there isn't much out there." + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) Thank You!" + CommonValues.NewLine + //end here
                        "B) Who created you?" + CommonValues.NewLine + //Jump to 111
                        "C) Exit Conversation";
        public const string _021 = "Charlie: You can do all sorts of things like \"Take\" things from my desk, " + CommonValues.NewLine +
                        "\"Drop\" them, or \"Put\" small objects inside biger objects." + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) Thank You!" + CommonValues.NewLine + //end here
                        "B) Who created you?" + CommonValues.NewLine + //Jump to 111
                        "C) Exit Conversation";
        public const string _022 = "Charlie: We were put here by a brilliant programmer of video games." + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) What is their name?" + CommonValues.NewLine + //Jump to 111
                        "B) Brilliant, eh? Sound more like errogant to me." + CommonValues.NewLine + //Jump to 211
                        "C) Exit Conversation";
        public const string _111 = "Charlie: The creator of this game is named, Kent. He created me and all you read here." + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) I have more questions." + CommonValues.NewLine + //Jump to 001
                        "B) What can I do here?" + CommonValues.NewLine + //Jump to 021
                        "C) Exit Conversation";
        public const string _211 = "Charlie: HAHAHAHAHA! Yeah, he can be at times." + CommonValues.NewLine +
                        "Select a letter" + CommonValues.NewLine +
                        "A) I have more questions." + CommonValues.NewLine + //Jump to 001
                        "B) What can I do here?" + CommonValues.NewLine + //Jump to 021
                        "C) Exit Conversation";
        public CharlieResponses() { }
        public string NextResponse(string input, int lastResponse, out int Next)
        {
            Next = 000;
            switch (lastResponse)
            {
                case 000:
                    switch (input)
                    {
                        case "a":
                            Next = 001;
                            break;
                        case "b":
                            Next = 002;
                            break;
                    }
                    break;
                case 001:
                    switch (input)
                    {
                        case "a":
                            Next = 011;
                            break;
                        case "b":
                            Next = 021;
                            break;
                    }
                    break;
                case 002:
                    switch (input)
                    {
                        case "a":
                            Next = 012;
                            break;
                        case "b":
                            Next = 022;
                            break;
                    }
                    break;
                case 011:
                    switch (input)
                    {
                        case "a":
                            Next = 111;
                            break;
                        case "b":
                            Next = 211;
                            break;
                    }
                    break;
                case 012:
                    switch (input)
                    {
                        case "a":
                            Next = 1000;
                            break;
                        case "b":
                            Next = 111;
                            break;
                    }
                    break;
                case 021:
                    switch (input)
                    {
                        case "a":
                            Next = 1000;
                            break;
                        case "b":
                            Next = 111;
                            break;
                    }
                    break;
                case 022:
                    switch (input)
                    {
                        case "a":
                            Next = 111;
                            break;
                        case "b":
                            Next = 211;
                            break;
                    }
                    break;
                case 111:
                    switch (input)
                    {
                        case "a":
                            Next = 001;
                            break;
                        case "b":
                            Next = 021;
                            break;
                    }
                    break;
                case 211:
                    switch (input)
                    {
                        case "a":
                            Next = 001;
                            break;
                        case "b":
                            Next = 021;
                            break;
                    }
                    break;
            }
            return GetResponse(Next);
        }
        private string GetResponse(int responseNumber)
        {
            switch (responseNumber)
            {
                case 000:
                    return _000;
                case 001:
                    return _001;
                case 002:
                    return _002;
                case 011:
                    return _011;
                case 012:
                    return _012;
                case 021:
                    return _021;
                case 022:
                    return _022;
                case 111:
                    return _111;
                case 211:
                    return _211;
                default:
                    return null;
            }
        }
    }
}
