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
            ItemSpawner.CreateUpdate += UpdateNextText;
        }
        private void OnDisable()
        {
            ItemSpawner.CreateUpdate -= UpdateNextText;
        }

        private void UpdateNextText(string itemName)
        {
            text.text = textExplanation + SaveSystem.SaveSystem.NextItemName;
        }
    }
}