#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Data
{
    public static class ImagePresetTool
    {
        [MenuItem("Tools/Create Presets from Selected images")]
        public static void CreateFromSprites()
        {
            var objects = Selection.objects;
            var length = objects.Length;
            for (var i = 0; i < length; i++)
            {
                var path = AssetDatabase.GetAssetPath(objects[i]);
                var fileName = Path.GetFileName(path);
                var name = fileName.Split('.')[0];
                if (string.IsNullOrEmpty(path))
                    path = "Assets";
                else if (!string.IsNullOrEmpty(Path.GetExtension(path)))
                    path = path.Replace(fileName, string.Empty);
                path = Path.Combine(path, $"{name} Item.asset");
                
                var sprite = (Sprite)objects[i];
                var item = ScriptableObject.CreateInstance<ImagePreset>();
                AssetDatabase.CreateAsset(item, path);
                item.ImageSource = sprite;
                item.ImageTitle = name;
                item.OnValidate();
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif