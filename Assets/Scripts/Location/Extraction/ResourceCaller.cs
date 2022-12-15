using Scripts.Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using YG;

namespace Scripts.Location.Extraction
{
    public class ResourceCaller : MonoBehaviour
    {
        [SerializeField] private ToolType toolType;
        public ToolType ToolType => toolType;

        public void TriggerEnter(Collider other)
        {
            if (other.CompareTag("Person"))
                other.GetComponent<PersonController>().SwitchTool(toolType, YandexGame.savesData.data.GetMaterial(toolType));
        }
        public void TriggerExit(Collider other)
        {
            if (other.CompareTag("Person"))
                other.GetComponent<PersonController>().SwitchTool(ToolType.Empty, YandexGame.savesData.data.GetMaterial(toolType));
        }
    }
}