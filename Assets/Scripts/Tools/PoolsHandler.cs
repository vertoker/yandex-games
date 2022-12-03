using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Location.Extraction;
using Game.Pool;
using Unity.VisualScripting;
using System;

namespace Scripts.Pools
{
    public class PoolsHandler : MonoBehaviour
    {
        [SerializeField] private PoolData[] tools;
        private PoolSpawner[,] poolSpawners;
        private int typeCount, materialCount;
        private static PoolsHandler self;

        private void Awake()
        {
            self = this;
            materialCount = Enum.GetNames(typeof(ToolMaterial)).Length;
            typeCount = Enum.GetNames(typeof(ToolType)).Length - 1;
            poolSpawners = new PoolSpawner[typeCount, materialCount];

            int counter = 0;
            for (int i = 0; i < materialCount; i++)
            {
                for (int j = 0; j < typeCount; j++)
                {
                    poolSpawners[i, j] = transform.AddComponent<PoolSpawner>();
                    poolSpawners[i, j].data = tools[counter];
                    poolSpawners[i, j].Spawn();
                    counter++;
                }
            }
        }

        public static PoolSpawner GetSpawner(ToolType type, ToolMaterial material)
        {
            return self.poolSpawners[(int)material, (int)type - 1];
        }
    }
}