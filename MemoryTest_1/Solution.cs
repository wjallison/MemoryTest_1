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
        public string name;

        public int minSteps;
        public int takenSteps;
        public double score;
        public List<ConsoleKeyInfo> actions = new List<ConsoleKeyInfo>();
        public List<double> actionsDouble = new List<double>();
        public List<Matrix<double>> viewStates = new List<Matrix<double>>();

        public List<SingleTraining> result = new List<SingleTraining>(); 

        //public double[] 
        //public Matrix<double> 


        public Solution()
        {
            //actions.Add(null);
        }

        public void add(ConsoleKeyInfo c, Matrix<double> view)
        {
            actions.Add(c);
            actionsDouble.Add(convertActions(c));
            viewStates.Add(Matrix.Build.DenseOfMatrix(view));
        }

        public void CalculateScore()
        {
            if (takenSteps - minSteps <= 1000)
            {
                score = 1000.0 - (takenSteps - minSteps);
            }
            else { score = 0; }
        }

        public double convertActions(ConsoleKeyInfo c)
        {
            switch (c.Key)
            {
                case ConsoleKey.UpArrow:
                    return 1.0;
                case ConsoleKey.DownArrow:
                    return 2.0;
                case ConsoleKey.RightArrow:
                    return 3.0;
                case ConsoleKey.LeftArrow:
                    return 4.0;
            }
            return 0.0;
        }

        public void FinishUp()
        {
            for(int i = 0; i < actions.Count; i++)
            {
                //actionsDouble.Add(convertActions(actions[i]));

                List<double> buildList = new List<double>();
                for (int x = 0; x < viewStates[0].RowCount; x++)
                {
                    for (int y = 0; y < viewStates[0].ColumnCount; y++)
                    {
                        buildList.Add(viewStates[i][x, y]);
                    }
                }
                result.Add(new SingleTraining(buildList, actionsDouble[i], score));
            }
        }

        //public Matrix<double> 

        public double[] inputFormat()
        {
            double[] output;
            List<double> buildOutput = new List<double>();
            for (int i = 0; i < actionsDouble.Count; i++)
            {
                buildOutput.Add(actionsDouble[i]);
                for(int x = 0; x < viewStates[0].RowCount; x++)
                {
                    for(int y = 0; y < viewStates[0].ColumnCount; y++)
                    {
                        buildOutput.Add(viewStates[i][x, y]);
                    }
                }
            }

            output = buildOutput.ToArray();
            return output;
        }

        public void GetName(TileMap m)
        {
            Console.WriteLine("Please enter a name for the solution:");
            Console.Write(m.name + " + ");
            name = m.name + Console.ReadLine();
        }
    }

    public struct SingleTraining
    {
        //public Matrix<double> state;
        public List<double> state;
        public double output;
        public double score;

        public SingleTraining(List<double> sta, double ou, double sco)
        {
            state = sta;
            output = ou;
            score = sco;
        }
    }

    
}
