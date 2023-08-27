using System.Collections;
using System.Linq;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class ImageHistorySerialization
    {
        public string key;
        public byte[] data;

        public void WriteData(Texture2D source, Texture2D result)
        {
            var count = Mathf.FloorToInt(source.width * source.height / 8f) + 1;
            var nextData = new byte[count];

            var pixelsSource = source.GetPixels32();
            var pixelsResult = result.GetPixels32();
            var length = pixelsSource.Length;
            var bits = new BitArray(length);
            
            for (var i = 0; i < length; i++)
            {
                var correct = EqualsNoAlpha(pixelsSource[i], pixelsResult[i]);
                bits[i] = correct;
            }
            
            bits.CopyTo(nextData, 0);
            data = nextData;
        }
        public void ReadData(Texture2D source, Texture2D result)
        {
            var pixelsSource = source.GetPixels();
            var pixelsResult = result.GetPixels();
            var length = pixelsSource.Length;

            if (data.Length == 0)
                data = Enumerable.Repeat<byte>(0, Mathf.FloorToInt(length / 8f) + 1).ToArray();
            var bits = new BitArray(data);

            for (var i = 0; i < length; i++)
                pixelsResult[i] = bits[i] ? pixelsSource[i] : pixelsSource[i].ToGrayNonEqual();
            
            result.SetPixels(pixelsResult);
            result.Apply();
        }

        private static bool EqualsNoAlpha(Color32 c1, Color32 c2)
        {
            return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
        }
    }
}