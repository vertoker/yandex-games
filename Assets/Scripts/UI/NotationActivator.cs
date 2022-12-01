using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class NotationActivator : MonoBehaviour
    {
        [SerializeField] private NextItem nextItem;

        [SerializeField] Camera m_cam;
        [SerializeField] GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        [SerializeField] EventSystem m_EventSystem;

        private void OnMouseOver()
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = (Vector2)m_cam.ScreenToWorldPoint(Input.mousePosition);

            var results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);

            if (results.Count > 0)
                Debug.Log("Hit " + results.Count);
        }
    }
}