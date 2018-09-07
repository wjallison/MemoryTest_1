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
    class Program
    {
        static void Main(string[] args)
        {
            //Init the map
            TileMap m = new TileMap();
            List<Solution> trainingSolns = new List<Solution>();
            Solution solve;

            Console.WriteLine("Do you want to train a classic network? (y/n)");
            if(Console.ReadKey().KeyChar == 'y')
            {
                List<int> networkInit = new List<int> { 12 * 12, 30, 4 };
                ClassicNetwork net = new ClassicNetwork(networkInit);

                trainingSolns = LoadSolutions();
                


            }
            

            //Ask if the user wants to build a map
            Console.WriteLine("Do you want to build a map? (y/n)");
            if(Console.ReadKey().KeyChar == 'y')
            {
                m = BuildMap(m);
                Console.WriteLine("Please enter a title:");
                m.name = Console.ReadLine();
                SaveMap(m);
            }
            else
            {
                m = LoadMap();
            }
            //Console.WriteLine")

            //One way or another, we now have a map.  Now, we will choose who will solve the map.
            Console.WriteLine("Will the solution be provided by the user? (y/n)");
            if(Console.ReadKey().KeyChar == 'y')
            {
                solve = UserSolution(m);
                SaveSolution(solve);
            }
            else
            {
                Console.WriteLine("Classic network? (y/n)");
                if(Console.ReadKey().KeyChar == 'y')
                {
                    List<int> networkInit = new List<int> { 12 * 12, 30, 4 };

                }


            }
            

            Console.ReadKey();
        }

        public static Solution programSolution(TileMap m, ClassicNetwork net)
        {
            Solution solve = new Solution();




            return solve;
        }

        public static Solution UserSolution(TileMap m)
        {
            Solution solve = new Solution();
            bool notDone = true;
            int xPlayer = m.startPos[0];
            int yPlayer = m.startPos[1];
            while (notDone)
            {
                Console.Write(m.visibleMap.ToString());
                ConsoleKeyInfo c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        solve.add(c, m.visibleMap);
                        xPlayer--;
                        if (m.isWall(xPlayer, yPlayer)) { xPlayer++; }
                        m.updateVisibleMap(xPlayer, yPlayer);
                        break;
                    case ConsoleKey.DownArrow:
                        solve.add(c, m.visibleMap);
                        xPlayer++;
                        if (m.isWall(xPlayer, yPlayer)) { xPlayer--; }
                        m.updateVisibleMap(xPlayer, yPlayer);
                        break;
                    case ConsoleKey.RightArrow:
                        solve.add(c, m.visibleMap);
                        yPlayer++;
                        if (m.isWall(xPlayer, yPlayer)) { yPlayer--; }
                        m.updateVisibleMap(xPlayer, yPlayer);
                        break;
                    case ConsoleKey.LeftArrow:
                        solve.add(c, m.visibleMap);
                        yPlayer--;
                        if (m.isWall(xPlayer, yPlayer)) { yPlayer++; }
                        m.updateVisibleMap(xPlayer, yPlayer);
                        break;
                }
                if(m.map[xPlayer,yPlayer] == 6) { notDone = false; }
            }
            Console.Write(m.map.ToString());
            solve.takenSteps = solve.actions.Count;
            Console.WriteLine("Completed in " + solve.takenSteps + " steps.");
            Console.WriteLine("Please enter the minimum number of steps:");
            solve.minSteps = Convert.ToInt16(Console.ReadLine());
            solve.CalculateScore();
            Console.WriteLine("Score (out of 1000): " + solve.score.ToString());

            solve.GetName(m);

            return solve;
        }

        public static TileMap BuildMap(TileMap m)
        {
            //Allow the user to build the map
            int xBuild = 0;
            int yBuild = 0;
            bool t = true;
            Console.Write(m.buildingMap.ToString());
            while (t)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        yBuild--;
                        m.buildingMap = Matrix.Build.DenseOfMatrix(m.map);
                        //m.buildingMap = m.map;
                        m.buildingMap[yBuild, xBuild] = 5;
                        break;
                    case ConsoleKey.DownArrow:
                        yBuild++;
                        m.buildingMap = Matrix.Build.DenseOfMatrix(m.map);
                        m.buildingMap[yBuild, xBuild] = 5;
                        break;
                    case ConsoleKey.RightArrow:
                        xBuild++;
                        m.buildingMap = Matrix.Build.DenseOfMatrix(m.map);
                        m.buildingMap[yBuild, xBuild] = 5;
                        break;
                    case ConsoleKey.LeftArrow:
                        xBuild--;
                        m.buildingMap = Matrix.Build.DenseOfMatrix(m.map);
                        m.buildingMap[yBuild, xBuild] = 5;
                        break;
                    case ConsoleKey.NumPad0:
                        m.map[yBuild, xBuild] = 1;
                        m.startPos[0] = yBuild;
                        m.startPos[1] = xBuild;
                        break;
                    case ConsoleKey.NumPad5:
                        m.map[yBuild, xBuild] = 6;
                        break;
                    case ConsoleKey.NumPad8:
                        m.map[yBuild, xBuild] = 8;
                        break;
                    case ConsoleKey.Escape:
                        t = false;
                        break;
                }
                Console.Write(m.buildingMap.ToString());
            }
            Console.Write(m.map.ToString());
            m.reInit();
            return m;
        }

        public static void SaveMap(TileMap m)
        {
            //Console.WriteLine("Please enter a title:");
            //string name = Console.ReadLine();
            //m.name = name;
            var csv = new StringBuilder();
            string line;
            csv.AppendLine(m.name);
            for(int i = 0; i < 12; i++)
            {
                line = m.map[i, 0].ToString();
                for (int j = 1; j < 12; j++)
                {
                    line = line + "," + m.map[i, j].ToString();
                }
                csv.AppendLine(line);
            }

            File.WriteAllText(Directory.GetCurrentDirectory() + @"/" + m.name + ".csv", csv.ToString());
        }

        public static TileMap LoadMap()
        {
            TileMap m = new TileMap();

            DirectoryInfo dInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = dInfo.GetFiles("*.csv");

            Console.WriteLine("Please choose the map you wish to load.");
            for(int i = 0; i < files.Length; i++) 
            {
                Console.WriteLine(i.ToString() + ":  " + files[i].Name);
            }
            int ind = Convert.ToInt16(Console.ReadLine());
            if (ind < files.Length)
            {
                using(var reader = new StreamReader(files[ind].FullName))
                {
                    int i = 0;
                    m.name = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        for(int j = 0; j < 12; j++)
                        {
                            double k = Convert.ToDouble(values[j]);
                            m.map[i, j] = k;
                            if(k == 1)
                            {
                                m.startPos[0] = i;
                                m.startPos[1] = j;
                            }
                        }
                        i++;
                    }
                }
            }
            m.reInit();
            Console.Write(m.map.ToString());
            return m;
        }
        public static List<Solution> LoadSolutions()
        {
            List<Solution> ret = new List<Solution>();

            DirectoryInfo dInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = dInfo.GetFiles("soln_*.csv");
            List<bool> selectedFiles = new List<bool>();
            for(int i = 0; i < files.Length; i++) { selectedFiles.Add(false); }

            Console.WriteLine("Please select solutions to use for training.");
            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(" " + i.ToString() + ". " + files[i].Name);
            }
            bool notDone = true;
            while (notDone)
            {
                string s = Console.ReadLine();
                if(s == "") { notDone = false; }
                else
                {
                    if (!selectedFiles[Convert.ToInt16(s)])
                    {
                        ret.Add(new Solution(files[Convert.ToInt16(s)]));
                    }
                }
                for (int i = 0; i < files.Length; i++)
                {
                    if (selectedFiles[i])
                    {
                        Console.WriteLine("[" + i.ToString() + ".]" + files[i].Name);
                    }
                    else
                    {
                    Console.WriteLine(" " + i.ToString() + ". " + files[i].Name);
                    }
                }

            }


            return ret;
        }
        
        public static void SaveSolution(Solution s)
        {
            //Console.WriteLine("Please enter a title:");
            //Console.Write(m.name);
            //Console.ReadLine()
            var csv = new StringBuilder();
            string line;
            csv.AppendLine(s.name);
            csv.AppendLine(s.score.ToString());
            for(int i = 0; i < s.result.Count; i++)
            {
                csv.AppendLine(s.result[i].output.ToString());
                for(int j = 0; j < s.result[0].state.Count; j++)
                {
                    csv.AppendLine(s.result[i].state[j].ToString());
                }
            }

            File.WriteAllText(Directory.GetCurrentDirectory() + @"/" + "soln_" + s.name + ".csv", csv.ToString());
        }
    }
}
