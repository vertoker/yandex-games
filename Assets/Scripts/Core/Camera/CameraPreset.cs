using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Camera
{
    [CreateAssetMenu(menuName = "Data/" + nameof(CameraPreset), fileName = nameof(CameraPreset))]
    public class CameraPreset : ScriptableObject
    {
        public DimensionType dimension;
        public VectorFollow Pos;
        public VectorFollow Rot;
    }

    public enum DimensionType
    {
        ThreeD = 0,
        TwoD = 1
    }
}