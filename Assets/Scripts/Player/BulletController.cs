using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.UI;
using UnityEngine.EventSystems;

namespace Scripts.Player
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private InputReceiver inputReceiver;

        private void OnEnable()
        {
            inputReceiver.OnDownUpdate += Down;
            inputReceiver.OnDragUpdate += Drag;
            inputReceiver.OnUpUpdate += Up;
        }
        private void OnDisable()
        {
            inputReceiver.OnDownUpdate -= Down;
            inputReceiver.OnDragUpdate -= Drag;
            inputReceiver.OnUpUpdate -= Up;
        }

        private void Down(PointerEventData data)
        {

        }
        private void Drag(PointerEventData data)
        {

        }
        private void Up(PointerEventData data)
        {

        }
    }
}