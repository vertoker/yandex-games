using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using Scripts.Items;
using UnityEditor;
using UnityEngine;
using YG;
using System.Linq;

namespace Scripts.SaveSystem
{
    public class SaveSystem : MonoBehaviour
    {
        [SerializeField] private ItemRoadmap roadmap;
        public static string TRUE = "true", FALSE = "false", ONLYRECIPE = "only_recipe";

        private static SaveSystem instance;
        private static UnityEvent recipeUnlockEvent = new UnityEvent();
        public static event UnityAction RecipeUnlock
        {
            add => recipeUnlockEvent.AddListener(value);
            remove => recipeUnlockEvent.RemoveListener(value);
        }

        private static UnityEvent dataLoadedEvent = new UnityEvent();
        public static event UnityAction DataLoaded
        {
            add => dataLoadedEvent.AddListener(value);
            remove => dataLoadedEvent.RemoveListener(value);
        }

        public static string NextItemName => YandexGame.savesData.nextItem;
        public static int CountOpenedItems => YandexGame.savesData.countOpened;

        private void Start()
        {
            instance = this;
            YandexGame.LoadProgress();
            if (YandexGame.savesData.isFirstSession)
                InitialSave();
            dataLoadedEvent.Invoke();
#if UNITY_EDITOR
            recipeUnlockEvent.Invoke();
#endif
        }
        private void InitialSave()
        {
            YandexGame.savesData.isFirstSession = false;
            YandexGame.savesData.nextItem = "Камень";
            YandexGame.savesData.countOpened = 4;

            YandexGame.savesData.items = new Dictionary<string, string>();
            foreach (var item in ItemSpawner.Items)
                YandexGame.savesData.items.Add(item.Name, FALSE);
            YandexGame.savesData.items["Вода"] = TRUE;
            YandexGame.savesData.items["Земля"] = TRUE;
            YandexGame.savesData.items["Огонь"] = TRUE;
            YandexGame.savesData.items["Воздух"] = TRUE;
            YandexGame.SaveProgress();
            //YandexGame.LoadProgress();
        }
        public static void Unlock(string name)
        {
            //Debug.Log(YandexGame.savesData.items[name]);
            if (YandexGame.savesData.items[name] != TRUE)
            {
                YandexGame.savesData.items[name] = TRUE;
                YandexGame.savesData.countOpened++;
                YandexGame.NewLeaderboardScores("BestOfInventing", YandexGame.savesData.countOpened);
                GetNextItem();
            }
        }
        public static void UnlockNextRecipe()
        {
            if (YandexGame.savesData.nextItem != string.Empty)
            {
                YandexGame.savesData.items[YandexGame.savesData.nextItem] = ONLYRECIPE;
                GetNextItem();
            }
        }
        public static bool GetUnlocked()//38 42
        {
            foreach (var item in ItemSpawner.Items)
            {
                if (YandexGame.savesData.items[item.Name] == FALSE)
                    return false;
            }
            return true;
        }
        public static void UnlockAll()
        {
            foreach (var item in ItemSpawner.Items)
            {
                if (YandexGame.savesData.items[item.Name] == FALSE)
                    YandexGame.savesData.items[item.Name] = ONLYRECIPE;
            }
            YandexGame.SaveProgress();
        }

        public static string[] GetList()
        {
            /*return YandexGame.savesData.items.Keys.Where((string key) => { return key == TRUE; }).ToArray();*/

            List<string> list = new List<string>();
            foreach (var item in ItemSpawner.Items)
                if (YandexGame.savesData.items[item.Name] == TRUE)
                    list.Add(item.Name);
            return list.ToArray();
        }
        public static string[] GetListRecipes()
        {
            /*return YandexGame.savesData.items.Keys.Where((string key) => { return key == ONLYRECIPE; })
                .Concat(YandexGame.savesData.items.Keys.Where((string key) => { return key == TRUE; }).ToArray())
                .ToArray();*/

            List<string> list = new List<string>();
            foreach (var item in ItemSpawner.Items)
                if (YandexGame.savesData.items[item.Name] == ONLYRECIPE)
                    list.Add(item.Name);
            foreach (var item in ItemSpawner.Items)
                if (YandexGame.savesData.items[item.Name] == TRUE)
                    list.Add(item.Name);
            return list.ToArray();
        }

        public static void GetNextItem()
        {
            bool contains = false;
            for (int i = 4; i < instance.roadmap.List.Count; i++)
            {
                if (YandexGame.savesData.items[instance.roadmap.List[i]] == FALSE)
                {
                    YandexGame.savesData.nextItem = instance.roadmap.List[i];
                    contains = true;
                    break;
                }
            }
            if (!contains)
                YandexGame.savesData.nextItem = string.Empty;
            //Debug.Log(YandexGame.savesData.nextItem);
            YandexGame.SaveProgress();
            recipeUnlockEvent.Invoke();
        }

        public static void ResetAll()
        {
            YandexGame.ResetSaveProgress();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SaveSystem))]
    class EditorSaveSystem : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            SaveSystem data = (SaveSystem)target;
            if (GUILayout.Button("Reset"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
#endif
}