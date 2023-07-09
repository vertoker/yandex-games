using UnityEngine;

namespace Prefabs
{
    public class SingletonPrefab : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
