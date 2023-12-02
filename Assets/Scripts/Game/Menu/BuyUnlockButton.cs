using System;
using UnityEngine;
using YG;

namespace Game.Menu
{
    public class BuyUnlockButton : MonoBehaviour
    {
        [SerializeField] private int purchaseID = 1;
        [SerializeField] private MenuButtonsHandler handler;
        
        public void Buy()
        {
            switch (purchaseID)
            {
                case 1 when YandexGame.savesData.unlockEverything:
                case 2 when YandexGame.savesData.addDisabled:
                    return;
                default:
                    YandexGame.BuyPayments(purchaseID.ToString());
                    break;
            }
        }

        private void Awake()
        {
            switch (purchaseID)
            {
                case 1 when YandexGame.savesData.unlockEverything:
                case 2 when YandexGame.savesData.addDisabled:
                    gameObject.SetActive(false);
                    break;
            }
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
            switch (id)
            {
                case "1": YandexGame.savesData.unlockEverything = true; break;
                case "2": YandexGame.savesData.addDisabled = true; break;
            }
            
            gameObject.SetActive(false);
            handler.UpdateButtonsByStatus();
            YandexGame.SaveProgress();
        }
    }
}