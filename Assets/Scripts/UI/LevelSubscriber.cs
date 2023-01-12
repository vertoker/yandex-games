using Game;
using UnityEngine;
using YG;

namespace UI
{
    public class LevelSubscriber : MonoBehaviour
    {
        [SerializeField] private Transform content;
        private LevelItem[] items;

        private void Awake()
        {
            items = new LevelItem[SavesYG.LEVELCOUNT];
            for (int i = 0; i < SavesYG.LEVELCOUNT; i++)
            {
                items[i] = content.GetChild(i).GetComponent<LevelItem>();
            }
        }
        
        public void UpdateItems()
        {
            for (int i = 0; i < SavesYG.LEVELCOUNT; i++)
            {
                items[i].SetItem(i);
            }
        }
    }
}
