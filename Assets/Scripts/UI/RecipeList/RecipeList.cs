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
        [SerializeField] private Camera cam;
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
            listWindowAnimator.CloseEndEvent += Clear;
        }
        private void OnDisable()
        {
            listWindowAnimator.OpenStartEvent -= UpdateRecipe;
            listWindowAnimator.CloseEndEvent -= Clear;
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
            self.sizeDelta = new Vector2(1080f * cam.aspect, 1080f);
            list = SaveSystem.SaveSystem.GetListRecipes().ToList();
            list.Remove("Вода");
            list.Remove("Земля");
            list.Remove("Огонь");
            list.Remove("Воздух");

            int listCount = Mathf.FloorToInt((float)list.Count / icons.Length) + 1;
            if (listCount > 2)
            {
                slider.maxValue = listCount;
                slider.value = 1;
            }
            else
            {
                slider.maxValue = 2f;
                slider.value = 1;
            }
            slider.interactable = listCount > 1;
            slider.onValueChanged.Invoke(1);
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
        public void Close()
        {
            listWindowAnimator.Close();
        }
        private void Clear()
        {
            inputField.text = string.Empty;
        }

        public IEnumerator SearchTask(string value)
        {
            yield return null;

            self.sizeDelta = new Vector2(1080f * cam.aspect, 1080f);
            list = SaveSystem.SaveSystem.GetListRecipes().ToList();
            list.Remove("Вода");
            list.Remove("Земля");
            list.Remove("Огонь");
            list.Remove("Воздух");
            value = value.ToLower();

            list = list.Where(s => ItemSpawner.RecipeDictionary[s].Contains(value)).ToList();

            int listCount = Mathf.FloorToInt((float)list.Count / icons.Length) + 1;
            if (listCount > 2)
            {
                slider.maxValue = listCount;
                slider.value = 1;
            }
            else
            {
                slider.maxValue = 2f;
                slider.value = 1;
            }
            slider.interactable = listCount > 1;
            slider.onValueChanged.Invoke(1);
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
            bg.color = transparent;
            im1.color = transparent;
            im2.color = transparent;
            im3.color = transparent;

            icon1.Clear();
            icon2.Clear();
            icon3.Clear();
            icon4.Clear();

            icon1.SetActive(false);
            icon2.SetActive(false);
            icon3.SetActive(false);
            icon4.SetActive(false);
        }
    }
}