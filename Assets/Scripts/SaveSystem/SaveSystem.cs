using Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

namespace Scripts.SaveSystem
{
    public class SaveSystem : MonoBehaviour
    {
        [SerializeField] private ItemRoadmap roadmap;
        private static string TRUE = "true", FALSE = "false", ONLYRECIPE = "only_recipe";

        private static SaveSystem instance;

        public static string NextItemName => PlayerPrefs.GetString("next_item");

        private void Start()
        {
            instance = this;
            if (!PlayerPrefs.HasKey("start"))
                InitialSave();
        }
        private void InitialSave()
        {
            PlayerPrefs.SetString("start", TRUE);
            PlayerPrefs.SetString("next_item", "Камень");
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
            if (PlayerPrefs.GetString(name) == FALSE)
            {
                PlayerPrefs.SetString(name, TRUE);
                GetNextItem();
            }
        }
        public static void UnlockAll()
        {
            foreach (var item in ItemSpawner.Items)
            {
                PlayerPrefs.SetString(item.Name, TRUE);
            }
        }

        public static string[] GetList()
        {
            List<string> list = new List<string>();
            foreach (var item in ItemSpawner.Items)
                if (PlayerPrefs.GetString(item.Name) == TRUE)
                    list.Add(item.Name);
            return list.ToArray();
        }

        public static void GetNextItem()
        {
            for (int i = 4; i < instance.roadmap.List.Count; i++)
            {
                if (PlayerPrefs.GetString(instance.roadmap.List[i]) == FALSE)
                {
                    PlayerPrefs.SetString("next_item", instance.roadmap.List[i]);
                    break;
                }
            }
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