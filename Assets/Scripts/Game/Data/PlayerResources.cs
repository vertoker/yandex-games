using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Data
{
    [System.Serializable]
    public class PlayerResources
    {
        public ulong[] values = new ulong[29];

        public ulong GetValue(PlayerResourceType type) => values[(int)type];
        public void SetValue(PlayerResourceType type, ulong value) => values[(int)type] = value;
        public void AppendValue(PlayerResourceType type, ulong value) => values[(int)type] += value;
    }

    public enum PlayerResourceType : byte
    {
        // Деньги
        Emerald = 0,
        // Основа
        Dirt = 1,
        Sand = 2,
        Wood = 3,
        Stone = 4,
        // Руда
        Coal = 5,
        Iron = 6,
        Gold = 7,
        Diamond = 8,
        // Еда
        Wheat = 9,
        Carrot = 10,
        Potato = 11,
        Watermelon = 12,

        // Ад: добыча
        NetherRack = 13,
        GlowStone = 14,
        SoulSand = 15,
        Quartz = 16,
        Netherrite = 17,
        // Ад: данжи
        NetherWart = 18,
        GoldIgnot = 19,
        Bazalt = 20,
        Obsidian = 21,
        WitherSkeletHead = 22,
        BlazeRod = 23,
        EnderPearl = 24,

        // Край: добыча
        EndStone = 25,
        NetherStar = 26,
        // Край: данжи
        EndRod = 27,
        ShulkerShell = 28
    }
}