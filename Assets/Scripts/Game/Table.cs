using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public class Table : MonoBehaviour
    {
        //[SerializeField] private Color[] colors;
        //private int current = 0;

        [SerializeField] private float pixelPerUnit = 1.8f;
        [SerializeField] private int xCornerSizePixel = 5;

        [SerializeField] private Vector2 pixelBorders;
        [SerializeField] private Vector2 borders;
        [SerializeField] private float aspect;

        private Transform edgeLeft, edgeRight;
        private Transform cornerLeft, cornerLeftCenter;
        private Transform cornerRight, cornerRightCenter;

        private void Start()
        {
            edgeLeft = transform.GetChild(0);
            edgeRight = transform.GetChild(1);
            cornerLeft = transform.GetChild(2);
            cornerLeftCenter = transform.GetChild(3);
            cornerRightCenter = transform.GetChild(4);
            cornerRight = transform.GetChild(5);

            aspect = (float)Screen.width / Screen.height;
            borders = new Vector2(5f * aspect, 5f);
            pixelBorders = borders * pixelPerUnit;

            edgeLeft.localScale = new Vector3(borders.x, 1f);
            edgeRight.localScale = new Vector3(borders.x, 1f);
            edgeLeft.localPosition = new Vector3(-pixelBorders.x / 2f / pixelPerUnit, 0f);
            edgeRight.localPosition = new Vector3(pixelBorders.x / 2f / pixelPerUnit, 0f);
            cornerLeft.localPosition = new Vector3((-pixelBorders.x + xCornerSizePixel / 2f) / pixelPerUnit, 0f);
            cornerRight.localPosition = new Vector3((pixelBorders.x - xCornerSizePixel / 2f) / pixelPerUnit, 0f);
            cornerLeftCenter.localPosition = new Vector3(xCornerSizePixel / 2f / -pixelPerUnit, 0f);
            cornerRightCenter.localPosition = new Vector3(xCornerSizePixel / 2f / pixelPerUnit, 0f);
        }

        private void Update()
        {
            
        }
    }
}