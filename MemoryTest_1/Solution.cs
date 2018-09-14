using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.IO;

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
        public List<ExpandedTraining> expandedResult = new List<ExpandedTraining>();

        //public double[] 
        //public Matrix<double> 


        public Solution()
        {
            //actions.Add(null);
        }

       

        public void ProcessExpanded()
        {
            for (int i = 0; i < result.Count; i++)
            {
                List<double> pos = new List<double>();
                List<double> walls = new List<double>();
                List<double> endPos = new List<double>();
                List<double> combined = new List<double>();
                for (int j = 0; j < result[0].state.Count; j++)
                {

                    switch (result[i].state[j])
                    {
                        case 0:
                            pos.Add(0);
                            walls.Add(0);
                            endPos.Add(0);
                            break;
                        case 9:
                            pos.Add(.5);
                            walls.Add(.5);
                            endPos.Add(.5);
                            break;
                        case 1:
                            pos.Add(0);
                            walls.Add(0);
                            endPos.Add(0);
                            break;
                        case 2:
                            pos.Add(1);
                            walls.Add(0);
                            endPos.Add(0);
                            break;
                        case 6:
                            pos.Add(0);
                            walls.Add(0);
                            endPos.Add(1);
                            break;
                        case 8:
                            pos.Add(0);
                            walls.Add(1);
                            endPos.Add(0);
                            break;
                    }
                }
                for(int j = 0; j < 3; j++)
                {
                    for(int k = 0; k < pos.Count; k++)
                    {
                        switch (j)
                        {
                            case 0:
                                combined.Add(pos[k]);
                                break;
                            case 1:
                                combined.Add(walls[k]);
                                break;
                            case 2:
                                combined.Add(endPos[k]);
                                break;
                        }
                    }
                }
                expandedResult.Add(new ExpandedTraining(pos, walls, endPos, combined, result[i].output, result[i].score));
            }
        }

        public Solution(FileInfo f)
        {
            using(var reader = new StreamReader(f.FullName))
            {
                name = reader.ReadLine();
                score = Convert.ToDouble(reader.ReadLine());
                while (!reader.EndOfStream)
                {
                    //tempList.Add(Convert.ToDouble(reader.ReadLine()));
                    double action = Convert.ToDouble(reader.ReadLine());
                    List<double> tempList = new List<double>();
                    for (int i = 0; i < 144; i++)
                    {
                        tempList.Add(Convert.ToDouble(reader.ReadLine()));
                    }
                    result.Add(new SingleTraining(tempList, action, score));
                }
            }
        }

        public void add(ConsoleKeyInfo c, Matrix<double> view)
        {
            actions.Add(c);
            actionsDouble.Add(convertActions(c));
            viewStates.Add(Matrix.Build.DenseOfMatrix(view));
        }
        public void add(double c, Matrix<double> view)
        {
            actionsDouble.Add(c);
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

    public struct ExpandedTraining
    {
        public List<double> statePosition;
        public List<double> stateWalls;
        public List<double> stateEndPos;
        public List<double> combinedState;
        public double output;
        public double score;

        public ExpandedTraining(List<double> staP, List<double> staW, List<double> staEnd, List<double> comb, double ou, double sco)
        {
            statePosition = staP;
            stateWalls = staW;
            stateEndPos = staEnd;
            combinedState = comb;
            output = ou;
            score = sco;
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
