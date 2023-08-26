using System.Collections.Generic;
using System.Linq;

namespace Data
{
    [System.Serializable]
    public class LevelData
    {
        public bool completed;
        public int maxPoints;
        public int points;
        public int errors;

        public static int Sum(IEnumerable<LevelData> levels)
        {
            return (from level in levels select level.maxPoints).Sum();
        }

        public void Save()
        {
            var newMaxPoints = GetNormalizedScore();
            if (newMaxPoints > maxPoints)
                maxPoints = newMaxPoints;
        }
        public int GetNormalizedScore()
        {
            return points < errors ? 0 :  points - errors;
        }
    }
}