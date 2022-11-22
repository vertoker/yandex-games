//using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.Advertisement
{
    public class AdvertisementRecipeTip : MonoBehaviour
    {
        public void Reward()
        {
            SaveSystem.SaveSystem.UnlockNextRecipe();
        }
    }
}