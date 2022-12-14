using Scripts.Location.Extraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Data
{
    public class PlayerPump : MonoBehaviour
    {
        private static int START_EXPERIENCE = 100, PROGRESS_EXPERIENCE = 20;

        public ulong experience = 0;
        public PlayerLevel playerLevel = PlayerLevel.Noob;

        public ToolMaterial axeMaterial = ToolMaterial.Wood;
        public ToolMaterial hoeMaterial = ToolMaterial.Wood;
        public ToolMaterial pickaxeMaterial = ToolMaterial.Wood;
        public ToolMaterial shovelMaterial = ToolMaterial.Wood;
        public ToolMaterial swordMaterial = ToolMaterial.Wood;

        public int GetLevel()
        {
            //y=100x+10x(x-1)
            //y=90x+10x^2
            return Mathf.FloorToInt(1);
        }
        public void Upgrade(ToolType toolType)
        {
            switch (toolType)
            {
                case ToolType.Pickaxe: if (pickaxeMaterial != ToolMaterial.Netherite) pickaxeMaterial = (ToolMaterial)((byte)pickaxeMaterial + 1); break;
                case ToolType.Shovel: if (shovelMaterial != ToolMaterial.Netherite) shovelMaterial = (ToolMaterial)((byte)shovelMaterial + 1); break;
                case ToolType.Hoe: if (hoeMaterial != ToolMaterial.Netherite) hoeMaterial = (ToolMaterial)((byte)hoeMaterial + 1); break;
                case ToolType.Axe: if (axeMaterial != ToolMaterial.Netherite) axeMaterial = (ToolMaterial)((byte)axeMaterial + 1); break;
                case ToolType.Sword: if (swordMaterial != ToolMaterial.Netherite) swordMaterial = (ToolMaterial)((byte)swordMaterial + 1); break;
            }
        }
    }

    public enum PlayerLevel : byte
    {
        Noob = 0,
        Pro = 1,
        Hacker = 2,
        God = 3
    }
}