using System;
using System.Collections;
using UnityEngine;

namespace Game.Drawer
{
    public class BackgroundMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float size = 0.4f;
        [SerializeField] private Transform self;
        
        private Coroutine _updater;
        private float _counter;

        private void OnValidate()
        {
            self = transform;
        }
        private void OnEnable()
        {
            _updater = StartCoroutine(UpdateTimer());
        }
        private void OnDisable()
        {
            StopCoroutine(_updater);
        }
        
        private IEnumerator UpdateTimer()
        {
            while (true)
            {
                self.transform.position = new Vector3(_counter, 0, 0);
                
                yield return null;
                _counter += Time.deltaTime * speed;
                if (_counter > size)
                    _counter -= Mathf.Floor(_counter / size) * size;
            }
        }
    }
}