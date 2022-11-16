using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using TMPro;

namespace Scripts.Advertisement
{
    public class AdvertisementTimer : MonoBehaviour
    {
        [SerializeField] private float initialSecondsWait = 5f;
        [SerializeField] private float secondsWait = 120f;
        [SerializeField] private float secondsAlert = 2.99f;
        [SerializeField] private string alertText = "Реклама будет через ";
        private TMP_Text text;

        private void OnEnable()
        {
            AdvertisementInitializator.Initialize += Initialize;
        }
        private void OnDisable()
        {
            AdvertisementInitializator.Initialize -= Initialize;
        }
        private void Start()
        {
            text = GetComponent<TMP_Text>();
        }
        private void Initialize()
        {
            StartCoroutine(WaitNext(initialSecondsWait));
        }

        private IEnumerator WaitNext(float time)
        {
            yield return new WaitForSeconds(time);
            StartCoroutine(AlertTimer());
        }

        private IEnumerator AlertTimer()
        {
            for (float i = secondsAlert; i > 0; i -= Time.deltaTime)
            {
                yield return null;
                text.text = alertText + Mathf.FloorToInt(i + 1).ToString();
            }
            text.text = string.Empty;
            InterstitialAd.Show();
            StartCoroutine(WaitNext(secondsWait));
        }
    }
}