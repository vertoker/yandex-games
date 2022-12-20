using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(DeathableObject))]
    public class DestructableObject : MonoBehaviour
    {
        private GameObject selfObject;
        private Transform selfTransform;
        private Rigidbody selfRigidbody;

        private void Awake()
        {
            selfObject = gameObject;
            selfTransform = transform;
            selfRigidbody = GetComponent<Rigidbody>();
        }
        public void Destruct(Vector3 epicenter, float radius, float power)
        {
            var direction = selfTransform.position - epicenter;
            var distance = direction.magnitude;
            var actualPower = radius / distance * power;
            selfRigidbody.AddForce(direction.normalized * actualPower, ForceMode.Impulse);
        }
    }
}