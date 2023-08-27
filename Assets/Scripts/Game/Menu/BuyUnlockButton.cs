using System;
using UnityEngine;
using YG;

namespace Game.Menu
{
    public class BuyUnlockButton : MonoBehaviour
    {
        [SerializeField] private MenuButtonsHandler handler;
        
        public void Buy(string id)
        {
            if (id == "1" && YandexGame.savesData.unlockEverything) return;
            if (id == "2" && YandexGame.savesData.addDisabled) return;
            YandexGame.BuyPayments(id);
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
            if (id == "1") YandexGame.savesData.unlockEverything = true;
            if (id == "2") YandexGame.savesData.addDisabled = true;
            handler.UpdateButtonsByStatus();
            YandexGame.SaveProgress();
        }
    }
}