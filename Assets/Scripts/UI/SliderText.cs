using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Scripts.UI
{
    public class SliderText : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }
        public void SliderUpdate()
        {
            text.text = slider.interactable ? string.Format("{0} / {1}", slider.value, slider.maxValue) : "1 / 1";
        }
    }
}