using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Game.Data;
using YG;

namespace Scripts.Game.ResourceUI
{
    public class ResourceParameter : MonoBehaviour
    {
        [SerializeField] private PlayerResourceType resourceType;
        private TMP_Text text;
        private Image image;

        private void Awake()
        {
            image = transform.GetChild(0).GetComponent<Image>();
            text = transform.GetChild(1).GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            SaveSystem.updateResources += UpdateResource;
        }
        private void OnDisable()
        {
            SaveSystem.updateResources -= UpdateResource;
        }

        private void UpdateResource()
        {
            text.text = YandexGame.savesData.resources.GetValue(resourceType).ToString();
        }
    }
}