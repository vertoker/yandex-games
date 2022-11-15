using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SaveSystem
{
    public class SaveSystem : MonoBehaviour
    {
        private static string TRUE = "true", FALSE = "false";

        [SerializeField] private List<Items.Item> items;

        private static SaveSystem instance;

        private void Awake()
        {
            instance = this;
            if (!PlayerPrefs.HasKey("start"))
                InitialSave();
        }
        private void InitialSave()
        {
            PlayerPrefs.SetString("start", TRUE);
            foreach (var item in items)
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
                PlayerPrefs.SetString(name, TRUE);
        }

        public static string[] GetList()
        {
            List<string> list = new List<string>();
            foreach (var item in instance.items)
                if (PlayerPrefs.GetString(item.Name) == TRUE)
                    list.Add(item.Name);
            return list.ToArray();
        }
    }
}