using System.Collections;
using Scripts.UI.ItemList;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace Scripts.UI.RecipeList
{
    public class RecipeList : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private AnimatorUI listWindowAnimator;
        [SerializeField] private TMP_InputField inputField;
        private RecipeIcon[] icons;
        private Coroutine searchUpdate;

        private void OnEnable()
        {
            listWindowAnimator.OpenStartEvent += UpdateRecipe;
            listWindowAnimator.CloseEndEvent += Close;
        }
        private void OnDisable()
        {
            listWindowAnimator.OpenStartEvent -= UpdateRecipe;
            listWindowAnimator.CloseEndEvent -= Close;
        }
        private void Start()
        {
            icons = new RecipeIcon[content.childCount];
            for (int i = 0; i < content.childCount; i++)
            {
                icons[i] = new RecipeIcon(content.GetChild(i));
                icons[i].Clear();
            }
        }

        private void UpdateRecipe()
        {
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
            inputField.text = string.Empty;
        }

        public void ClickItem(int id)
        {
            Debug.Log(icons[id].Name);
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

    public struct RecipeIcon
    {
        private static Color transparent = new Color(1, 1, 1, 0);

        private Image image0, image1, image2, image3;
        private TMP_Text text0, text1, text2, text3;

        public string Name => text.text;

        public RecipeIcon(Transform tr)
        {
            image = tr.GetComponent<Image>();
            text = tr.GetChild(0).GetComponent<TMP_Text>();
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