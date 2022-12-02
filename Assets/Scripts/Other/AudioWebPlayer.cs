using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using Unity.VisualScripting;
using System.IO;

namespace Scripts.Audio
{
    public class AudioWebPlayer : MonoBehaviour
    {
        [SerializeField] protected List<Clip> assets;
        private static AudioWebPlayer instance;

        private void Awake()
        {
            instance = this;
            CreateAll();
            LoadAll();
        }

        private void CreateAll()
        {
            foreach (var clip in assets)
            {
                clip.SetSource(transform.AddComponent<AudioSource>());
            }
        }
        private void LoadAll()
        {
            foreach (var clip in assets)
            {
                var task = GetFile(clip, clip.SetClip);
            }
        }

        public async Task GetFile(Clip clip, Action<AudioClip> action)
        {
            UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(clip.FilePath, clip.Extension);
            request.SendWebRequest();
            while (!request.isDone)
                await Task.Yield();
            AudioClip cash = DownloadHandlerAudioClip.GetContent(request);
            action?.Invoke(cash);
        }

        public static void Play(string name)
        {
            instance.assets.FirstOrDefault((Clip c) => { return c.SoundName == name; }).Play();
        }
    }

    [Serializable]
    public class Clip
    {
        [SerializeField] private string soundName;
        [SerializeField] private AudioType soundExtension;
        private AudioSource source;

        public string SoundName => soundName;
        public AudioType Extension => soundExtension;
        public string FilePath => Path.Combine(Application.streamingAssetsPath, string.Join('.', soundName, soundExtension.ToString().ToLower()));

        public void SetSource(AudioSource source) => this.source = source;
        public void SetClip(AudioClip clip)
        {
            source.clip = clip;
        }
        public void Play()
        {
            if (source.clip != null)
                source.Play();
        }
    }
}