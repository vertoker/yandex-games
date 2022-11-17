using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Scripts.UI
{
    public class ItemCount : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            SaveSystem.SaveSystem.RecipeUnlock += UpdateCountText;
        }
        private void OnDisable()
        {
            SaveSystem.SaveSystem.RecipeUnlock -= UpdateCountText;
        }

        private void UpdateCountText(string itemName)
        {
            text.text = string.Format("{0} / 550", SaveSystem.SaveSystem.CountOpenedItems);
        }
    }
}