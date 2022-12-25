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
            GameCycle.EndGameEvent += EnableWindow;
        }
        private void OnDisable()
        {
            GameCycle.EndGameEvent -= EnableWindow;
        }

        private void EnableWindow()
        {
            if (YandexGame.savesData.LevelPoints < ScoreCounter.Score)
                YandexGame.savesData.LevelPoints = ScoreCounter.Score;
            score.text = ScoreCounter.Score.ToString();
            obj.SetActive(true);
        }
    }
}
