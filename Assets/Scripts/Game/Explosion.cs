using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(SphereCollider), typeof(DeathableObject))]
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float chainExplosionDelay = 0.5f;
        [SerializeField] private float timeCollectExplodeObjects = 0.1f;
        [SerializeField] private float powerExplode = 20f;
        [SerializeField] private float radiusExplode = 20f;

        private List<DestructableObject> objects;
        private DeathableObject self;
        private SphereCollider trigger;
        private Transform tr;

        public Vector3 Position => tr.position;

        private void Awake()
        {
            tr = GetComponent<Transform>();
            trigger = GetComponent<SphereCollider>();
            self = GetComponent<DeathableObject>();
            trigger.radius = radiusExplode;
        }
        private void OnEnable()
        {
            self.DeathEvent += Explode;
        }
        private void OnDisable()
        {
            self.DeathEvent -= Explode;
        }

        private void ExplodeDelay()
        {
            StartCoroutine(DelayExplosion());
        }
        private IEnumerator DelayExplosion()
        {
            yield return new WaitForSeconds(chainExplosionDelay);
            Explode();
        }
        private void Explode()
        {
            trigger.enabled = true;
            objects = new List<DestructableObject>();
            StartCoroutine(CreateObjectList());
        }
        private IEnumerator CreateObjectList()
        {
            yield return new WaitForSeconds(timeCollectExplodeObjects);
            trigger.enabled = false;
            ExplosionComputator.ComputateExplosion(tr.position, radiusExplode, powerExplode, objects);
            PoolEffects.ExecuteExplosion(this);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DestructableObject obj))
                objects.Add(obj);
            else if (other.TryGetComponent(out Explosion exp))
                exp.ExplodeDelay();
        }
    }
}