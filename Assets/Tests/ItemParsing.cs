using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using Items;
using UnityEditor;

public class ItemParsing
{
    private static string pathList = "E:\\Projects\\Unity\\Алхимия\\Assets\\Data\\list.txt";
    private static string pathItemsOut = "E:\\Projects\\Unity\\Алхимия\\Assets\\Data\\Items";
    private static string pathRecipesOut = "E:\\Projects\\Unity\\Алхимия\\Assets\\Data\\Recipes";

    [Test]
    public void ItemParsingSimplePasses()
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
        /*foreach (string item in itemsNoDupes)
            Debug.Log(item);*/

        // Сохранение

        foreach (var item in itemsNoDupes)
        {
            Item asset = ScriptableObject.CreateInstance<Item>();
            asset.Set(item);
            AssetDatabase.CreateAsset(asset, Path.Combine(pathItemsOut, item + ".asset"));
        }

        AssetDatabase.SaveAssets();
    }
}
