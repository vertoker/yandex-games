using UnityEngine;

namespace Data
{
    public static class Extensions
    {
        public static Vector3 NormalScale => new Vector3(1, 1, 1);

        public static Color LightGray => new Color(0.8f, 0.8f, 0.8f, 1f);
        
        public static Color ToHighlighted(this Color color)
        {
            var power = color.r + color.g + color.b;
            return power > 1.5f ? Color.black : Color.white;
        }
        public static Color ToGray(this Color color)
        {
            var gray = (color.r + color.g + color.b) / 3f;
            return new Color(gray, gray, gray, color.a);
        }
        public static Color ToGrayNonEqual(this Color color, float saturation = 0.8f, float grayThreshold = 0.2f, float grayOffset = 0.05f)
        {
            var gray = (color.r + color.g + color.b) / 3f * saturation;
            if (gray < grayThreshold) gray -= grayOffset;
            if (gray < 0) gray = grayThreshold + gray;
            return new Color(gray, gray, gray, color.a);
        }
        public static Color ToNonContrast(this Color color, float progress = 0.5f)
        {
            var r = Mathf.Lerp(color.r, 1, progress);
            var g = Mathf.Lerp(color.r, 1, progress);
            var b = Mathf.Lerp(color.r, 1, progress);
            return new Color(r, g, b, color.a);
        }
    }
}