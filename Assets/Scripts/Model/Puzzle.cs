using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deleteAfterReading.Model
{
    [Serializable]
    public class Puzzle
    {
        public string prompt;
        public string solutionPrompt;
        public List<string> keywords;
    }
}
