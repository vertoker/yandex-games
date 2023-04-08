using System;
using Maze.Utility;
using UnityEngine;

namespace Maze.Data
{
    public class MazeData : IPacker
    {
        public Vector2Int Size { get; set; }
        public CellData[] Cells { get; set; }

        public MazeData(int x, int y)
        {
            Size = new Vector2Int(x, y);
            var count = x * y;
            Cells = new CellData[count];
            
            for (int i = 0; i < count; i++)
            {
                Cells[i].Right = true;
                Cells[i].Up = true;
            }
        }
        
        public MazeData(byte[] data)
        {
            UnPack(data);
        }
        
        public void UnPack(byte[] data)
        {
            var dataLength = data.Length;

            int x = BitConverter.ToInt32(data, 0);
            int y = BitConverter.ToInt32(data, 4);
            var cellsCount = x * y;

            if (cellsCount != 4 * (dataLength - 8))
                throw new Exception($"Data length ({dataLength} bytes) doesn't fit to cells count ({cellsCount}). " +
                                    "For right conversion sizes must properly standing in next equation: " +
                                    "Cells count = 4 * (data length - 8)");

            Size = new Vector2Int(x, y);
            Cells = new CellData[cellsCount];

            var bytesCounter = 8;
            var cellsCounter = 0;

            while (cellsCounter < cellsCount)
                ReadByte(ref data, ref bytesCounter, ref cellsCounter);
        }
        
        public byte[] Pack()
        {
            var cellsCount = Size.x * Size.y;
            var dataLength = cellsCount / 4 + 8;
            
            var data = new byte[dataLength];

            var spanX = data.AsSpan(0, 4);
            var spanY = data.AsSpan(4, 4);
            BitConverter.TryWriteBytes(spanX, Size.x);
            BitConverter.TryWriteBytes(spanY, Size.y);
            
            var bytesCounter = 8;
            var cellsCounter = 0;
            var nextByte = new bool[8];
            
            while (cellsCounter < cellsCount)
                WriteByte(ref data, ref bytesCounter, ref cellsCounter, ref nextByte);

            return data;
        }

        public void ReadByte(ref byte[] data, ref int bytesCounter, ref int cellsCounter)
        {
            var byteCells = Converter.ByteToBoolArray(data[bytesCounter]);
            bytesCounter++;

            for (int i = 0; i < 8; i += 2)
            {
                Cells[cellsCounter].Right = byteCells[i];
                Cells[cellsCounter].Up = byteCells[i + 1];
                cellsCounter++;
            }
        }
        public void WriteByte(ref byte[] data, ref int bytesCounter, ref int cellsCounter, ref bool[] nextByte)
        {
            for (int i = 0; i < 8; i += 2)
            {
                nextByte[i] = Cells[cellsCounter].Right;
                nextByte[i + 1] = Cells[cellsCounter].Up;
                cellsCounter++;
            }
            
            data[bytesCounter] = Converter.BoolArrayToByte(nextByte);
            bytesCounter++;
        }
    }
}