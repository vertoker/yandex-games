using UnityEngine;

namespace Core.Prefabs
{
    public class SingletonPrefab : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
