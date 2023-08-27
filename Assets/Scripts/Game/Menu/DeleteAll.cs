using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Game.Menu
{
    public class DeleteAll : MonoBehaviour
    {
        public void Delete()
        {
            var data = YandexGame.savesData;
            YandexGame.savesData = new SavesYG
            {
                isFirstSession = false,
                unlockEverything = data.unlockEverything,
                addDisabled = data.addDisabled
            };
            YandexGame.SaveProgress();
            SceneManager.LoadScene(1);
        }
    }
}