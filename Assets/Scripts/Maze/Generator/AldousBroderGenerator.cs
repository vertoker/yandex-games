using System.Threading.Tasks;
using Maze.Data;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Maze.Generator
{
    public class AldousBroderGenerator
    {
        public Action<float, float> OnPosUpdate;
        public Action<MazeData> OnDataUpdate;

        public AldousBroderGenerator()
        {
            
        }

        public MazeData Generate(int x, int y)
        {
            var countCells = x * y;
            
            var visited = new bool[countCells];
            var data = new MazeData(x, y);

            var pointX = Random.Range(0, x);
            var pointY = Random.Range(0, y);
            var currentIndex = pointY * x + pointX;
            
            visited[currentIndex] = true;
            OnPosUpdate?.Invoke(pointX, pointY);
            
            var visitedCells = 1;
            while (visitedCells < countCells)
            {
                var lastIndex = currentIndex;
                var direction = (Direction)Random.Range(0, 4);
                
                switch (direction)
                {
                    case Direction.Up:
                        if (pointY < y - 1)
                        {
                            pointY++;
                            currentIndex += x;
                            
                            if (!visited[currentIndex])
                            {
                                visited[currentIndex] = true;
                                data.Cells[lastIndex].Up = false;
                                visitedCells++;
                            }
                        }
                        break;
                    case Direction.Down:
                        if (pointY > 0)
                        {
                            pointY--;
                            currentIndex -= x;
                            
                            if (!visited[currentIndex])
                            {
                                visited[currentIndex] = true;
                                data.Cells[currentIndex].Up = false;
                                visitedCells++;
                            }
                        }
                        break;
                    case Direction.Left:
                        if (pointX < x - 1)
                        {
                            pointX++;
                            currentIndex += 1;
                            
                            if (!visited[currentIndex])
                            {
                                visited[currentIndex] = true;
                                data.Cells[currentIndex].Right = false;
                                visitedCells++;
                            }
                        }
                        break;
                    case Direction.Right:
                        if (pointX > 0)
                        {
                            pointX--;
                            currentIndex -= 1;
                            
                            if (!visited[currentIndex])
                            {
                                visited[currentIndex] = true;
                                data.Cells[lastIndex].Right = false;
                                visitedCells++;
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return data;
        }
    }
}