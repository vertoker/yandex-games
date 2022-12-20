using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(SphereCollider), typeof(DeathableObject))]
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float timeCollectExplodeObjects = 0.1f;
        [SerializeField] private float powerExplode = 20f;
        [SerializeField] private float radiusExplode = 20f;

        private List<DestructableObject> objects = new List<DestructableObject>();
        private DeathableObject self;
        private SphereCollider trigger;
        private Transform tr;

        private void Awake()
        {
            tr = GetComponent<Transform>();
            trigger = GetComponent<SphereCollider>();
            self = GetComponent<DeathableObject>();
            trigger.radius = radiusExplode;
        }
        private void OnEnable()
        {
            self.deathEvent += Explode;
        }
        private void OnDisable()
        {
            self.deathEvent += Explode;
        }

        public void Explode()
        {
            trigger.enabled = true;
            StartCoroutine(CreateObjectList());
        }
        private IEnumerator CreateObjectList()
        {
            yield return new WaitForSeconds(timeCollectExplodeObjects);
            trigger.enabled = false;
            foreach (var item in objects)
            {
                item.Destruct(tr.position, radiusExplode, powerExplode);
            }
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DestructableObject obj))
                objects.Add(obj);
        }
    }
}