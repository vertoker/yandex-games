using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.Advertisement
{
    public class AdvertisementRecipeTip : MonoBehaviour
    {
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
        private void Initialize()
        {

        }
    }
}