using System;
using Core.Language;
using Data.Localization;
using TMPro;
using UnityEngine;

namespace Game.Menu
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizationText : MonoBehaviour
    {
        [SerializeField] private BaseLocalize data;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            LanguageHandler.LanguageChanged += UpdateLanguage;
            UpdateLanguage();
        }
        private void OnDisable()
        {
            LanguageHandler.LanguageChanged -= UpdateLanguage;
        }
        
        private void UpdateLanguage()
        {
            _text.text = data.GetString();
        }
    }
}