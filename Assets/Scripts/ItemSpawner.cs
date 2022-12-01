using System.Collections.Generic;
using Scripts.Audio;
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

        [SerializeField] private List<Item> items;
        [SerializeField] private List<Recipe> recipes;
        [SerializeField] private List<ItemDependence> dependencies;

        [SerializeField] private Dictionary<string, Item> itemDictionary;
        [SerializeField] private Dictionary<string, Recipe> recipeDictionary;

        [SerializeField] private float radius = 0.5f;

        private List<GameObject> activeObjects = new List<GameObject>();


        private static ItemSpawner Instance;

        public static List<Item> Items => Instance.items;
        public static Dictionary<string, Item> ItemDictionary => Instance.itemDictionary;
        public static Dictionary<string, Recipe> RecipeDictionary => Instance.recipeDictionary;

        [RuntimeInitializeOnLoadMethod]
        private void Awake()
        {
            Instance = this;

            itemDictionary = new Dictionary<string, Items.Item>();
            for (int i = 0; i < items.Count; i++)
                itemDictionary.Add(items[i].Name, items[i]);

            recipeDictionary = new Dictionary<string, Recipe>();
            for (int i = 0; i < recipes.Count; i++)
                recipeDictionary.Add(recipes[i].Output.Name, recipes[i]);
        }
        private void Start()
        {
            CreateItem("����", true);
            CreateItem("�����", true);
            CreateItem("�����", true);
            CreateItem("������", true);
        }

        public static void CreateItem(string name, bool startSpawn = false)
        {
            CreateItem(name, Math.GetRandomPointInCircle(Instance.radius), startSpawn);
        }
        public static void CreateItem(string name, Vector2 position, bool startSpawn = false)
        {
            var item = Instance.items.FirstOrDefault((Items.Item i) => { return i.Name == name; });
            if (item != null)
            {
                var obj = Instance.itemSpawner.Dequeue();
                obj.transform.position = position;
                obj.GetComponent<SpriteRenderer>().sprite = item.Sprite;
                obj.transform.GetChild(0).GetComponent<TextMesh>().text = item.Name;
                obj.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = item.Sprite;

                obj.name = item.Name;
                Instance.activeObjects.Add(obj);
                if (!startSpawn)
                {
                    SaveSystem.SaveSystem.Unlock(name);
                    AudioResourcesPlay.Play("Fail");
                }
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
            var dependence3 = Instance.dependencies.FirstOrDefault(x => x.Name == item2);
            bool use1 = false, use2 = false, use3 = false;

            foreach (var item in dependence1.Dependencies3)
            {
                if (RecipeDictionary[item].GetRecipe(item1, item2, item3, out use1, out use2, out use3))
                {
                    //Debug.Log(recipe.Output.Name);
                    RecipeSuccess(RecipeDictionary[item], position);
                    if (use1)
                        DeleteItem(obj1);
                    if (use2)
                        DeleteItem(obj2);
                    if (use3)
                        DeleteItem(obj3);
                    return;
                }
            }
            foreach (var item in dependence2.Dependencies3)
            {
                if (RecipeDictionary[item].GetRecipe(item1, item2, item3, out use1, out use2, out use3))
                {
                    //Debug.Log(recipe.Output.Name);
                    RecipeSuccess(RecipeDictionary[item], position);
                    if (use1)
                        DeleteItem(obj1);
                    if (use2)
                        DeleteItem(obj2);
                    if (use3)
                        DeleteItem(obj3);
                    return;
                }
            }
            foreach (var item in dependence3.Dependencies3)
            {
                if (RecipeDictionary[item].GetRecipe(item1, item2, item3, out use1, out use2, out use3))
                {
                    //Debug.Log(recipe.Output.Name);
                    RecipeSuccess(RecipeDictionary[item], position);
                    if (use1)
                        DeleteItem(obj1);
                    if (use2)
                        DeleteItem(obj2);
                    if (use3)
                        DeleteItem(obj3);
                    return;
                }
            }
            foreach (var item in dependence1.Dependencies2)
            {
                if (RecipeDictionary[item].GetRecipe(item1, item2, item3, out use1, out use2, out use3))
                {
                    //Debug.Log(recipe.Output.Name);
                    RecipeSuccess(RecipeDictionary[item], position);
                    if (use1)
                        DeleteItem(obj1);
                    if (use2)
                        DeleteItem(obj2);
                    if (use3)
                        DeleteItem(obj3);
                    return;
                }
            }
            foreach (var item in dependence2.Dependencies2)
            {
                if (RecipeDictionary[item].GetRecipe(item1, item2, item3, out use1, out use2, out use3))
                {
                    //Debug.Log(recipe.Output.Name);
                    RecipeSuccess(RecipeDictionary[item], position);
                    if (use1)
                        DeleteItem(obj1);
                    if (use2)
                        DeleteItem(obj2);
                    if (use3)
                        DeleteItem(obj3);
                    return;
                }
            }
            foreach (var item in dependence3.Dependencies2)
            {
                if (RecipeDictionary[item].GetRecipe(item1, item2, item3, out use1, out use2, out use3))
                {
                    //Debug.Log(recipe.Output.Name);
                    RecipeSuccess(RecipeDictionary[item], position);
                    if (use1)
                        DeleteItem(obj1);
                    if (use2)
                        DeleteItem(obj2);
                    if (use3)
                        DeleteItem(obj3);
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
            AudioResourcesPlay.Play("Success");
        }
        private static void RecipeFail(Vector2 position)
        {
            //Debug.Log("Fail");
            Destroy(Instantiate(Instance.failEffect, position, Quaternion.identity, Instance.transform), 1f);
            AudioResourcesPlay.Play("Fail");
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