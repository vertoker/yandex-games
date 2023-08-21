using System;
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

        public void SetProgress(float progress)
        {
            progressView.fillAmount = progress;
            textView.text = progress.ToString("0.00");
        }
        public void SetPercent(float progress)
        {
            progressView.fillAmount = progress;
            textView.text = $"{(int)(progress * 100f)}%";
        }
        public void SetCount(int count)
        {
            progressView.fillAmount = 0;
            textView.text = count.ToString();
        }
    }
}