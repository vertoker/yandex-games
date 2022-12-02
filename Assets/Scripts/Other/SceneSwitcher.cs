using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

using Scripts.Game;
using System.Linq;

namespace Scripts.Other
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private Location initializeLocation = Location.Island;
        [SerializeField] private List<LocationSwitchCondition> conditions = new List<LocationSwitchCondition>();

        private static Location currentActive = Location.None;
        private static SceneSwitcher self;

        private void OnEnable()
        {
            Inializator.InitializationComplete += InitializationLoad;
        }
        private void OnDisable()
        {
            Inializator.InitializationComplete -= InitializationLoad;
        }
        private void Awake()
        {
            self = this;
        }

        private void InitializationLoad()
        {
            LoadLocation(initializeLocation);
        }
        public static void LoadLocation(Location target)
        {
            var condition = self.conditions.FirstOrDefault((LocationSwitchCondition lc) => { return lc.source == currentActive && lc.target == target; });

        }
    }

    public struct LocationSwitchCondition
    {
        public Location source;
        public Location target;
        public Vector2 position;
    }

    public enum Location : byte
    {
        None = 0,
        Island = 1,
        Village = 2,
        Nether = 3,
        End = 4
    }
}