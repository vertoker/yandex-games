using System;
using UnityEngine;
using YG;

namespace Core.Language
{
    public class LanguageHandler : MonoBehaviour
    {
        public static string ActiveLanguage { get; private set; } = "ru";
        public static event Action LanguageChanged;

        private void OnEnable()
        {
            YandexGame.SwitchLangEvent += SwitchLanguage;
        }
        private void OnDisable()
        {
            YandexGame.SwitchLangEvent -= SwitchLanguage;
        }

        private static void SwitchLanguage(string language)
        {
            ActiveLanguage = language;
            LanguageChanged?.Invoke();
        }
    }
}