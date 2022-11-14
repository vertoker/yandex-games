using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utils
{
    public class Borders : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _boldness = 2f;
        private Transform[] boxes;

        private void Awake()
        {
            boxes = new Transform[4];
            for (int i = 0; i < 4; i++)
                boxes[i] = transform.GetChild(i);

            Vector2 innerSize = new Vector2(_camera.orthographicSize * 2f * _camera.aspect, _camera.orthographicSize * 2f);
            boxes[0].localScale = new Vector3(innerSize.x + _boldness * 2f, _boldness, 1f);
            boxes[0].localPosition = new Vector3(0f, (innerSize.y + _boldness) / 2f, 0f);
            boxes[1].localScale = new Vector3(innerSize.x + _boldness * 2f, _boldness, 1f);
            boxes[1].localPosition = new Vector3(0f, -(innerSize.y + _boldness) / 2f, 0f);
            boxes[2].localScale = new Vector3(_boldness, innerSize.y + _boldness * 2f, 1f);
            boxes[2].localPosition = new Vector3((innerSize.x + _boldness) / 2f, 0f, 0f);
            boxes[3].localScale = new Vector3(_boldness, innerSize.y + _boldness * 2f, 1f);
            boxes[3].localPosition = new Vector3(-(innerSize.x + _boldness) / 2f, 0f, 0f);

            for (int i = 0; i < 4; i++)
                boxes[i].gameObject.SetActive(true);
        }
    }
}