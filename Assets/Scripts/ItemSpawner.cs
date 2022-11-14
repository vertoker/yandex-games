using System.Collections.Generic;
using System.Linq;
using Scripts.Items;
using Scripts.Utils;
using UnityEngine;
using Game.Pool;
using UnityEngine.Events;

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

        [SerializeField] private float radius = 0.5f;

        private List<GameObject> activeObjects = new List<GameObject>();

        private static ItemSpawner Instance;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            CreateItem("Вода");
            CreateItem("Огонь");
            CreateItem("Земля");
            CreateItem("Воздух");
        }

        public void CreateItem(string name)
        {
            CreateItem(name, Math.GetRandomPointInCircle(radius));
        }
        public void CreateItem(string name, Vector2 position)
        {
            var item = items.FirstOrDefault((Items.Item i) => { return i.Name == name; });
            if (item != null)
            {
                var obj = itemSpawner.Dequeue();
                obj.transform.position = position;
                obj.GetComponent<SpriteRenderer>().sprite = item.Sprite;
                obj.name = item.Name;
                activeObjects.Add(obj);
            }
        }
        public void DeleteItem()
        {

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
                        RecipeSuccess(recipe, position);
                        Instance.itemSpawner.Enqueue(obj3);
                        Instance.itemSpawner.Enqueue(obj2);
                        Instance.itemSpawner.Enqueue(obj1);
                        return;
                    }
                }
            }
            foreach (var item in dependence1.Dependencies)
            {
                var recipe = Instance.recipes.FirstOrDefault(x => x.GetRecipe(item1, item2, item3));
                if (recipe != null)
                {
                    RecipeSuccess(recipe, position);
                    Instance.itemSpawner.Enqueue(obj2);
                    Instance.itemSpawner.Enqueue(obj1);
                    return;
                }
            }
            RecipeFail(position);
        }
        private static void RecipeSuccess(Recipe recipe, Vector2 position)
        {
            Instance.CreateItem(recipe.Output.Name, position);
            Destroy(Instantiate(Instance.successEffect, position, Quaternion.identity, Instance.transform), 1f);
        }
        private static void RecipeFail(Vector2 position)
        {
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
                data.CreateItem(nameItem);
            }
        }
    }
#endif
}