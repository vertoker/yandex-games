using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Scripts.UI
{
    public class SliderSettings : MonoBehaviour
    {
        [SerializeField] private float defaultValue = 1f;
        [SerializeField] private string key;
        [SerializeField] private Slider slider;
        [Space]
        [SerializeField] private Image icon;
        [SerializeField] private Sprite on, off;

        private float localValue;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(key))
                PlayerPrefs.SetFloat(key, defaultValue);
            localValue = defaultValue;
            slider.value = defaultValue;
            icon.sprite = defaultValue > 0f ? on : off;
        }

        public void PressIcon()
        {
            slider.value = slider.value > 0f ? 0f : (localValue > 0f ? localValue : defaultValue);
            icon.sprite = slider.value > 0f ? on : off;
        }
        public void SliderUpdate()
        {
            localValue = slider.value;
            PlayerPrefs.SetFloat(key, slider.value);
            icon.sprite = slider.value > 0f ? on : off;
        }
    }
}