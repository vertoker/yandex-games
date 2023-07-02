using System;
using System.IO;
using UnityEngine;

namespace Core.Audio
{
    [Serializable]
    public class AudioWebClip
    {
        [SerializeField] private string soundName;
        [SerializeField] private AudioType soundExtension;
        private AudioSource _source;

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

        public void SetSource(AudioSource source) => this._source = source;
        public void SetClip(AudioClip clip) 
        {
            _source.clip = clip;
        }
        public void Play()
        {
            if (_source.clip != null)
                _source.Play();
        }
    }
}