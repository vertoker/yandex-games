using System;
using System.Text;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Game.Menu
{
    public class ProgressView : MonoBehaviour
    {
        [SerializeField] private Image progressView;
        [SerializeField] private TMP_Text textView;

        private void Awake()
        {
            OnDisable();
        }
        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            progressView.fillAmount = 0;
        }

        public void SetProgress(float progress, int accuracy = 2)
        {
            progressView.fillAmount = progress;
            textView.text = progress.ToString(GetFormalFloat(accuracy));
        }
        public void SetPercent(float progress, int accuracy = 0)
        {
            progressView.fillAmount = progress;
            textView.text = $"{(progress * 100f).ToString(GetFormalFloat(accuracy))}%";
        }
        public void SetCount(int count)
        {
            progressView.fillAmount = 0;
            textView.text = count.ToString();
        }

        private static string GetFormalFloat(int accuracy)
        {
            if (accuracy == 0) return "0";
            var builder = new StringBuilder(2 + accuracy);
            builder.Append('0');
            builder.Append('.');
            while (accuracy > 0)
            {
                builder.Append('0');
                accuracy--;
            }
            return builder.ToString();
        }
    }
}