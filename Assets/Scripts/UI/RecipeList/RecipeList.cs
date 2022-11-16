using System.Collections;
using Scripts.UI.ItemList;
using UnityEngine.UI;
using UnityEngine;
using Scripts.Items;
using TMPro;
using System.Linq;
using System.Collections.Generic;

namespace Scripts.UI.RecipeList
{
    public class RecipeList : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Transform content;
        [SerializeField] private AnimatorUI listWindowAnimator;
        [SerializeField] private TMP_InputField inputField;

        private RecipeIcon[] icons;
        private Coroutine searchUpdate;
        private List<string> list;
        private RectTransform self;

        private void OnEnable()
        {
            listWindowAnimator.OpenStartEvent += UpdateRecipe;
            //listWindowAnimator.CloseEndEvent += Close;
            slider.SliderUpdate += UpdateSlider;
        }
        private void OnDisable()
        {
            listWindowAnimator.OpenStartEvent -= UpdateRecipe;
            //listWindowAnimator.CloseEndEvent -= Close;
            slider.SliderUpdate -= UpdateSlider;
        }
        private void Start()
        {
            self = GetComponent<RectTransform>();
            icons = new RecipeIcon[content.childCount];
            for (int i = 0; i < content.childCount; i++)
            {
                icons[i] = new RecipeIcon(content.GetChild(i));
            }
            UpdateRecipe();
        }

        public void UpdateRecipe()
        {
            self.sizeDelta = new Vector2(1080f * (Screen.width / Screen.height), 1080f);
            list = SaveSystem.SaveSystem.GetList().ToList();
            list.Remove("Вода");
            list.Remove("Земля");
            list.Remove("Огонь");
            list.Remove("Воздух");
            slider.Initialize(Mathf.FloorToInt(list.Count / 10f) + 1);
            slider.FullUp();
        }
        private void UpdateSlider(int currentPage, int pageCount)
        {
            List<string> showList;

            if (list.Count - icons.Length >= currentPage * icons.Length)
                showList = list.GetRange(currentPage * icons.Length, icons.Length);
            else
                showList = list.GetRange(currentPage * icons.Length, list.Count - currentPage * icons.Length);

            for (int i = 0; i < showList.Count; i++)
                icons[i].Update(ItemSpawner.RecipeDictionary[showList[i]]);
            for (int i = showList.Count; i < icons.Length; i++)
                icons[i].Clear();
        }
        public void UpdateSearch()
        {
            if (searchUpdate != null)
                StopCoroutine(searchUpdate);
            searchUpdate = StartCoroutine(SearchTask(inputField.text));
        }
        public void Clear()
        {
            if (inputField.text.Length != 0)
            {
                inputField.text = string.Empty;
            }
            else
            {
                listWindowAnimator.Close();
            }
        }

        public IEnumerator SearchTask(string value)
        {
            yield return null;

            self.sizeDelta = new Vector2(1080f * (Screen.width / Screen.height), 1080f);
            list = SaveSystem.SaveSystem.GetList().ToList();
            list.Remove("Вода");
            list.Remove("Земля");
            list.Remove("Огонь");
            list.Remove("Воздух");

            list = list.Where(s => s.ToLower().Contains(value.ToLower())).ToList();
            slider.Initialize(Mathf.FloorToInt(list.Count / 10f) + 1);
            slider.FullUp();
        }
    }

    public struct RecipeIcon
    {
        private static Color transparent = new Color(1, 1, 1, 0);

        private ItemIcon icon1, icon2, icon3, icon4;
        private Image im1, im2, im3;

        public RecipeIcon(Transform recipeParent)
        {
            icon1 = new ItemIcon(recipeParent.GetChild(0));
            im1 = recipeParent.GetChild(1).GetComponent<Image>();
            icon2 = new ItemIcon(recipeParent.GetChild(2));
            im2 = recipeParent.GetChild(3).GetComponent<Image>();
            icon3 = new ItemIcon(recipeParent.GetChild(4));
            im3 = recipeParent.GetChild(5).GetComponent<Image>();
            icon4 = new ItemIcon(recipeParent.GetChild(6));
        }

        public void Update(Recipe recipe)
        {
            if (recipe.Input.Length == 3)
            {
                icon1.Update(recipe.Input[0]);
                im1.color = Color.white;
                icon2.Update(recipe.Input[1]);
                im2.color = Color.white;
                icon3.Update(recipe.Input[2]);
                im3.color = Color.white;
                icon4.Update(recipe.Output);
            }
            else
            {
                icon1.Clear();
                im1.color = transparent;
                icon2.Update(recipe.Input[0]);
                im2.color = Color.white;
                icon3.Update(recipe.Input[1]);
                im3.color = Color.white;
                icon4.Update(recipe.Output);
            }
        }
        public void Clear()
        {
            icon1.Clear();
            im1.color = transparent;
            icon2.Clear();
            im2.color = transparent;
            icon3.Clear();
            im3.color = transparent;
            icon4.Clear();
        }
    }
}