using System;
using UnityEngine;
using YG;

namespace Game.Menu
{
    public class BuyUnlockButton : MonoBehaviour
    {
        [SerializeField] private MenuButtonsHandler handler;
        
        public void Buy()
        {
            YandexGame.BuyPayments("1");
        }
        
        private void OnEnable()
        {
            YandexGame.PurchaseSuccessEvent += SuccessPurchased;
        }
        private void OnDisable()
        {
            YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
        }

        private void SuccessPurchased(string id)
        {
            if (id == "1")
                YandexGame.savesData.unlockEverything = true;
            handler.UpdateButtonsByStatus();
            YandexGame.SaveProgress();
        }
    }
}