using System.Collections;
using UnityEngine;
using TMPro;
using YG;

namespace Scripts.Advertisement
{
    public class AdvertisementTimer : MonoBehaviour
    {
        [SerializeField] private float secondsWait = 120f;
        [SerializeField] private float secondsAlert = 2.99f;
        [SerializeField] private string alertText = "Реклама будет через ";
        private TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
            StartCoroutine(WaitNext());
        }

        private IEnumerator WaitNext()
        {
            yield return new WaitForSeconds(5f);
            if (YandexGame.CanShow(secondsWait))
                StartCoroutine(AlertTimer());
            else
                StartCoroutine(WaitNext());
        }

        private IEnumerator AlertTimer()
        {
            for (float i = secondsAlert; i > 0; i -= Time.deltaTime)
            {
                yield return null;
                text.text = alertText + Mathf.FloorToInt(i + 1).ToString();
            }
            text.text = string.Empty;
            YandexGame.FullscreenShow();
            StartCoroutine(WaitNext());
        }
    }
}