using System.Linq;

namespace Data
{
    [System.Serializable]
    public struct LevelData
    {
        public bool completed;
        public int points;
        public int errors;

        public static int Sum(LevelData[] levels)
        {
            return levels.Sum(level => level.points - level.errors);
        }
    }
}