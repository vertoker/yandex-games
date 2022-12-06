using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Tools
{
    public static class Math
    {
        private const float quadCoeff = 0f;//-0.78539816339f;
        public static Vector2 RotateVector(Vector2 value, float offsetAngle)
        {
            float angle = Mathf.Atan2(value.y, value.x) + offsetAngle * Mathf.Deg2Rad + quadCoeff;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * value.magnitude;
        }
    }
}