using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Data
{
    [System.Serializable]
    public class PlayerResources
    {
        #region Данные
        // Деньги
        public ulong emerald = 100;
        // Основа
        public ulong dirt = 0;
        public ulong sand = 0;
        public ulong wood = 0;
        public ulong stone = 0;
        // Руда
        public ulong coal = 0;
        public ulong iron = 0;
        public ulong gold = 0;
        public ulong diamond = 0;
        // Еда
        public ulong wheat = 0;
        public ulong carrot = 0;
        public ulong potato = 0;
        public ulong watermelon = 0;

        /*// Мобы: дружелюбные
        public ulong raw_beef = 0;
        public ulong raw_chicken = 0;
        public ulong raw_porkchop = 0;
        public ulong cooked_beef = 0;
        public ulong cooked_chicken = 0;
        public ulong cooked_porkchop = 0;
        // Мобы: враждебные
        public ulong ender_pearl = 0;
        public ulong rotten_flesh = 0;
        public ulong gunpowder = 0;
        public ulong thread = 0;
        public ulong bone = 0;*/

        // Ад: добыча
        public ulong netherrack = 0;
        public ulong glowwstone = 0;
        public ulong soul_sand = 0;
        public ulong quartz = 0;
        public ulong netherite = 0;
        // Ад: данжи
        public ulong nether_wart = 0;
        public ulong gold_ignot = 0;
        public ulong bazalt = 0;
        public ulong obsidian = 0;
        public ulong wither_skelet_head = 0;
        public ulong blaze_rod = 0;
        public ulong ender_pearl = 0;

        // Край: добыча
        public ulong end_stone = 0;
        public ulong nether_star = 0;
        // Край: данжи
        public ulong end_rod = 0;
        public ulong shulker_shell = 0;
        #endregion

        #region Методы
        public ulong GetValue(PlayerResource resource)
        {
            switch (resource)
            {
                case PlayerResource.Emerald: return emerald;
                case PlayerResource.Dirt: return dirt;
                case PlayerResource.Sand: return sand;
                case PlayerResource.Wood: return wood;
                case PlayerResource.Stone: return stone;
                case PlayerResource.Coal: return coal;
                case PlayerResource.Iron: return iron;
                case PlayerResource.Gold: return gold;
                case PlayerResource.Diamond: return diamond;
                case PlayerResource.Wheat: return wheat;
                case PlayerResource.Carrot: return carrot;
                case PlayerResource.Potato: return potato;
                case PlayerResource.Watermelon: return watermelon;
                case PlayerResource.NetherRack: return netherrack;
                case PlayerResource.GlowStone: return glowwstone;
                case PlayerResource.SoulSand: return soul_sand;
                case PlayerResource.Quartz: return quartz;
                case PlayerResource.Netherrite: return netherite;
                case PlayerResource.NetherWart: return nether_wart;
                case PlayerResource.GoldIgnot: return gold_ignot;
                case PlayerResource.Bazalt: return bazalt;
                case PlayerResource.Obsidian: return obsidian;
                case PlayerResource.WitherSkeletHead: return wither_skelet_head;
                case PlayerResource.BlazeRod: return blaze_rod;
                case PlayerResource.EnderPearl: return ender_pearl;
                case PlayerResource.EndStone: return end_stone;
                case PlayerResource.NetherStar: return nether_star;
                case PlayerResource.EndRod: return end_rod;
                case PlayerResource.ShulkerShell: return shulker_shell;
                default: Debug.LogError("Resource isn't exist"); return ulong.MaxValue;
            }
        }
        public void SetValue(PlayerResource resource, ulong value)
        {
            switch (resource)
            {
                case PlayerResource.Emerald: emerald = value; break;
                case PlayerResource.Dirt: dirt = value; break;
                case PlayerResource.Sand: sand = value; break;
                case PlayerResource.Wood: wood = value; break;
                case PlayerResource.Stone: stone = value; break;
                case PlayerResource.Coal: coal = value; break;
                case PlayerResource.Iron: iron = value; break;
                case PlayerResource.Gold: gold = value; break;
                case PlayerResource.Diamond: diamond = value; break;
                case PlayerResource.Wheat: wheat = value; break;
                case PlayerResource.Carrot: carrot = value; break;
                case PlayerResource.Potato: potato = value; break;
                case PlayerResource.Watermelon: watermelon = value; break;
                case PlayerResource.NetherRack: netherrack = value; break;
                case PlayerResource.GlowStone: glowwstone = value; break;
                case PlayerResource.SoulSand: soul_sand = value; break;
                case PlayerResource.Quartz: quartz = value; break;
                case PlayerResource.Netherrite: netherite = value; break;
                case PlayerResource.NetherWart: nether_wart = value; break;
                case PlayerResource.GoldIgnot: gold_ignot = value; break;
                case PlayerResource.Bazalt: bazalt = value; break;
                case PlayerResource.Obsidian: obsidian = value; break;
                case PlayerResource.WitherSkeletHead: wither_skelet_head = value; break;
                case PlayerResource.BlazeRod: blaze_rod = value; break;
                case PlayerResource.EnderPearl: ender_pearl = value; break;
                case PlayerResource.EndStone: end_stone = value; break;
                case PlayerResource.NetherStar: nether_star = value; break;
                case PlayerResource.EndRod: end_rod = value; break;
                case PlayerResource.ShulkerShell: shulker_shell = value; break;
                default: Debug.LogError("Resource isn't exist"); break;
            }
        }
        public void AppendValue(PlayerResource resource, ulong value)
        {
            switch (resource)
            {
                case PlayerResource.Emerald: emerald += value; break;
                case PlayerResource.Dirt: dirt += value; break;
                case PlayerResource.Sand: sand += value; break;
                case PlayerResource.Wood: wood += value; break;
                case PlayerResource.Stone: stone += value; break;
                case PlayerResource.Coal: coal += value; break;
                case PlayerResource.Iron: iron += value; break;
                case PlayerResource.Gold: gold += value; break;
                case PlayerResource.Diamond: diamond += value; break;
                case PlayerResource.Wheat: wheat += value; break;
                case PlayerResource.Carrot: carrot += value; break;
                case PlayerResource.Potato: potato += value; break;
                case PlayerResource.Watermelon: watermelon += value; break;
                case PlayerResource.NetherRack: netherrack += value; break;
                case PlayerResource.GlowStone: glowwstone += value; break;
                case PlayerResource.SoulSand: soul_sand += value; break;
                case PlayerResource.Quartz: quartz += value; break;
                case PlayerResource.Netherrite: netherite += value; break;
                case PlayerResource.NetherWart: nether_wart += value; break;
                case PlayerResource.GoldIgnot: gold_ignot += value; break;
                case PlayerResource.Bazalt: bazalt += value; break;
                case PlayerResource.Obsidian: obsidian += value; break;
                case PlayerResource.WitherSkeletHead: wither_skelet_head += value; break;
                case PlayerResource.BlazeRod: blaze_rod += value; break;
                case PlayerResource.EnderPearl: ender_pearl += value; break;
                case PlayerResource.EndStone: end_stone += value; break;
                case PlayerResource.NetherStar: nether_star += value; break;
                case PlayerResource.EndRod: end_rod += value; break;
                case PlayerResource.ShulkerShell: shulker_shell += value; break;
                default: Debug.LogError("Resource isn't exist"); break;
            }
        }
        #endregion
    }

    public enum PlayerResource : byte
    {
        Emerald = 0,
        Dirt = 1,
        Sand = 2,
        Wood = 3,
        Stone = 4,
        Coal = 5,
        Iron = 6,
        Gold = 7,
        Diamond = 8,
        Wheat = 9,
        Carrot = 10,
        Potato = 11,
        Watermelon = 12,
        NetherRack = 13,
        GlowStone = 14,
        SoulSand = 15,
        Quartz = 16,
        Netherrite = 17,
        NetherWart = 18,
        GoldIgnot = 19,
        Bazalt = 20,
        Obsidian = 21,
        WitherSkeletHead = 22,
        BlazeRod = 23,
        EnderPearl = 24,
        EndStone = 25,
        NetherStar = 26,
        EndRod = 27,
        ShulkerShell = 28
    }
}