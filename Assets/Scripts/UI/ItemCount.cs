using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

namespace Scripts.UI
{
    public class ItemCount : MonoBehaviour
    {
        [SerializeField] private string description = "Количество открытых предметов";
        private bool includeDescription = false;
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            YandexGame.GetDataEvent += UpdateCountText;
            SaveSystem.SaveSystem.RecipeUnlock += UpdateCountText;
            ScreenCaller.ScreenOrientationChanged += DescriptionUpdate;
        }
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= UpdateCountText;
            SaveSystem.SaveSystem.RecipeUnlock -= UpdateCountText;
            ScreenCaller.ScreenOrientationChanged -= DescriptionUpdate;
        }

        private void DescriptionUpdate(bool vertical, float aspect)
        {
            includeDescription = !vertical;
            UpdateCountText();
        }
        public void UpdateCountText()
        {
            text.text = includeDescription ? 
                string.Format("{0}\n{1} / 550", description, SaveSystem.SaveSystem.CountOpenedItems) :
                string.Format("{0} / 550", SaveSystem.SaveSystem.CountOpenedItems);
        }
    }
}