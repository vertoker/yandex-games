using UnityEngine;

namespace Core.Audio
{
    public class AudioProvider : MonoBehaviour
    {
        public void Play(string key)
        {
            AudioController.Play(key);
        }
    }
}