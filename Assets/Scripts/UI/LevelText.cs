using System;
using Game;
using UnityEngine;
using TMPro;
using YG;

namespace UI
{
    public class LevelText : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            GameCycle.StartMenuEvent += UpdateText;
        }
        private void OnDisable()
        {
            GameCycle.StartMenuEvent -= UpdateText;
        }

        public void UpdateText()
        {
            text.text = "Уровень " + YandexGame.savesData.currentLevel;
        }
    }
}
