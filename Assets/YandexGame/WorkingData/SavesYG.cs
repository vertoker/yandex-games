using System.Collections.Generic;
using System.Linq;
using Data;

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

        public List<ImageSerialization> tempSaves = new();
        public LevelData[] levels = new LevelData[50];

        public ImageSerialization Add(string key)
        {
            var im = tempSaves.FirstOrDefault(i => i.Key == key);
            if (im != null) return im;
            im = new ImageSerialization { Key = key };
            tempSaves.Add(im);
            return im;
        }
        public void Remove(string key)
        {
            tempSaves.Remove(tempSaves.FirstOrDefault(s => s.Key == key));
        }
    }
}
