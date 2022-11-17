using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace Scripts.UI.ItemList
{
    public class ItemList : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private Transform content;
        [SerializeField] private AnimatorUI listWindowAnimator;
        [SerializeField] private TMP_InputField inputField;

        private RectTransform self;
        private ItemIcon[] icons;
        private Coroutine searchUpdate;

        private void OnEnable()
        {
            listWindowAnimator.OpenStartEvent += UpdateList;
            listWindowAnimator.CloseEndEvent += Clear;
        }
        private void OnDisable()
        {
            listWindowAnimator.OpenStartEvent -= UpdateList;
            listWindowAnimator.CloseEndEvent -= Clear;
        }
        private void Start()
        {
            self = GetComponent<RectTransform>();
            icons = new ItemIcon[content.childCount];
            for (int i = 0; i < content.childCount; i++)
            {
                icons[i] = new ItemIcon(content.GetChild(i));
                icons[i].Clear();
            }
            UpdateList();
        }

        private void UpdateList()
        {
            self.sizeDelta = new Vector2(1080f * cam.aspect, 1080f);
            var list = SaveSystem.SaveSystem.GetList();
            for (int i = 0; i < list.Length; i++)
                icons[i].Update(ItemSpawner.ItemDictionary[list[i]]);
            for (int i = list.Length; i < icons.Length; i++)
                icons[i].Clear();
        }
        public void UpdateSearch()
        {
            if (searchUpdate != null)
                StopCoroutine(searchUpdate);
            searchUpdate = StartCoroutine(SearchTask(inputField.text));
        }
        public void Close()
        {
            listWindowAnimator.Close();
        }
        private void Clear()
        {
            inputField.text = string.Empty;
        }

        public void ClickItem(int id)
        {
            //Debug.Log(icons[id].Name);
            ItemSpawner.CreateItem(icons[id].Name);
        }
        public void BeginDragItem(int id)
        {

        }

        public IEnumerator SearchTask(string value)
        {
            yield return null;
            var list = SaveSystem.SaveSystem.GetList();
            list = list.Where(s => s.ToLower().Contains(value.ToLower())).ToArray();
            for (int i = 0; i < list.Length; i++)
                icons[i].Update(ItemSpawner.ItemDictionary[list[i]]);
            for (int i = list.Length; i < icons.Length; i++)
                icons[i].Clear();
        }
    }

    public struct ItemIcon
    {
        private static Color transparent = new Color(1, 1, 1, 0);

        private Image image;
        private TMP_Text text;

        public string Name => text.text;

        public ItemIcon(Transform tr)
        {
            image = tr.GetComponent<Image>();
            text = tr.GetChild(0).GetComponent<TMP_Text>();
        }

        public void SetActive(bool active)
        {
            image.raycastTarget = active;
            text.raycastTarget = active;
        }

        public void Update(Items.Item item)
        {
            image.sprite = item.Sprite;
            text.text = item.Name;
            image.color = Color.white;
            text.color = Color.white;
            image.enabled = true;
            text.enabled = true;
        }
        public void Clear()
        {
            image.sprite = null;
            text.text = string.Empty;
            image.color = transparent;
            text.color = transparent;
            image.enabled = false;
            text.enabled = false;
        }
    }
}