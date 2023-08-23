using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class ImageSerialization
    {
        public string Key;
        public byte[] Data;

        public void WriteData(Texture2D source, Texture2D result)
        {
            var count = source.width * source.height / 8;
            int byteCounter = 0, counter = 0;
            Data = new byte[count];

            var pixelsSource = source.GetPixels32();
            var pixelsResult = result.GetPixels32();
            var length = pixelsSource.Length;
            
            for (var i = 0; i < length; i++)
            {
                var correct = EqualsNoAlpha(pixelsSource[i], pixelsResult[i]);
                if (correct) Data[counter] |= (byte)(1 << byteCounter);
                
                byteCounter++;
                if (byteCounter < 8)
                    continue;
                byteCounter = 0;
                counter++;
            }
        }
        public void ReadData(Texture2D source, Texture2D result)
        {
            int byteCounter = 0, counter = 0;
            
            var pixelsSource = source.GetPixels();
            var pixelsResult = result.GetPixels();
            var length = pixelsSource.Length;
            
            for (var i = 0; i < length; i++)
            {
                if ((Data[counter] & (1 << i)) == 0)
                    pixelsResult[i] = pixelsSource[i].ToGrayNonEqual();
                else
                    pixelsResult[i] = pixelsSource[i];
                
                byteCounter++;
                if (byteCounter < 8)
                    continue;
                byteCounter = 0;
                counter++;
            }
            
            result.SetPixels(pixelsResult);
            result.Apply();
        }

        private static bool EqualsNoAlpha(Color32 c1, Color32 c2)
        {
            return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
        }
    }
}