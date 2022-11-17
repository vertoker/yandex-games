using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.Advertisement
{
    public class AdvertisementRecipeTip : MonoBehaviour
    {
        private bool active = true;
        private bool debug = false;

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

        }
        private void Initialize(bool debug)
        {
            this.debug = debug;
            active = false;
        }
        public void GetTip()
        {
            if (active)
                return;
            active = true;

            if (debug)
            {
                OpenAd();
                Reward();
            }
            else
                VideoAd.Show(OpenAd, Reward, null, null);
        }

        private void OpenAd()
        {
            active = false;
        }
        private void Reward()
        {
            Debug.Log("VideoAd.Show()");
            SaveSystem.SaveSystem.UnlockNextRecipe();
        }
    }
}