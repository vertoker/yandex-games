using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Utility;

namespace Game
{
    public class ExplosionComputator : MonoBehaviour
    {
        public static readonly Vector3 EPSILONSIZE = new Vector3(1, 1, 1);
        
        private static ExplosionComputator _self;
        private void Awake()
        {
            _self = this;
        }
        
        public static void ComputateExplosion(Vector3 epicenter, float radius, float power, List<DestructableObject> objects)
        {
            foreach (var obj in objects)
            {
                var direction = obj.Position - epicenter;
                var distance = direction.magnitude;
                var actualPower = radius / distance * power;
                
                if (obj.IsDestructable)
                {
                    obj.gameObject.SetActive(false);
                    obj.Transform.localScale = EPSILONSIZE;
                    PoolObjects.Enqueue(obj);
                    
                    var quaternion = Quaternion.Euler(obj.Rotation);
                    var matrix = Rotate(quaternion);
                    var max = new Vector3(obj.Scale.x - 1, obj.Scale.y - 1, obj.Scale.z - 1) / 2f;

                    for (var x = -max.x; x <= max.x; x++)
                    {
                        for (var y = -max.y; y <= max.y; y++)
                        {
                            for (var z = -max.z; z <= max.z; z++)
                            {
                                var newObj = PoolObjects.Dequeue();
                                Vector3 offset = new Vector3
                                (
                                    matrix[0] * x + matrix[1] * y + matrix[2] * z + matrix[3],
                                    matrix[4] * x + matrix[5] * y + matrix[6] * z + matrix[7],
                                    matrix[8] * x + matrix[9] * y + matrix[10] * z + matrix[11]
                                );
                                newObj.Transform.SetPositionAndRotation(obj.Position + offset, quaternion);
                                newObj.gameObject.SetActive(true);
                                newObj.Rigidbody.AddForce((direction + offset).normalized * actualPower, ForceMode.Impulse);
                                newObj.Rigidbody.angularVelocity = offset;
                            }
                        }
                    }
                    
                    ScoreCounter.AddPoints(obj.Scale.x * obj.Scale.y * obj.Scale.z * 10);
                }
                else
                {
                    obj.Rigidbody.AddForce(direction.normalized * actualPower, ForceMode.Impulse);
                }
            }
        }
        
        // Take from Unity CSReference
        private static float[] Rotate(Quaternion q)
        {
            // Precalculate coordinate products
            float x = q.x * 2.0F;
            float y = q.y * 2.0F;
            float z = q.z * 2.0F;
            float xx = q.x * x;
            float yy = q.y * y;
            float zz = q.z * z;
            float xy = q.x * y;
            float xz = q.x * z;
            float yz = q.y * z;
            float wx = q.w * x;
            float wy = q.w * y;
            float wz = q.w * z;

            // Calculate 3x3 matrix from orthonormal basis
            float[] m = new float[12];
            m[0] = 1.0f - (yy + zz); m[1] = xy + wz; m[2] = xz - wy; m[3] = 0.0F;
            m[4] = xy - wz; m[5] = 1.0f - (xx + zz); m[6] = yz + wx; m[7] = 0.0F;
            m[8] = xz + wy; m[9] = yz - wx; m[10] = 1.0f - (xx + yy); m[11] = 0.0F;
            return m;
        }
    }
}
