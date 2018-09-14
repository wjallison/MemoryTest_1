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
            altMain();
            
            ////Init the map
            //TileMap m = new TileMap();
            //List<Solution> trainingSolns = new List<Solution>();

            //ClassicNetwork net;
            //MemoryNetwork mNet;

            //Solution solve;

            //Console.WriteLine("Do you want to train a classic network? (y/n)");
            //if (Console.ReadKey().KeyChar == 'y')
            //{
            //    List<int> networkInit = new List<int> { 12 * 12, 30, 4 };
            //    net = new ClassicNetwork(networkInit);

            //    trainingSolns = LoadSolutions();

            //    for(int i = 0; i < trainingSolns.Count; i++)
            //    {
            //        net.ConvertFromContinuous(trainingSolns[i]);
            //    }

            //    net.SGD(net.dataIn, 30, 1);

            //    Console.WriteLine("Would you like to test the network? (y/n)");
            //    if(Console.ReadKey().KeyChar == 'y')
            //    {
            //        m = LoadMap();

            //        ClassicProgramSolution(m, net);
            //    }

            //    Console.WriteLine("Would you like to save the network? (y/n)");
            //    if(Console.ReadKey().KeyChar == 'y')
            //    {
            //        SaveClassicNetwork(net);
            //    }
            //}

            //Console.WriteLine("Do you want to train a memory network? (y/n)");
            //if(Console.ReadKey().KeyChar == 'y')
            //{
            //    List<int> networkInit = new List<int> { 12 * 12, 30, 4 };
            //    mNet = new MemoryNetwork(networkInit, 144);

            //    trainingSolns = LoadSolutions();

            //    for(int i = 0; i < trainingSolns.Count; i++)
            //    {
            //        for(int j = 0; j < trainingSolns[i].result.Count; j++)
            //        {
            //            mNet.Train(trainingSolns[i].result[j]);
            //        }
            //        mNet.ResetMemory();
            //    }

            //    Console.WriteLine("Would you like to test the network? (y/n)");
            //    if (Console.ReadKey().KeyChar == 'y')
            //    {
            //        m = LoadMap();

                    
            //    }

            //    Console.WriteLine("Would you like to save the network? (y/n)");
            //    if (Console.ReadKey().KeyChar == 'y')
            //    {
                    
            //    }
            //}

            //Console.WriteLine("Do you want to build a map? (y/n)");
            //if(Console.ReadKey().KeyChar == 'y')
            //{
            //    m = BuildMap(m);
            //    Console.WriteLine("Please enter a title:");
            //    m.name = Console.ReadLine();
            //    SaveMap(m);
            //}
            //else
            //{
            //    m = LoadMap();
            //}
            ////Console.WriteLine")

            ////One way or another, we now have a map.  Now, we will choose who will solve the map.
            //Console.WriteLine("Will the solution be provided by the user? (y/n)");
            //if(Console.ReadKey().KeyChar == 'y')
            //{
            //    solve = UserSolution(m);
            //    solve.FinishUp();
            //    SaveSolution(solve);
            //}
            //else
            //{
            //    Console.WriteLine("Do you want to load a classic network? (y/n)");
            //    if(Console.ReadKey().KeyChar == 'y')
            //    {
            //        List<int> networkInit = new List<int> { 12 * 12, 30, 4 };

            //    }
            //    else
            //    {
            //        Console.WriteLine("Do you want to load a memory network? (y/n)");
            //        if(Console.ReadKey().KeyChar == 'y')
            //        {


            //        }
            //    }

            //}
            

            //Console.ReadKey();
        }

        public static void altMain()
        {
            TileMap m = new TileMap();
            List<Solution> trainingSolns = new List<Solution>();

            List<int> networkInit = new List<int> { 12 * 12 * 3, 30, 4 };
            ClassicNetwork cnet = new ClassicNetwork(networkInit);
            MemoryNetwork mNet;

            Solution solve;
            //Console.cl
            bool mapLoaded = false;
            bool cNetLoaded = false;
            bool mNetLoaded = false;
            bool solnsLoaded = false;

            while (true)
            {
                Console.Clear();
                if (mapLoaded) { Console.WriteLine("Map loaded: " + m.name); }
                else { Console.WriteLine("Map loaded: FALSE"); }
                Console.Write("Classic network loaded: ");
                if (cNetLoaded) { Console.WriteLine("TRUE"); }
                else { Console.WriteLine("FALSE"); }
                Console.Write("Memory network loaded: ");
                if (mNetLoaded) { Console.WriteLine("TRUE"); }
                else { Console.WriteLine("FALSE"); }
                //Console.Write("Solutions ")
                Console.WriteLine();
                Console.WriteLine("Please select an option:");
                Console.WriteLine();
                Console.WriteLine("1a. Build a map");
                Console.WriteLine("1b. Edit a map");
                Console.WriteLine("2. Load a map");
                Console.WriteLine("3. Provide a solution for a map");
                Console.WriteLine("4. Train a classic network");
                Console.WriteLine("5. Train a memory network");
                Console.WriteLine("6. Load a network");
                Console.WriteLine("7. Test a network");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1a":
                        m = NewBuildMap();
                        SaveMap(m);
                        mapLoaded = true;
                        break;
                    case "1b":
                        m = NewBuildMap(m);
                        SaveMap(m);
                        mapLoaded = true;
                        break;
                    case "2":
                        m = LoadMap();
                        mapLoaded = true;
                        break;
                    case "3":
                        if (!mapLoaded) { Console.WriteLine("Map not loaded!"); Console.ReadKey(); break; }
                        solve = UserSolution(m);
                        solve.FinishUp();
                        SaveSolution(solve);
                        break;
                    case "4":
                        Console.WriteLine("Intense? (y/n)");
                        if(Console.ReadKey().KeyChar == 'y')
                        {
                            cnet = ClassicTrainNetwork(cnet, true);
                        }
                        else
                        {
                            if (cNetLoaded) { cnet = ClassicTrainNetwork(cnet); }
                            else { cnet = ClassicTrainNetwork(); }
                        }                        
                        cNetLoaded = true;
                        mNetLoaded = false;
                        break;
                    case "5":

                        break;
                    case "7":
                        if (!mapLoaded) { Console.WriteLine("Map not loaded!"); Console.ReadKey(); break; }
                        else if(!cNetLoaded && !mNetLoaded) { Console.WriteLine("Network not loaded!"); Console.ReadKey(); break; }

                        ClassicProgramSolution(m, cnet);
                        break;
                }
            }
        }

        public static ClassicNetwork ClassicTrainNetwork(bool intense = false)
        {

            List<int> networkInit = new List<int> { 12 * 12 * 3, 30, 4 };
            ClassicNetwork net = new ClassicNetwork(networkInit);

            List<Solution> trainingSolns = LoadSolutions();
            for(int i = 0; i < trainingSolns.Count; i++)
            {
                net.ConvertFromContinuous(trainingSolns[i]);
            }
            if (!intense)
            {
                net.SGD(net.dataIn, 30, 1);
            }
            else { net.IntenseSGD(net.dataIn, 1); }
            return net;
        }
        public static ClassicNetwork ClassicTrainNetwork(ClassicNetwork cNet, bool intense = false)
        {
            List<Solution> trainingSolns = LoadSolutions();
            for (int i = 0; i < trainingSolns.Count; i++)
            {
                cNet.ConvertFromContinuous(trainingSolns[i]);
            }
            if (!intense)
            {
                cNet.SGD(cNet.dataIn, 30, 1);
            }
            else { cNet.IntenseSGD(cNet.dataIn, 1); }

            return cNet;
        }

        public static TileMap NewBuildMap()
        {
            TileMap m = BuildMap();
            Console.WriteLine("Please enter a title:");
            m.name = Console.ReadLine();
            return m;
        }
        public static TileMap NewBuildMap(TileMap m)
        {
            m = BuildMap(m);
            //Console.WriteLine("Please enter a title:");
            //m.name = Console.ReadLine();
            return m;
        }

        public static void SaveMemoryNetwork(MemoryNetwork net)
        {
            Console.WriteLine("Please enter a title:");
            string netName = Console.ReadLine();

            var csv = new StringBuilder();
            string line;

            csv.AppendLine(netName);
            csv.AppendLine(net.numLayers.ToString());
            for (int i = 0; i < net.weights.Count; i++)
            {
                //csv.AppendLine(net.weights[i].RowCount.ToString() + "," + net.weights[i].ColumnCount.ToString());
                csv.Append(net.weights[i].ToString());
                csv.Append(net.biases[i].ToString());
            }
            File.WriteAllText(Directory.GetCurrentDirectory() + @"/mnet_" + netName + ".csv", csv.ToString());
        }

        public static void SaveClassicNetwork(ClassicNetwork net)
        {
            Console.WriteLine("Please enter a title:");
            string netName = Console.ReadLine();

            var csv = new StringBuilder();
            string line;

            csv.AppendLine(netName);
            csv.AppendLine(net.numLayers.ToString());
            for(int i = 0; i < net.weights.Count; i++)
            {
                //csv.AppendLine(net.weights[i].RowCount.ToString() + "," + net.weights[i].ColumnCount.ToString());
                csv.Append(net.weights[i].ToString());
                csv.Append(net.biases[i].ToString());
            }
            File.WriteAllText(Directory.GetCurrentDirectory() + @"/cnet_" + netName + ".csv", csv.ToString());
        }

        public static Solution ClassicProgramSolution(TileMap m, ClassicNetwork net, bool waitForGoAhead = true)
        {
            Solution solve = new Solution();

            int xPlayer = m.startPos[0];
            int yPlayer = m.startPos[1];

            double[] action;
            bool notDone = true;
            while (notDone)
            {
                Console.Write(m.visibleMap.ToString());
                if (waitForGoAhead) { if (Console.ReadKey().Key == ConsoleKey.Escape) { break; } }
                action = net.Act(m);

                if(action[0] == 1)
                {
                    solve.add(1, m.visibleMap);
                    xPlayer--;
                    if (m.isWall(xPlayer, yPlayer)) { xPlayer++; }
                    m.updateVisibleMap(xPlayer, yPlayer);
                }
                else if (action[1] == 1)
                {
                    solve.add(2, m.visibleMap);
                    xPlayer++;
                    if (m.isWall(xPlayer, yPlayer)) { xPlayer--; }
                    m.updateVisibleMap(xPlayer, yPlayer);
                }
                else if(action[2] == 1)
                {
                    solve.add(3, m.visibleMap);
                    yPlayer++;
                    if (m.isWall(xPlayer, yPlayer)) { yPlayer--; }
                    m.updateVisibleMap(xPlayer, yPlayer);
                }
                else if(action[3] == 1)
                {
                    solve.add(4, m.visibleMap);
                    yPlayer--;
                    if (m.isWall(xPlayer, yPlayer)) { yPlayer++; }
                    m.updateVisibleMap(xPlayer, yPlayer);
                }
                if (m.map[xPlayer, yPlayer] == 6) { notDone = false; }
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

        public static Solution MemoryProgramSolution(TileMap m, MemoryNetwork net, bool waitForGoAhead = true)
        {
            Solution solve = new Solution();

            int xPlayer = m.startPos[0];
            int yPlayer = m.startPos[1];

            double[] action;
            bool notDone = true;
            while (notDone)
            {
                Console.Write(m.visibleMap.ToString());
                if (waitForGoAhead) { Console.ReadKey(); }
                action = net.Act(m);

                if (action[0] == 1)
                {
                    solve.add(1, m.visibleMap);
                    xPlayer--;
                    if (m.isWall(xPlayer, yPlayer)) { xPlayer++; }
                    m.updateVisibleMap(xPlayer, yPlayer);
                }
                else if (action[1] == 1)
                {
                    solve.add(2, m.visibleMap);
                    xPlayer++;
                    if (m.isWall(xPlayer, yPlayer)) { xPlayer--; }
                    m.updateVisibleMap(xPlayer, yPlayer);
                }
                else if (action[2] == 1)
                {
                    solve.add(3, m.visibleMap);
                    yPlayer++;
                    if (m.isWall(xPlayer, yPlayer)) { yPlayer--; }
                    m.updateVisibleMap(xPlayer, yPlayer);
                }
                else if (action[3] == 1)
                {
                    solve.add(4, m.visibleMap);
                    yPlayer--;
                    if (m.isWall(xPlayer, yPlayer)) { yPlayer++; }
                    m.updateVisibleMap(xPlayer, yPlayer);
                }
                if (m.map[xPlayer, yPlayer] == 6) { notDone = false; }
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
        public static TileMap BuildMap()
        {
            TileMap m = new TileMap();
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
                Console.Clear();
                Console.Write(m.buildingMap.ToString());
            }
            Console.Write(m.map.ToString());
            m.reInit();
            return m;
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
                Console.Clear();
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

            File.WriteAllText(Directory.GetCurrentDirectory() + @"/map_" + m.name + ".csv", csv.ToString());
        }

        public static TileMap LoadMap()
        {
            TileMap m = new TileMap();

            DirectoryInfo dInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            //System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^^(?!soln_|cnet_|mnet_).*");
            //FileInfo[] files = dInfo.GetFiles("*.csv").Where(path => r.IsMatch(path));
            FileInfo[] files = dInfo.GetFiles("map_*.csv");
            //files = files.Where(x => x.Name.Contains("soln_")).ToArray();

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

            Console.WriteLine("Please select solutions to use for training. 'A' for all.");
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
                    if(s == "A")
                    {
                        for(int i = 0; i < selectedFiles.Count; i++) {
                            ret.Add(new Solution(files[i]));
                            selectedFiles[i] = true; }
                        notDone = false;
                        break;
                    }
                    if (!selectedFiles[Convert.ToInt16(s)])
                    {
                        ret.Add(new Solution(files[Convert.ToInt16(s)]));
                        selectedFiles[Convert.ToInt16(s)] = true;
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
            for(int i = 0; i < ret.Count; i++)
            {
                ret[i].ProcessExpanded();
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
