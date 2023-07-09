using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using Task = System.Threading.Tasks.Task;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] protected AudioWebClip[] assets;
        private static AudioController _instance;

        public Action AudioLoaded;

        private async void Awake()
        {
            if (_instance != null)
                return;
            _instance = this;
            
            // Create All
            foreach (var clip in assets)
                clip.SetSource(transform.AddComponent<AudioSource>());
            
            // Load All
            foreach (var clip in assets)
                await GetFile(clip, clip.SetClip);
            
            Debug.Log("Audio loaded");
            AudioLoaded?.Invoke();
        }

        public void SetupClips(string[] clipNames)
        {
            var length = clipNames.Length;
            assets = new AudioWebClip[length];
            for (var i = 0; i < length; i++)
                assets[i] = new AudioWebClip(clipNames[i]);
        }

        private static async Task GetFile(AudioWebClip clip, Action<AudioClip> action)
        {
            var request = UnityWebRequestMultimedia.GetAudioClip(clip.FilePath, clip.Extension);
            //Debug.Log(clip.FilePath);
            request.SendWebRequest();
            while (!request.isDone)
                await Task.Yield();
            //Debug.Log("Done 1");
            var cash = DownloadHandlerAudioClip.GetContent(request);
            //Debug.Log("Done 2");
            action?.Invoke(cash);
        }

        public static void Play(string name)
        {
            _instance.assets.FirstOrDefault(c => c.SoundName == name)?.Play();
        }
    }
}