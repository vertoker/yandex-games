using System;
using System.Linq;
using Core.Language;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Menu
{
    [Serializable]
    public class EnumToggle
    {
        public Button toggle;
        public string identity = "ru";
    }
    
    public class EnumGroupLanguage : MonoBehaviour
    {
        [SerializeField] private EnumToggle[] toggles;
        private string _currentKey;

        private void Awake()
        {
            for (var i = 0; i < toggles.Length; i++)
            {
                var index = i;
                toggles[i].toggle.onClick.AddListener(() => Switch(toggles[index].identity));
                toggles[i].toggle.interactable = true;
            }
        }

        private void OnEnable()
        {
            LanguageHandler.LanguageChanged += OnLanguageChanged;
            OnLanguageChanged();
        }
        private void OnDisable()
        {
            LanguageHandler.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            _currentKey = LanguageHandler.ActiveLanguage;
            var toggle = toggles.FirstOrDefault(t => t.identity == _currentKey);
            if (toggle == null) return;
            
            toggle.toggle.interactable = false;
        }
        
        private void Switch(string key)
        {
            var toggle = toggles.FirstOrDefault(t => t.identity == _currentKey);
            if (toggle != null)
                toggle.toggle.interactable = true;
            
            YandexGame.SwitchLanguage(key);
        }
    }
}