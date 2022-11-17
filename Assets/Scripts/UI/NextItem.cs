using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts.UI
{
    public class NextItem : MonoBehaviour
    {
        [SerializeField] private string textExplanation = "Следующий предмет: ";
        private TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            SaveSystem.SaveSystem.RecipeUnlock += UpdateNextText;
        }
        private void OnDisable()
        {
            SaveSystem.SaveSystem.RecipeUnlock -= UpdateNextText;
        }

        private void UpdateNextText(string itemName)
        {
            var name = SaveSystem.SaveSystem.NextItemName;
            text.text = name == string.Empty ? string.Empty : textExplanation + name;
        }
    }
}