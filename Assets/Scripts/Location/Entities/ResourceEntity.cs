using Scripts.Game.Controller;
using Scripts.Location.Extraction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

namespace Scripts.Location.Entities
{
    [RequireComponent(typeof(ResourceCaller), typeof(AnimationExtractionActivator))]
    public class ResourceEntity : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 120;
        [SerializeField] private int[] damages = new int[5] { 24, 30, 40, 60, 120 };//5 4 3 2 1
        [SerializeField] private float timeRespawn = 5f;
        private PersonController personController;

        [Header("Animation Data")]
        [SerializeField] private float animRespawnLength = 0.5f;
        [SerializeField] private float animDespawnLength = 0.5f;
        [SerializeField] private float animExtractImpactLength = 0.2f;
        [SerializeField] private Vector2 shakeAngle = new Vector2(3f, 3f);
        [SerializeField] private float disableHeigth = -10f;
        [SerializeField] private Transform mesh;
        private float defaultHeigth;
        [Header("Current Data")]
        [SerializeField] private int currentHealth;

        [SerializeField] private TriggerCaller mainCaller;
        private AnimationExtractionActivator animActivator;
        private ResourceCaller caller;
        private bool active = true;

        private void Awake()
        {
            animActivator = GetComponent<AnimationExtractionActivator>();
            caller = GetComponent<ResourceCaller>();
            defaultHeigth = mesh.localPosition.y;
            Respawn();
        }
        private void OnEnable()
        {
            mainCaller.triggerEnterEvent += animActivator.TriggerEnter;
            mainCaller.triggerEnterEvent += caller.TriggerEnter;
            mainCaller.triggerEnterEvent += TriggerEnter;
            mainCaller.triggerExitEvent += animActivator.TriggerExit;
            mainCaller.triggerExitEvent += caller.TriggerExit;
            mainCaller.triggerExitEvent += TriggerExit;
        }
        private void OnDisable()
        {
            mainCaller.triggerEnterEvent -= animActivator.TriggerEnter;
            mainCaller.triggerEnterEvent -= caller.TriggerEnter;
            mainCaller.triggerEnterEvent -= TriggerEnter;
            mainCaller.triggerExitEvent -= animActivator.TriggerExit;
            mainCaller.triggerExitEvent -= caller.TriggerExit;
            mainCaller.triggerExitEvent -= TriggerExit;
        }

        private void TriggerEnter(Collider other)
        {
            if (other.CompareTag("Person"))
            {
                personController = other.GetComponent<PersonController>();
                personController.extractionUpdate += Extraction;
            }
        }
        private void TriggerExit(Collider other)
        {
            if (other.CompareTag("Person"))
            {
                personController.extractionUpdate -= Extraction;
                personController = null;
            }
        }
        private void Extraction()
        {
            Damage(damages[(int)YandexGame.savesData.data.GetMaterial(caller.ToolType)]);
        }

        public void Respawn()
        {
            currentHealth = maxHealth;
            animActivator.SetActive(true);
            active = true;
        }
        public void Damage(int damage)
        {
            if (!active)
                return;
            currentHealth -= damage;
            StartCoroutine(AnimExtractImpact());
            if (currentHealth <= 0)
            {
                Despawn();
            }
        }
        public void Despawn()
        {
            active = false;
            animActivator.SetActive(false);
            StartCoroutine(AnimDespawn());
        }

        public IEnumerator AnimExtractImpact()
        {
            for (float i = 0; i <= animExtractImpactLength; i += Time.deltaTime)
            {
                float x = Random.Range(-shakeAngle.x, shakeAngle.x);
                float z = Random.Range(-shakeAngle.y, shakeAngle.y);
                mesh.localEulerAngles = new Vector3(x, 0f, z);
                yield return null;
            }
            mesh.localEulerAngles = Vector3.zero;
        }
        public IEnumerator AnimRespawn()
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime / animRespawnLength)
            {
                mesh.localPosition = new Vector3(0f, defaultHeigth + i * disableHeigth);
                yield return null;
            }
            mesh.localPosition = new Vector3(0f, defaultHeigth);
            Respawn();
        }
        public IEnumerator AnimDespawn()
        {
            for (float i = 0; i <= 1; i += Time.deltaTime / animDespawnLength)
            {
                mesh.localPosition = new Vector3(0f, defaultHeigth + i * disableHeigth);
                yield return null;
            }
            mesh.localPosition = new Vector3(0f, defaultHeigth + disableHeigth);
            StartCoroutine(TimeDelay());
        }
        public IEnumerator TimeDelay()
        {
            yield return new WaitForSeconds(timeRespawn);
            StartCoroutine(AnimRespawn());
        }
    }
}