using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deleteAfterReading.Model
{
    [Serializable]
    public class Disk
    {
        //Time the floppy is dropped onto the screen (maybe not applicable?)
        public int start;
        //Title on the floppy
        public string title;
        //Who the message is to
        public string to;
        //Who the message is from
        public string from;
        //body of the message
        public string text;
        //keywords in the message
        public List<string> keywords;
    }
}
