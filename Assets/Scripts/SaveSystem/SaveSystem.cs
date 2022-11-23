using System.Collections.Generic;
using UnityEngine.Events;
using Scripts.Items;
using UnityEditor;
using UnityEngine;
using YG;

namespace Scripts.SaveSystem
{
    public class SaveSystem : MonoBehaviour
    {
        [SerializeField] private ItemRoadmap roadmap;
        public static string TRUE = "true", FALSE = "false", ONLYRECIPE = "only_recipe";

        private static SaveSystem instance;
        private static UnityEvent<string> recipeUnlockEvent = new UnityEvent<string>();
        public static event UnityAction<string> RecipeUnlock
        {
            add => recipeUnlockEvent.AddListener(value);
            remove => recipeUnlockEvent.RemoveListener(value);
        }

        public static string NextItemName => PlayerPrefs.GetString("next_item");
        public static int CountOpenedItems => PlayerPrefs.GetInt("count_opened");

        public static string GetString(string value)
        {
            return PlayerPrefs.GetString(value);
        }

        private void Start()
        {
            instance = this;
            if (!PlayerPrefs.HasKey("start"))
                InitialSave();
            recipeUnlockEvent.Invoke(GetString("next_item"));
        }
        private void InitialSave()
        {
            PlayerPrefs.SetString("start", TRUE);
            PlayerPrefs.SetString("next_item", "Камень");
            PlayerPrefs.SetInt("count_opened", 4);

            foreach (var item in ItemSpawner.Items)
            {
                PlayerPrefs.SetString(item.Name, FALSE);
            }
            PlayerPrefs.SetString("Вода", TRUE);
            PlayerPrefs.SetString("Земля", TRUE);
            PlayerPrefs.SetString("Огонь", TRUE);
            PlayerPrefs.SetString("Воздух", TRUE);
            PlayerPrefs.Save();
        }
        public static void Unlock(string name)
        {
            if (GetString(name) != TRUE)
            {
                PlayerPrefs.SetString(name, TRUE);
                int count_opened = PlayerPrefs.GetInt("count_opened") + 1;
                PlayerPrefs.SetInt("count_opened", count_opened);
                YandexGame.NewLeaderboardScores("BestOfInventing", count_opened);
                GetNextItem();
            }
        }
        public static void UnlockNextRecipe()
        {
            if (GetString("next_item") != string.Empty)
            {
                PlayerPrefs.SetString(GetString("next_item"), ONLYRECIPE);
                GetNextItem();
            }
        }
        public static bool GetUnlocked()
        {
            foreach (var item in ItemSpawner.Items)
            {
                if (PlayerPrefs.GetString(item.Name) == FALSE)
                    return false;
            }
            return true;
        }
        public static void UnlockAll()
        {
            foreach (var item in ItemSpawner.Items)
            {
                if (PlayerPrefs.GetString(item.Name) == FALSE)
                    PlayerPrefs.SetString(item.Name, ONLYRECIPE);
            }
        }

        public static string[] GetList()
        {
            List<string> list = new List<string>();
            foreach (var item in ItemSpawner.Items)
                if (GetString(item.Name) == TRUE)
                    list.Add(item.Name);
            return list.ToArray();
        }
        public static string[] GetListRecipes()
        {
            List<string> list = new List<string>();
            foreach (var item in ItemSpawner.Items)
                if (GetString(item.Name) == ONLYRECIPE)
                    list.Add(item.Name);
            foreach (var item in ItemSpawner.Items)
                if (GetString(item.Name) == TRUE)
                    list.Add(item.Name);
            return list.ToArray();
        }

        public static void GetNextItem()
        {
            bool contains = false;
            for (int i = 4; i < instance.roadmap.List.Count; i++)
            {
                if (GetString(instance.roadmap.List[i]) == FALSE)
                {
                    PlayerPrefs.SetString("next_item", instance.roadmap.List[i]);
                    contains = true;
                    break;
                }
            }
            if (!contains)
                PlayerPrefs.SetString("next_item", string.Empty);
            recipeUnlockEvent.Invoke(GetString("next_item"));
        }

        public static void ResetAll()
        {
            PlayerPrefs.DeleteAll();
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