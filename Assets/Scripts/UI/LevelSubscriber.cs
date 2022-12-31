using Game;
using UnityEngine;
using YG;

namespace UI
{
    public class LevelSubscriber : MonoBehaviour
    {
        [SerializeField] private Transform content;
        private LevelItem[] items;
        private int length;

        private void Awake()
        {
            length = YandexGame.savesData.levelPoints.Length;
            items = new LevelItem[length];
            for (int i = 0; i < length; i++)
            {
                items[i] = content.GetChild(i).GetComponent<LevelItem>();
            }
        }
        
        public void UpdateItems()
        {
            for (int i = 0; i < length; i++)
            {
                items[i].SetItem(i);
            }
        }
    }
}
