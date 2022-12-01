using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

namespace Scripts.UI
{
    public class NextItem : MonoBehaviour
    {
        [SerializeField] private string textExplanation = "Следующий предмет: ";
        [SerializeField] private string textNotation = "Посмотрите рекламу и разблокируйте следующий рецепт";
        private TMP_Text text;
        private bool activeNotation = false;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            YandexGame.GetDataEvent += UpdateNextText;
            SaveSystem.SaveSystem.RecipeUnlock += UpdateNextText;
        }
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= UpdateNextText;
            SaveSystem.SaveSystem.RecipeUnlock -= UpdateNextText;
        }

        public void ActivateNotation()
        {
            activeNotation = true;
            UpdateNextText();
        }
        public void DeactivateNotation()
        {
            activeNotation = false;
            UpdateNextText();
        }
        public void UpdateNextText()
        {
            text.text = activeNotation ? textNotation : textExplanation + SaveSystem.SaveSystem.NextItemName;
        }
    }
}