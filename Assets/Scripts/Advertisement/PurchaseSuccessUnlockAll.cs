using Scripts.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Purchase
{
    public class PurchaseSuccessUnlockAll : MonoBehaviour
    {
        [SerializeField] private GameObject btnPaymentCall;
        private void OnEnable()
        {
            YandexGame.GetDataEvent += BtnActivityUpdate;
            //SaveSystem.RecipeUnlock += BtnActivityUpdate;
            //SaveSystem.DataLoaded += BtnActivityUpdate;
        }
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= BtnActivityUpdate;
            //SaveSystem.RecipeUnlock -= BtnActivityUpdate;
            //SaveSystem.DataLoaded -= BtnActivityUpdate;
        }
        public void BtnActivityUpdate()
        {
            btnPaymentCall.SetActive(!SaveSystem.GetUnlocked());
        }
        public void UnlockAll()
        {
            SaveSystem.UnlockAll();
            SceneManager.LoadScene(0);
        }
    }
}