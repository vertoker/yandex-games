using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Camera
{
    [CreateAssetMenu(menuName = "Data/" + nameof(CameraPreset), fileName = nameof(CameraPreset))]
    public class CameraPreset : ScriptableObject
    {
        public DimensionType dimension;
        public VectorFollow pos;
        public VectorFollow rot;
    }

    public enum DimensionType
    {
        ThreeDimension = 0,
        TwoDimension = 1
    }
}