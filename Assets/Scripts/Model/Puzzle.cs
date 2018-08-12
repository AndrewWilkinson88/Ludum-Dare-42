using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeleteAfterReading.Model
{
    [Serializable]
    public class Puzzle
    {
        public string prompt;
        public string solutionPrompt;
        public List<string> keywords;
        public string headlineSuccess;
    }
}
