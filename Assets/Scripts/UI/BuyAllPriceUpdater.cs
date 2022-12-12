using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

namespace Scripts.UI
{
    public class BuyAllPriceUpdater : MonoBehaviour
    {
        [SerializeField] private string origText;
        [SerializeField] private TMP_Text txtField;

        private void OnEnable()
        {
            YandexGame.GetPaymentsEvent += UpdatePrice;
        }
        private void OnDisable()
        {
            YandexGame.GetPaymentsEvent -= UpdatePrice;
        }
        private void UpdatePrice()
        {
            var lan = YandexGame.EnvironmentData.language == "ru" ? "ßÍ" : "YAN";
            txtField.text = string.Format("{0} ({1} {2})", origText, YandexGame.PaymentsData.priceValue[0], lan);
        }
    }
}