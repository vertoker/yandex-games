using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(SphereCollider), typeof(DeathableObject))]
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float timeCollectExplodeObjects = 0.1f;
        [SerializeField] private float radiusExplode = 20f;

        private List<Transform> objects = new List<Transform>();
        private DeathableObject self;
        private SphereCollider trigger;

        private void Awake()
        {
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
            Debug.Log(objects.Count);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            objects.Add(other.transform);
        }
    }
}