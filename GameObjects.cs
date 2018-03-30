using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class GameObjects
    {//Game objects definition and data

        static Cup cup = new Cup();
        static Chair chair = new Chair();
        static Charlie charlie = new Charlie();
        static Desk desk = new Desk();
        static Note note = new Note();
        static Pen pen = new Pen();
        //Return the descriptions of objects
        public string GetDescription(string item)
        {
            string description = "";
            switch (item)
            {
                case "desk":
                    description = desk.Description;
                    break;
                case "note":
                    description = note.Description;
                    break;
                case "cup":
                    description = cup.Description;
                    break;
                case "charlie":
                    description = charlie.Description;
                    break;
                case "chair":
                    description = chair.Description;
                    break;
                case "pen":
                    description = pen.Description;
                    break;
            }
            return description;
        }
        //Get a list of all the objects to parse
        public string[] AllObjects()
        {
            string[] all = { "desk", "note", "cup", "charlie", "chair", "pen" };
            return all;
        }
        public class Desk
        {
            public string Name = "desk";
            public bool canPickup = false;
            public bool canPut = true;
            public string[] Contains { get; set; }
            public string[] Objects { get; set; }
            public string Description = "A normal wooden desk that has a coffee cup and a note on it.";

        }
        public class Note
        {
            public string Name = "note";
            public bool canPickup = true;
            public bool canPut = false;
            public string[] Contains { get; set; }
            public string[] Objects { get; set; }
            public string Description = "Seems to be some scribbling but Charlie's hand writing is so bad you cannot make out the words.";
        }
        public class Cup
        {
            public string Name = "cup";
            public bool canPickup = true;
            public bool canPut = true;
            public string[] Contains { get; set; }
            public string[] Objects { get; set; }
            public string Description = "Normally used for holding coffee but is currently empty.";
        }
        public class Charlie
        {
            public string Name = "charlie";
            public bool canPickup = false;
            public bool canPut = false;
            public string[] Contains { get; set; }
            public string[] Objects { get; set; }
            public string Description = "Charlie Chaplan is a well dressed man. He sits in a chair with a welcoming smile.";
        }
        public class Chair
        {
            public string Name = "chair";
            public bool canPickup = false;
            public bool canPut = false;
            public string[] Contains { get; set; }
            public string[] Objects { get; set; }
            public string Description = "An old office chair worn from being sat in for 8 hours a day.";
        }
        public class Pen
        {
            public string Name = "pen";
            public bool canPickup = false;
            public bool canPut = false;
            public string[] Contains { get; set; }
            public string[] Objects { get; set; }
            public string Description = "A pen of blue ink, good for writing notes.";
        }

    }
}
