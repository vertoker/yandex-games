using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts.UI.RecipeList
{
    public class ItemIconSpawner : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            text = transform.GetChild(0).GetComponent<TMP_Text>();
        }
        public void Click()
        {
            ItemSpawner.CreateItem(text.text);
        }
    }
}