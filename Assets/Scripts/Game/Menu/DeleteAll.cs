using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Game.Menu
{
    public class DeleteAll : MonoBehaviour
    {
        [SerializeField] private bool deleteBuys;
        
        public void Delete()
        {
            var data = YandexGame.savesData;
            YandexGame.savesData = new SavesYG
            {
                isFirstSession = false,
                unlockEverything = !deleteBuys && data.unlockEverything,
                addDisabled = !deleteBuys && data.addDisabled
            };
            YandexGame.SaveProgress();
            SceneManager.LoadScene(1);
        }
    }
}