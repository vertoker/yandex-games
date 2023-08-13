using System;
using Preset;
using UnityEngine;

namespace Game.Menu
{
    public class MenuButtonsHandler : MonoBehaviour
    {
        [SerializeField] protected Transform parent;
        [SerializeField] protected MenuButton buttonTemplate;
        [SerializeField] protected ImageGroupPreset presets;

        private void Awake()
        {
            foreach (var preset in presets.Presets)
            {
                var button = Instantiate(buttonTemplate, parent);
                button.Install(preset);
            }
        }
    }
}