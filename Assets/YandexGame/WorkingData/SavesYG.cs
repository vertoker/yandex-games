using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public List<ImageHistorySerialization> tempSaves;
        public LevelData[] levels;

        public ImageHistorySerialization Add(string key)
        {
            var im = Get(key);
            if (im != null) return im;
            im = new ImageHistorySerialization { key = key };
            tempSaves.Add(im);
            return im;
        }
        public ImageHistorySerialization Get(string key)
        {
            return tempSaves.FirstOrDefault(i => i.key == key);
        }
        public void Remove(string key)
        {
            tempSaves.Remove(Get(key));
        }

        public SavesYG()
        {
            tempSaves = new List<ImageHistorySerialization>();
            levels = new LevelData[50];
            for (var i = 0; i < 50; i++)
                levels[i] = new LevelData();
        }
    }
}
