using System;
using Core.Language;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Menu
{
    public class EnumGroupSelectable : MonoBehaviour
    {
        [SerializeField] private Button[] toggles;
        [SerializeField] private int currentIndex;

        private void Awake()
        {
            for (var i = 0; i < toggles.Length; i++)
            {
                var index = i;
                toggles[i].onClick.AddListener(() => Switch(index));
                toggles[i].interactable = true;
            }
            toggles[currentIndex].interactable = false;
        }

        private void Switch(int index)
        {
            toggles[currentIndex].interactable = true;
            currentIndex = index;
            toggles[currentIndex].interactable = false;
            YandexGame.SwitchLanguage();
        }
    }
}