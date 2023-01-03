using YG;
using Game;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStatsActivator : MonoBehaviour
    {
        [SerializeField] private TMP_Text score;
        [SerializeField] private GameObject obj;

        private void Awake()
        {
            obj.SetActive(false);
        }
        private void OnEnable()
        {
            GameCycle.StartEndGameUI += EnableWindow;
        }
        private void OnDisable()
        {
            GameCycle.StartEndGameUI -= EnableWindow;
        }

        private void EnableWindow()
        {
            if (YandexGame.savesData.levelPoints[YandexGame.savesData.currentLevel - 1] < ScoreCounter.Score)
                YandexGame.savesData.levelPoints[YandexGame.savesData.currentLevel - 1] = ScoreCounter.Score;
            if (YandexGame.savesData.currentLevel == YandexGame.savesData.maxLevel)
                YandexGame.savesData.maxLevel = Mathf.Clamp(YandexGame.savesData.maxLevel + 1, 0, 100);
            YandexGame.SaveProgress();
            
            score.text = ScoreCounter.Score.ToString();
            obj.SetActive(true);
        }
    }
}
