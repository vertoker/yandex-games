using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Data
{
    public class PlayerStatus : MonoBehaviour
    {
        public PlayerLevel playerLevel;

        public void Calculate()
        {

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