using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using Items;
using UnityEditor;
using System;

public class ItemParsing
{
    private static string pathList = "E:\\Projects\\Unity\\Алхимия\\Assets\\Data\\list.txt";
    private static string pathItemsOut = "Assets\\Data\\Items";
    private static string pathRecipesOut = "Assets\\Data\\Recipes";
    private static string pathSprites = "Sprites\\Items";


    [Test]
    public void ItemDependenciesExists()
    {
        string[] recipes = File.ReadAllLines(pathList);
        List<string> items = new List<string>();
        foreach (string recipe in recipes)
        {
            string[] str1 = recipe.Split(" = ");
            string[] str2 = str1[1].Split(" + ");
            items.Add(str1[0]);
            items.AddRange(str2);
        }

        List<string> itemsNoDupes = items.Distinct().ToList();

        for (int i = 0; i < itemsNoDupes.Count; i++)
        {
            string path = Path.Combine(pathSprites, itemsNoDupes[i] + ".png");
            if (!File.Exists(path))
            {
                Debug.Log(string.Format("Файла под именем {0} не существует", path));
                return;
            }
        }

        Debug.Log("Количество предметов: " + itemsNoDupes.Count);
        //Debug.Log(string.Join('\n', itemsNoDupes));
        return;
    }


    [Test]
    public void ItemCreateDependencies()
    {
        string[] recipes = File.ReadAllLines(pathList);
        List<string> items = new List<string>();
        foreach (string recipe in recipes)
        {
            string[] str1 = recipe.Split(" = ");
            string[] str2 = str1[1].Split(" + ");
            items.Add(str1[0]);
            items.AddRange(str2);
        }

        List<string> itemsNoDupes = items.Distinct().ToList();
        int length = itemsNoDupes.Count;

        // Сохранение

        Dictionary<string, Item> itemsDictionary = new Dictionary<string, Item>();
        for (int i = 0; i < length; i++)
        {
            Item asset = ScriptableObject.CreateInstance<Item>();
            asset.Set(itemsNoDupes[i], Resources.Load<Sprite>(Path.Combine(pathSprites, itemsNoDupes[i])));
            itemsDictionary.Add(itemsNoDupes[i], asset);
            AssetDatabase.CreateAsset(asset, Path.Combine(pathItemsOut, itemsNoDupes[i] + ".asset"));
        }

        foreach (string recipe in recipes)
        {
            string[] str1 = recipe.Split(" = ");
            string[] str2 = str1[1].Split(" + ");

            Recipe asset = ScriptableObject.CreateInstance<Recipe>();

            asset.Set(itemsDictionary[str1[0]], Array.ConvertAll(str2, s => itemsDictionary[s]));
            AssetDatabase.CreateAsset(asset, Path.Combine(pathRecipesOut, str1[0] + ".asset"));
        }

        AssetDatabase.SaveAssets();
    }
}
