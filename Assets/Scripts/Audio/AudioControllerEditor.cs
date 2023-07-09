#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Audio
{
    [CustomEditor(typeof(AudioController))]
    public class AudioControllerEditor : Editor
    {
        private static string AudioPath = "StreamingAssets/Audio/";

        public override void OnInspectorGUI()
        {
            var controller = (AudioController)target;

            if (controller == null) return;

            if (GUILayout.Button("Load audio"))
            {
                var clips = LoadClipNames();
                controller.SetupClips(clips);
            }
            
            base.OnInspectorGUI();
        }

        private string[] LoadClipNames()
        {
            var fullPath = Path.Combine(Application.dataPath, AudioPath);
            var files = Directory.GetFiles(fullPath);
                
            var length = files.Length;
            var fileNames = new List<string>(length);

            for (var i = 0; i < length; i++)
            {
                var info = new FileInfo(files[i]);
                var fileName = info.Name;
                if (Enum.TryParse(fileName.Split('.')[^1].ToUpper(), out AudioType extension))
                    fileNames.Add(fileName);
            }
            
            return fileNames.ToArray();
        }
    }
}
#endif