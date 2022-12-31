using System.Linq;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // Tech Saves
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Saves
        public int bulletCount = 3;
        public float bulletExplosionRadiusPower = 10;
        public float fuelTime = 8;

        public int currentLevel = 1;
        
        public int[] levelPoints = new int[100];
        public int spendPoints = 0;

        public int AllPoints => levelPoints.Sum();

        public int LevelPoints
        {
            get => levelPoints[currentLevel - 1];
            set => levelPoints[currentLevel - 1] = value;
        }
    }
}
