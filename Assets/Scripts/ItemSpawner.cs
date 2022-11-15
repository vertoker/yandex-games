using System.Collections.Generic;
using Scripts.Items;
using Scripts.Utils;
using UnityEngine;
using System.Linq;
using Game.Pool;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Scripts
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private PoolSpawner itemSpawner;
        [SerializeField] private GameObject successEffect;
        [SerializeField] private GameObject failEffect;

        [SerializeField] private List<Items.Item> items;
        [SerializeField] private List<Recipe> recipes;
        [SerializeField] private List<ItemDependence> dependencies;

        [SerializeField] private Dictionary<string, Items.Item> itemDictionary;
        [SerializeField] private Dictionary<string, Recipe> recipeDictionary;

        [SerializeField] private float radius = 0.5f;

        private List<GameObject> activeObjects = new List<GameObject>();

        private static ItemSpawner Instance;

        public static List<Items.Item> Items => Instance.items;
        public static Dictionary<string, Items.Item> ItemDictionary => Instance.itemDictionary;
        public static Dictionary<string, Recipe> RecipeDictionary => Instance.recipeDictionary;

        private void Awake()
        {
            Instance = this;

            itemDictionary = new Dictionary<string, Items.Item>();
            for (int i = 0; i < items.Count; i++)
                itemDictionary.Add(items[i].Name, items[i]);

            recipeDictionary = new Dictionary<string, Recipe>();
            for (int i = 0; i < items.Count; i++)
                recipeDictionary.Add(recipes[i].Output.Name, recipes[i]);
        }
        private void Start()
        {
            CreateItem("Вода");
            CreateItem("Огонь");
            CreateItem("Земля");
            CreateItem("Воздух");
        }

        public static void CreateItem(string name)
        {
            CreateItem(name, Math.GetRandomPointInCircle(Instance.radius));
        }
        public static void CreateItem(string name, Vector2 position)
        {
            var item = Instance.items.FirstOrDefault((Items.Item i) => { return i.Name == name; });
            if (item != null)
            {
                var obj = Instance.itemSpawner.Dequeue();
                obj.transform.position = position;
                obj.GetComponent<SpriteRenderer>().sprite = item.Sprite;
                obj.transform.GetChild(0).GetComponent<TextMesh>().text = item.Name;
                obj.name = item.Name;
                Instance.activeObjects.Add(obj);
                SaveSystem.SaveSystem.Unlock(name);
            }
        }
        public static void DeleteItem(GameObject obj)
        {
            Instance.itemSpawner.Enqueue(obj);
            Instance.activeObjects.Remove(obj);
        }
        public void DeleteAll()
        {
            for (int i = 0; i < activeObjects.Count; i++)
                itemSpawner.Enqueue(activeObjects[i]);
            activeObjects.Clear();
        }

        public void DisableItem(GameObject obj)
        {
            obj.GetComponent<CircleCollider2D>().enabled = false;
        }
        public void EnableItem(GameObject obj)
        {
            obj.GetComponent<CircleCollider2D>().enabled = true;
        }

        public static void ExecuteRecipe(string item1, string item2, string item3, Vector2 position, GameObject obj1, GameObject obj2, GameObject obj3)
        {
            var dependence1 = Instance.dependencies.FirstOrDefault(x => x.Name == item1);
            var dependence2 = Instance.dependencies.FirstOrDefault(x => x.Name == item2);

            if (item3 != string.Empty)
            {
                foreach (var item in dependence2.Dependencies)
                {
                    var recipe = Instance.recipes.FirstOrDefault(x => x.GetRecipe(item1, item2, item3));
                    if (recipe != null)
                    {
                        Debug.Log(recipe.Output.Name);
                        RecipeSuccess(recipe, position);
                        DeleteItem(obj3);
                        DeleteItem(obj2);
                        DeleteItem(obj1);
                        return;
                    }
                }
            }
            foreach (var item in dependence1.Dependencies)
            {
                var recipe = Instance.recipes.FirstOrDefault(x => x.GetRecipe(item1, item2, item3));
                if (recipe != null)
                {
                    Debug.Log(recipe.Output.Name);
                    RecipeSuccess(recipe, position);
                    DeleteItem(obj2);
                    DeleteItem(obj1);
                    return;
                }
            }
            RecipeFail(position);
        }
        private static void RecipeSuccess(Recipe recipe, Vector2 position)
        {
            //Debug.Log(recipe.Output.Name);
            CreateItem(recipe.Output.Name, position);
            Destroy(Instantiate(Instance.successEffect, position, Quaternion.identity, Instance.transform), 1f);
        }
        private static void RecipeFail(Vector2 position)
        {
            //Debug.Log("Fail");
            Destroy(Instantiate(Instance.failEffect, position, Quaternion.identity, Instance.transform), 1f);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ItemSpawner))]
    class EditorSpawner : Editor
    {
        string nameItem;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ItemSpawner data = (ItemSpawner)target;

            EditorGUILayout.Space(30);
            nameItem = EditorGUILayout.TextField(nameItem);
            if (GUILayout.Button("Spawn"))
            {
                ItemSpawner.CreateItem(nameItem);
            }
        }
    }
#endif
}