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
        }
        private void OnDisable()
        {
            listWindowAnimator.OpenStartEvent -= UpdateRecipe;
            //listWindowAnimator.CloseEndEvent -= Close;
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
            list = SaveSystem.SaveSystem.GetListRecipes().ToList();
            list.Remove("Вода");
            list.Remove("Земля");
            list.Remove("Огонь");
            list.Remove("Воздух");
            slider.maxValue = Mathf.FloorToInt(list.Count / 10f);
            slider.maxValue = slider.maxValue < 1 ? 1 : slider.maxValue;
            slider.value = 1f;
        }
        public void UpdateSlider()
        {
            List<string> showList;

            if (list.Count - icons.Length >= ((int)slider.value - 1) * icons.Length)
                showList = list.GetRange(((int)slider.value - 1) * icons.Length, icons.Length);
            else
                showList = list.GetRange(((int)slider.value - 1) * icons.Length, list.Count - ((int)slider.value - 1) * icons.Length);

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
            list = SaveSystem.SaveSystem.GetListRecipes().ToList();
            list.Remove("Вода");
            list.Remove("Земля");
            list.Remove("Огонь");
            list.Remove("Воздух");
            value = value.ToLower();

            list = list.Where(s => ItemSpawner.RecipeDictionary[s].Contains(value)).ToList();
            slider.maxValue = Mathf.FloorToInt(list.Count / 10f);
            slider.maxValue = slider.maxValue < 1 ? 1 : slider.maxValue;
            slider.value = 1f;
        }
    }

    public struct RecipeIcon
    {
        private static Color transparent = new Color(1, 1, 1, 0);
        private static Color gold = new Color(0.8f, 0.6f, 0.2f, 0.2f);

        private ItemIcon icon1, icon2, icon3, icon4;
        private Image bg, im1, im2, im3;

        public RecipeIcon(Transform recipeParent)
        {
            bg = recipeParent.GetComponent<Image>();
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
            bool isRecipeTrue = SaveSystem.SaveSystem.GetString(recipe.Output.Name) != SaveSystem.SaveSystem.ONLYRECIPE;
            if (recipe.Input.Length == 3)
            {
                im1.color = Color.white;
                im2.color = Color.white;
                im3.color = Color.white;

                icon1.Update(recipe.Input[0]);
                icon2.Update(recipe.Input[1]);
                icon3.Update(recipe.Input[2]);
                icon4.Update(recipe.Output);
            }
            else
            {
                im1.color = transparent;
                im2.color = Color.white;
                im3.color = Color.white;

                icon1.Clear();
                icon2.Update(recipe.Input[0]);
                icon3.Update(recipe.Input[1]);
                icon4.Update(recipe.Output);
            }

            icon1.SetActive(isRecipeTrue);
            icon2.SetActive(isRecipeTrue);
            icon3.SetActive(isRecipeTrue);
            icon4.SetActive(isRecipeTrue);

            if (isRecipeTrue)
                bg.color = transparent;
            else
                bg.color = gold;
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