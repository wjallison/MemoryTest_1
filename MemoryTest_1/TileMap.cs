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




            buildingMap = map;
            visibleMap = map;
        }

        public void updateVisibleMap(int x, int y)
        {
            visibleMap = Matrix.Build.Dense(12, 12);
            for (int i = 0; i < 360; i++)
            {
                for(int j = 0; j < 20; j++)
                {
                    int xP = x + j * Convert.ToInt16(Math.Cos(Convert.ToDouble(i) * Math.PI / 180));
                    int yP = y + j * Convert.ToInt16(Math.Sin(Convert.ToDouble(i) * Math.PI / 180));

                    if(map[xP,yP] != 8)
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
