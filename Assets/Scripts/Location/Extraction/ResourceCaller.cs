using Scripts.Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Location.Extraction
{
    public class ResourceCaller : MonoBehaviour
    {
        [SerializeField] private ToolType toolType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Person"))
                other.GetComponent<PersonController>().SwitchTool(toolType);
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Person"))
                other.GetComponent<PersonController>().SwitchTool(ToolType.Empty);
        }
    }
}