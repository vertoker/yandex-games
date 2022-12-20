using UnityEngine;

namespace Utility
{
    public static class CustomMath
    {
        public static Vector3 RotateVector(Vector3 value, Vector3 eulerAngles)
        {
            eulerAngles *= Mathf.Deg2Rad;
            
            // X axis
            var y1 = Mathf.Cos(eulerAngles.x) * value.y - Mathf.Sin(eulerAngles.x) * value.z;
            var z1 = Mathf.Sin(eulerAngles.x) * value.y + Mathf.Cos(eulerAngles.x) * value.z;
            // Y axis
            var x1 = Mathf.Cos(eulerAngles.z) * value.x - Mathf.Sin(eulerAngles.z) * y1;
            var y2 = Mathf.Sin(eulerAngles.z) * value.x + Mathf.Cos(eulerAngles.z) * y1;
            // Z axis
            var x2 = Mathf.Cos(eulerAngles.y) * x1 + Mathf.Sin(eulerAngles.y) * z1;
            var z2 = Mathf.Cos(eulerAngles.y) * z1 - Mathf.Sin(eulerAngles.y) * x1;
            
            return new Vector3(x2, y2, z2);
        }
        
        public static Vector2 RotateVector(Vector2 value, float offsetAngle)
        {
            var radAngle = offsetAngle * Mathf.Deg2Rad;
            var x = Mathf.Cos(radAngle) * value.x - Mathf.Sin(radAngle) * value.y;
            var y = Mathf.Sin(radAngle) * value.x + Mathf.Cos(radAngle) * value.y;
            return new Vector2(x, y);
        }

        public static Vector3 AngleLerp(Vector3 a, Vector3 b, float t)
        {
            var x = Mathf.LerpAngle(a.x, b.x, t);
            var y = Mathf.LerpAngle(a.y, b.y, t);
            var z = Mathf.LerpAngle(a.z, b.z, t);
            return new Vector3(x, y, z);
        }

        public static Vector3 Repeat(Vector3 value, float min, float max)
        {
            var x = Repeat(value.x, min, max);
            var y = Repeat(value.y, min, max);
            var z = Repeat(value.z, min, max);
            return new Vector3(x, y, z);
        }
        public static float Repeat(float value, float min, float max)
        {
            var delta = max - min;
            if (value < min)
                return value + delta;
            if (value > max)
                return value - delta;
            return value;
        }
    }
}
