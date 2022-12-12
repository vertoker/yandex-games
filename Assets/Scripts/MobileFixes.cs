using Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MobileFixes : MonoBehaviour
    {
        [SerializeField] private AnimatorUI settings, about, info;
        [SerializeField] private AnimatorUI recipes, items;

        public void OpenKeyboard()
        {
            TouchScreenKeyboard.Open(string.Empty, TouchScreenKeyboardType.Default, true, false, false, false, "Введите элемент");
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                Back();
        }
        public void Back()
        {
            if (items.IsOpen)
            {
                items.Close();
            }
            else if (recipes.IsOpen)
            {
                recipes.Close();
            }
            else if (settings.IsOpen)
            {
                settings.Close();
            }
            else if (about.IsOpen)
            {
                about.Close();
            }
            else if (info.IsOpen)
            {
                info.Close();
            }
        }
    }
}