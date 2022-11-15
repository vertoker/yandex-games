using System.Collections;
using Scripts.UI.ItemList;
using UnityEngine.UI;
using UnityEngine;
using Scripts.Items;
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
            }
        }

        private void UpdateRecipe()
        {
            var list = SaveSystem.SaveSystem.GetList().ToList();
            list.Remove("Вода");
            list.Remove("Земля");
            list.Remove("Огонь");
            list.Remove("Воздух");

            for (int i = 0; i < list.Count; i++)
                icons[i].Update(ItemSpawner.RecipeDictionary[list[i]]);
            for (int i = list.Count; i < icons.Length; i++)
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

        public IEnumerator SearchTask(string value)
        {
            yield return null;
            var list = SaveSystem.SaveSystem.GetList();
            list = list.Where(s => s.ToLower().Contains(value.ToLower())).ToArray();
            for (int i = 0; i < list.Length; i++)
                icons[i].Update(ItemSpawner.RecipeDictionary[list[i]]);
            for (int i = list.Length; i < icons.Length; i++)
                icons[i].Clear();
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