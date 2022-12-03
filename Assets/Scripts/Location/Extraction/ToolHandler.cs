using Scripts.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Location.Extraction
{
    public class ToolHandler : MonoBehaviour
    {
        private GameObject toolObject = null;
        [SerializeField] private Transform parentHand;
        private ToolType toolType = ToolType.Empty;
        private ToolMaterial toolMaterial = ToolMaterial.Netherite;

        public void SwitchTool(ToolType type = ToolType.Empty, ToolMaterial material = ToolMaterial.Wood)
        {
            if (toolType != type || toolMaterial != material)
            {
                if (toolType != ToolType.Empty)
                {
                    PoolsHandler.GetSpawner(toolType, toolMaterial).Enqueue(toolObject);
                }
                if (type != ToolType.Empty)
                {
                    toolObject = PoolsHandler.GetSpawner(type, material).Dequeue(false);
                    toolObject.transform.parent = parentHand;
                    toolObject.transform.localPosition = Vector3.zero;
                    toolObject.transform.localEulerAngles = Vector3.zero;
                    toolObject.SetActive(true);
                }
                toolType = type;
                toolMaterial = material;
            }
        }
    }

    public enum ToolType : byte
    {
        Empty = 0,
        Axe = 1,
        Hoe = 2,
        Pickaxe = 3,
        Shovel = 4,
        Sword = 5
    }
    public enum ToolMaterial : byte
    {
        Diamond = 0,
        Iron = 1,
        Netherite = 2,
        Stone = 3,
        Wood = 4
    }
}