using System;
using System.Linq;
using UnityEngine;

namespace Maze.Data
{
    public class MazesDataHandler : IPacker
    {
        public MazeData[] Mazes { get; set; }
        
        public MazesDataHandler()
        {
            
        }
        public MazesDataHandler(byte[] data)
        {
            UnPack(data);
        }
        
        public void UnPack(byte[] data)
        {
            var count = BitConverter.ToInt32(data, 0);
            
            Mazes = new MazeData[count];

            var bytesCounter = 4;
            var mazesCounter = 0;

            while (mazesCounter < count)
            {
                var x = BitConverter.ToInt32(data, bytesCounter);
                var y = BitConverter.ToInt32(data, bytesCounter + 4);
                bytesCounter += 8;
                
                Mazes[mazesCounter] = new MazeData(x, y);
                
                var cellsCount = x * y;
                var cellsCounter = 0;
                
                while (cellsCounter < cellsCount)
                    Mazes[mazesCounter].ReadByte(ref data, ref bytesCounter, ref cellsCounter);

                mazesCounter++;
            }

        }
        
        public byte[] Pack()
        {
            var mazesCount = Mazes.Length;
            var cellsCountAll = Mazes.Sum(m => m.Size.x * m.Size.y);
            var dataLength = cellsCountAll / 4 + 8 * mazesCount + 4;
            
            var data = new byte[dataLength];
            
            var spanCount = data.AsSpan(0, 4);
            BitConverter.TryWriteBytes(spanCount, mazesCount);
            
            var mazesCounter = 0;
            var bytesCounter = 8;
            var nextByte = new bool[8];
            
            while (mazesCounter < mazesCount)
            {
                var size = Mazes[mazesCounter].Size;
                var spanX = data.AsSpan(bytesCounter, 4);
                var spanY = data.AsSpan(bytesCounter + 4, 4);
                BitConverter.TryWriteBytes(spanX, size.x);
                BitConverter.TryWriteBytes(spanY, size.y);
                bytesCounter += 8;
                
                var cellsCount = size.x * size.y;
                var cellsCounter = 0;
                
                while (cellsCounter < cellsCount)
                    Mazes[mazesCounter].WriteByte(ref data, ref bytesCounter, ref cellsCounter, ref nextByte);

                mazesCounter++;
            }

            return data;
        }
    }
}