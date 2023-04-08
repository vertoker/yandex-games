namespace Maze.Utility
{
    public static class Converter
    {
        public static bool[] ByteToBoolArray(byte value)
        {
            var values = new bool[8];

            values[0] = (value & 1) != 0;
            values[1] = (value & 2) != 0;
            values[2] = (value & 4) != 0;
            values[3] = (value & 8) != 0;
            values[4] = (value & 16) != 0;
            values[5] = (value & 32) != 0;
            values[6] = (value & 64) != 0;
            values[7] = (value & 128) != 0;

            return values;
        }
        public static byte BoolArrayToByte(bool[] values)
        {
            byte value = 0;

            for (int i = 0; i < 8; i++)
            {
                value <<= 1;
                if (values[i])
                    value |= 1;
            }
            
            return value;
        }
    }
}