using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utils
{
    public static class Math
    {
        public static Vector2 GetRandomPointInCircle(float radius)
        {
            float theta = Random.value * radius * Mathf.PI * 2f;
            float r = (Random.value + Random.value) * radius;
            if (r >= radius)
                r = radius * 2f - r;
            return new Vector2(r * Mathf.Cos(theta), r * Mathf.Sin(theta));
        }
    }
}