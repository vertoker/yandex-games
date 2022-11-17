using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Scripts
{
    public class AudioCaller : MonoBehaviour
    {
        [SerializeField] private AudioClip fail, success;
        [SerializeField] private AudioSource source;

        private static AudioCaller instance;

        private void Awake()
        {
            instance = this;
            source.volume = 0.5f;
        }

        public void Call(int id)
        {
            //Call((AudioType)id);
        }
        public static void Call(AudioType audioType)
        {
            switch (audioType)
            {
                case AudioType.Fail:
                    instance.source.clip = instance.fail;
                    instance.source.Play();
                    break;
                case AudioType.Success:
                    instance.source.clip = instance.success;
                    instance.source.Play();
                    break;
            }
        }
    }

    public enum AudioType : byte
    {
        Fail = 0, Success = 1
    }
}