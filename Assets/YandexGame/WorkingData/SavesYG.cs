
using Scripts.Game.Data;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;

        // Ваши сохранения
        public PlayerResources resources = new PlayerResources();
        public PlayerData data = new PlayerData();
    }
}
