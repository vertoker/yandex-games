using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game
{
    public class ScoreCounter : MonoBehaviour
    {
        private TMP_Text text;
        private int score;

        public static int Score => _self.score;

        private static ScoreCounter _self;
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
            _self = this;
        }
        private void OnEnable()
        {
            BeginGame();
        }
        private void OnDisable()
        {
            
        }

        private void BeginGame()
        {
            print(123212);
            score = 0;
            text.text = score.ToString();
        }
        public static void AddPoints(int points)
        {
            _self.score += points;
            _self.text.text = _self.score.ToString();
        }
    }
}
