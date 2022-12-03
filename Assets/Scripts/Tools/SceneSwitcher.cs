using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using System;
using YG;

using Scripts.Game;

namespace Scripts.Tools
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private Location initializeLocation = Location.Island;
        [SerializeField] private List<LocationSwitchCondition> conditions = new List<LocationSwitchCondition>();

        private static Location currentActive = Location.None;
        private static SceneSwitcher self;

        private void OnEnable()
        {
            YandexGame.GetDataEvent += InitializationLoad;
            //Inializator.InitializationComplete += InitializationLoad;
        }
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= InitializationLoad;
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
            if (condition.source != Location.None)
            {
                SceneManager.UnloadSceneAsync((int)condition.source);
            }
            if (condition.target != Location.None)
            {
                SceneManager.LoadSceneAsync((int)condition.target, LoadSceneMode.Additive);
            }
        }
    }

    [Serializable]
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