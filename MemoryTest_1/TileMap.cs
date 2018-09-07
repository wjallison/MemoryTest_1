using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MemoryTest_1
{
    public class TileMap
    {
        public Matrix<double> map;

        public Matrix<double> visibleMap;

        public Matrix<double> buildingMap;

        public int[] startPos = { 0, 0 };


        public TileMap()
        {
            map = Matrix.Build.Dense(12,12);
            for(int i = 0; i < 12; i++)
            {
                for(int j = 0; j < 12; j++)
                {
                    if(i == 0 || i == 11) { map[i, j] = 8; }
                    if(j == 0 || j == 11) { map[i, j] = 8; }
                }
            }




            buildingMap = Matrix.Build.DenseOfMatrix(map);
            visibleMap = Matrix.Build.DenseOfMatrix(map);
        }
        public void reInit()
        {
            buildingMap = Matrix.Build.DenseOfMatrix(map);
            visibleMap = Matrix.Build.DenseOfMatrix(map);
        }

        public bool isWall(int x, int y)
        {
            if(map[x,y] == 8) { return true; }
            return false;
        }

        public void updateVisibleMap(int x, int y)
        {
            visibleMap = Matrix.Build.Dense(12, 12,9);
            for (int i = 0; i < 360; i++)
            {
                for(int j = 0; j < 80; j++)
                {
                    int xP = x + Convert.ToInt16(j * .25 * Math.Cos(Convert.ToDouble(i) * Math.PI / 180));
                    int yP = y + Convert.ToInt16(j * .25 * Math.Sin(Convert.ToDouble(i) * Math.PI / 180));

                    if (map[xP,yP] != 8)
                    {
                        visibleMap[xP, yP] = map[xP, yP];
                    }
                    else
                    {
                        visibleMap[xP, yP] = map[xP, yP];
                        break;
                    }
                }
            }

            visibleMap[x, y] = 1;
        }
    }
}
