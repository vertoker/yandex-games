using Scripts.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Purchase
{
    public class PurchaseSuccessUnlockAll : MonoBehaviour
    {
        [SerializeField] private GameObject btnPaymentCall;
        public void Start()
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