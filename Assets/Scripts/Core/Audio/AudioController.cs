using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Task = System.Threading.Tasks.Task;

namespace Core.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] protected int startAudioSourceSize = 10;
        [SerializeField] protected AudioWebClip[] assets;
        private static AudioController _instance;

        private Stack<AudioSource> _sourcePool;
        
        public Action AudioLoaded;
        private int _counter;

        private void Awake()
        {
            if (_instance != null)
                return;
            _instance = this;

            _sourcePool = new Stack<AudioSource>(startAudioSourceSize);
            for (var i = 0; i < startAudioSourceSize; i++)
                _sourcePool.Push(Create());
            
            // Create All
            foreach (var clip in assets)
            {
                clip.SetSource(_sourcePool);
                clip.SetAddAction(Create);
            }

            // Load All
            _counter = 0;
            var length = assets.Length;
            foreach (var clip in assets)
                StartCoroutine(GetFile(clip, length));
        }
        private AudioSource Create() => gameObject.AddComponent<AudioSource>();
        
        private IEnumerator GetFile(AudioWebClip clip, int length)
        {
            var request = UnityWebRequestMultimedia.GetAudioClip(clip.FilePath, clip.Extension);
            //Debug.Log(clip.FilePath);
            request.SendWebRequest();
            while (!request.isDone)
                yield return new WaitForSeconds(0.2f);
            var cash = DownloadHandlerAudioClip.GetContent(request);
            clip.SetClip(cash);

            _counter++;
            if (_counter != length) yield break;
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

        public static void Play(string name)
        {
            //if (false)//Application.isFocused)
                _instance.assets.FirstOrDefault(c => c.SoundName == name)?.Play(_instance);
        }
        public static void Play(int index)
        {
            //if (false)//Application.isFocused)
            _instance.assets[index]?.Play(_instance);
        }
    }
}