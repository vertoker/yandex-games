using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.Audio
{
    [Serializable]
    public class AudioWebClip
    {
        [SerializeField] private string soundName;
        [SerializeField] private AudioType soundExtension;
        
        private Stack<AudioSource> _stack;
        private AudioClip _clip;

        public AudioWebClip(string clipName)
        {
            var split = clipName.Split('.');
            if (Enum.TryParse(split[^1].ToUpper(), out soundExtension))
                soundName = clipName.Replace($".{split[^1]}", string.Empty);
        }

        public string SoundName => soundName;
        public AudioType Extension => soundExtension;
        public string FilePath => Path.Combine(Application.streamingAssetsPath,
            "Audio", string.Join('.', soundName, soundExtension.ToString().ToLower()));

        public void SetSource(Stack<AudioSource> stack) => _stack = stack;
        public void SetClip(AudioClip clip) 
        {
            _clip = clip;
        }
        
        public void Play(MonoBehaviour processor)
        {
            var source = _stack.Pop();
            source.clip = _clip;
            source.Play();
            processor.StartCoroutine(Off(source));
        }
        private IEnumerator Off(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            _stack.Push(source);
        }
    }
}