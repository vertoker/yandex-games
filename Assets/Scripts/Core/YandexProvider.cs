using UnityEngine;
using YG;

namespace Core
{
    public class YandexProvider : MonoBehaviour
    {
        public void FullscreenShow()
        {
            if (YandexGame.savesData.addDisabled) return;
            YandexGame.FullscreenShow();
        }

        public void BuyPayments(string key)
        {
            YandexGame.BuyPayments(key);
        }
    }
}