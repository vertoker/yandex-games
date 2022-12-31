using TMPro;
using UnityEngine.UI;
using UnityEngine;
using YG;

namespace UI
{
    public class LevelItem : MonoBehaviour
    {
        private TMP_Text level;
        private TMP_Text score;
        private Image lockObject;
        private int levelID;

        private void Awake()
        {
            level = transform.GetChild(0).GetComponent<TMP_Text>();
            score = transform.GetChild(1).GetComponent<TMP_Text>();
            lockObject = transform.GetChild(2).GetComponent<Image>();
        }

        public void SetItem(int levelID)
        {
            this.levelID = levelID;
            var points = YandexGame.savesData.levelPoints[levelID];
            level.text = (levelID + 1).ToString();
            score.text = points.ToString();
            lockObject.enabled = levelID >= YandexGame.savesData.currentLevel && points == 0;
        }

        public void SelectLevel()
        {
            YandexGame.savesData.currentLevel = levelID + 1;
        }
    }
}
