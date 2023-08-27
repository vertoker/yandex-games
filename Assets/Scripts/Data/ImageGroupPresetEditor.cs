using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Data
{
    [CustomEditor(typeof(ImageGroupPreset))]
    public class ImageGroupPresetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var group = (ImageGroupPreset)target;
            if (group == null) return;

            if (GUILayout.Button("Order By pixels count"))
            {
                group.Presets = group.Presets.OrderBy(p => p.PixelsCount).ToArray();
            }

            if (GUILayout.Button("Setup Blockers"))
            {
                var length = group.Presets.Length;
                var sum = 0;
                for (var i = 0; i < length; i++)
                {
                    if (i < group.FreeLevels)
                    {
                        group.Presets[i].LevelActiveThreshold = 0;
                    }
                    else
                    {
                        sum += group.Presets[i].PixelCountWithPercent;
                        var threshold = Mathf.FloorToInt(sum / 100f) * 100;
                        group.Presets[i].LevelActiveThreshold = threshold;
                    }
                }
            }
        }
    }
}