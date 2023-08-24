using System.Collections.Generic;
using System.Linq;

namespace Data
{
    [System.Serializable]
    public class LevelData
    {
        public bool completed;
        public int points;
        public int errors;

        public static int Sum(IEnumerable<LevelData> levels)
        {
            return (from level in levels 
                where !level.completed 
                select level.GetNormalizedScore())
                .Sum();
        }

        public int GetNormalizedScore()
        {
            return points < errors ? 0 :  points - errors;
        }
    }
}