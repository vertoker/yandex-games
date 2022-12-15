using Scripts.Game.Controller;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Location.Extraction
{
    public class AnimationExtractionActivator : MonoBehaviour
    {
        [SerializeField] private float extractionStartTime = 0.25f;
        private PersonController personController;
        private Coroutine extractionStart;
        private Transform self;
        private bool active = true;
        private bool inTrigger = false;

        public void SetActive(bool value)
        {
            active = value;
            if (value)
            {
                if (inTrigger)
                {
                    if (extractionStart != null)
                        StopCoroutine(extractionStart);
                    extractionStart = StartCoroutine(AnimStart());
                }
            }
            else
            {
                personController.EndAction(true);
            }
        }
        private void Awake()
        {
            self = transform;
        }

        public void TriggerEnter(Collider other)
        {
            if (other.CompareTag("Person"))
            {
                inTrigger = true;
                if (active)
                    EnableTrigger(other);
            }
        }
        public void TriggerExit(Collider other)
        {
            if (other.CompareTag("Person"))
            {
                inTrigger = false;
                DisableTrigger();
            }
        }
        private void EnableTrigger(Collider other)
        {
            personController = other.GetComponent<PersonController>();
            personController.movementUpdate += PersonMove;
            if (extractionStart != null)
                StopCoroutine(extractionStart);
            extractionStart = StartCoroutine(AnimStart());
        }
        private void DisableTrigger()
        {
            personController.movementUpdate -= PersonMove;
            //personController = null;
            StopCoroutine(extractionStart);
        }

        private void PersonMove()
        {
            personController.EndAction();
            if (extractionStart != null)
            {
                StopCoroutine(extractionStart);
                extractionStart = StartCoroutine(AnimStart());
            }
        }
        
        private IEnumerator AnimStart()
        {
            yield return new WaitForSeconds(extractionStartTime);
            personController.LookAt(self.position);
            personController.StartAction(AnimationKey.Extraction);
        }
    }
}