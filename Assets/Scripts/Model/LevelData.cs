using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeleteAfterReading.Model
{
    [Serializable]
    public class LevelData
    {
        public Array availableTools;
        public int availableSpace;
        public int timeToSolve;
        public Puzzle puzzle;
        public List<Disk> disks;
    }
}
