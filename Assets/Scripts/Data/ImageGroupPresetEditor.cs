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
                group.Presets = group.Presets.OrderByDescending(p => p.PixelsCount).ToArray();
            }

            if (GUILayout.Button("Setup Blockers"))
            {
                var length = group.Presets.Length;
                var sum = 0;
                for (var i = 0; i < length; i++)
                {
                    var currentSum = group.Presets[i].PixelCountWithPercent;
                    group.Presets[i].LevelActiveThreshold = i < group.FreeLevels ? 0 : sum;
                    sum += currentSum;
                }
            }
        }
    }
}