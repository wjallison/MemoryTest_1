using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MemoryTest_1
{
    public class Solution
    {
        public double score;
        public List<ConsoleKeyInfo> actions = new List<ConsoleKeyInfo>();
        public List<Matrix<double>> viewStates = new List<Matrix<double>>();
        public Solution()
        {
            //actions.Add(null);
        }

        public void add(ConsoleKeyInfo c, Matrix<double> view)
        {
            actions.Add(c);
            viewStates.Add(view);
        }

        //public double[] inputFormat()
        //{

        //}
    }
}
