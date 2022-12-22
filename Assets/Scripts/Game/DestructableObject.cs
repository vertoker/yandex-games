using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(DeathableObject))]
    public class DestructableObject : MonoBehaviour
    {
        [SerializeField] private GameObject selfObject;
        [SerializeField] private Transform selfTransform;
        [SerializeField] private Rigidbody selfRigidbody;
        
        public void Destruct(Vector3 epicenter, float radius, float power)
        {
            var direction = selfTransform.position - epicenter;
            var distance = direction.magnitude;
            var actualPower = radius / distance * power;
            selfRigidbody.AddForce(direction.normalized * actualPower, ForceMode.Impulse);
        }
    }
}