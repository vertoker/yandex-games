using System.Linq;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // Tech Saves
        public int idSave;
        public bool isFirstSession;
        public string language;
        public bool promptDone;

        // Saves
        public int bulletCount;
        public float bulletExplosionRadiusPower ;
        public float fuelTime;
        
        public int currentLevel;
        public int maxLevel;
        public int money;
        public int[] levelPoints;

        public SavesYG()
        {
            isFirstSession = true;
            language = "ru";

            bulletCount = 3;
            bulletExplosionRadiusPower = 10;
            fuelTime = 8;
            
            currentLevel = 1;
            maxLevel = 1;
            money = 0;
            levelPoints = new int[100];
        }
    }
}
