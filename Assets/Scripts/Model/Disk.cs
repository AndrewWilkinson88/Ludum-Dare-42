using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deleteAfterReading.Model
{
    [Serializable]
    public class Disk
    {
        public int start;
        public string to;
        public string from;
        public string text;
        public List<string> keywords;
    }
}
