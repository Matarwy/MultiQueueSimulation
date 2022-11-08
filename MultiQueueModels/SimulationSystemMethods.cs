using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MultiQueueModels.Enums;


namespace MultiQueueModels
{
    public class SimulationSystemMethods
    {
        public SimulationSystem simulationSystem;
        private Random random;


        public SimulationSystemMethods()
        {
            this.simulationSystem = new SimulationSystem();
            this.random = new Random();
        }

        public void Simulateion()
        {
            IntrArrivalTimeTable();
            ServiceTimeTable();
            FillRows();
            Performance(simulationSystem.SimulationTable.Count);
            for (int j = 0; j < simulationSystem.Servers.Count; j++)
            {
                simulationSystem.Servers[j].calculateMeasures(getTotalTime());
            }
        }

        public void FillRows()
        {
            for (int i = 0; i < simulationSystem.StoppingNumber; i++)
            {
                SimulationCase simulationCaseTemp = new SimulationCase();
                simulationCaseTemp.CustomerNumber = i+1;
                simulationCaseTemp.RandomInterArrival = random.Next(1, 101);
                simulationCaseTemp.RandomService = random.Next(1, 101);
                simulationCaseTemp.InterArrival = GetIntrArrivalTime(simulationCaseTemp.RandomInterArrival);


                if (i == 0)
                {
                    simulationCaseTemp.ArrivalTime = simulationCaseTemp.StartTime= 0;

                }
                else
                {
                    simulationCaseTemp.ArrivalTime = simulationCaseTemp.InterArrival + simulationSystem.SimulationTable[i - 1].ArrivalTime;
                }

                //Assigning Server Algorithm
                int ServerIndex = AssigningServer(simulationCaseTemp.ArrivalTime);

                if (ServerIndex == -1)
                {
                    int minServerFinish = FindMinFinishTime();
                    simulationCaseTemp.StartTime = simulationSystem.Servers[minServerFinish].FinishTime;
                    int ServerIndex2 = AssigningServer(simulationCaseTemp.StartTime);
                    simulationSystem.Servers[ServerIndex2].numberOfCustomers++;
                    simulationCaseTemp.AssignedServer = simulationSystem.Servers[ServerIndex2];
                    simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);
                    simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
                    simulationCaseTemp.TimeInQueue = simulationCaseTemp.StartTime - simulationCaseTemp.ArrivalTime;
                    simulationSystem.Servers[ServerIndex2].FinishTime = simulationCaseTemp.EndTime;
                }
                else
                {
                    simulationCaseTemp.StartTime = simulationCaseTemp.ArrivalTime;
                    simulationCaseTemp.AssignedServer = simulationSystem.Servers[ServerIndex];
                    simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);
                    simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
                    simulationSystem.Servers[ServerIndex].FinishTime = simulationCaseTemp.EndTime;
                    simulationSystem.Servers[ServerIndex].numberOfCustomers++;
                    simulationCaseTemp.TimeInQueue = 0;
                }

                simulationSystem.SimulationTable.Add(simulationCaseTemp);
            }
        }

        //New Arrivall Event
        public int AssigningServer(int arrivaTime)
        {
            int index = -1, id = 9999;

            if (simulationSystem.SelectionMethod == Enums.SelectionMethod.HighestPriority)
            {
                for (int i = 0; i < simulationSystem.Servers.Count; i++)
                {
                    if (simulationSystem.Servers[i].FinishTime <= arrivaTime)
                    {
                        if (simulationSystem.Servers[i].ID < id)
                        {
                            id = simulationSystem.Servers[i].ID;
                            index = i;
                        }
                    }
                }
            }
            else if (simulationSystem.SelectionMethod == Enums.SelectionMethod.Random)
            {
                for (int i = 0; i < simulationSystem.Servers.Count; i++)
                {
                    if (simulationSystem.Servers[i].FinishTime <= arrivaTime)
                    {
                        index = i;
                        break;
                    }
                }
            }
            else if (simulationSystem.SelectionMethod == Enums.SelectionMethod.LeastUtilization)
            {
                int MinWorkingTime = 9999;
                for (int i = 0; i < simulationSystem.Servers.Count; i++)
                {
                    if (simulationSystem.Servers[i].FinishTime <= arrivaTime)
                    {
                        if (simulationSystem.Servers[i].TotalWorkingTime < MinWorkingTime)
                        {
                            MinWorkingTime = simulationSystem.Servers[i].TotalWorkingTime;
                            index = i;
                        }
                    }
                }
            }
            return index;
        }
        public int getTotalTime()
        {
            int val = -1;
            for (int i = 0; i < simulationSystem.Servers.Count; i++)
            {
                val = Math.Max(val, simulationSystem.Servers[i].FinishTime);
            }
            return val;
        }
        public int FindMinFinishTime()
        {
            int minFinishTime = simulationSystem.Servers[0].FinishTime;
            int minFinishTimeindex = 0;
            for (int j = 0; j < simulationSystem.Servers.Count; j++)
            {
                if (minFinishTime >= simulationSystem.Servers[j].FinishTime)
                {
                    minFinishTimeindex = j;
                    minFinishTime = simulationSystem.Servers[j].FinishTime;
                }
            }
            return (minFinishTimeindex);
        }

        //InterArrival Time Distrbution Table Mathods
        public void IntrArrivalTimeTable()
        {
            decimal TotalCalcProp = 0;
            for (int i = 0; i < simulationSystem.InterarrivalDistribution.Count; i++)
            {
                TotalCalcProp += simulationSystem.InterarrivalDistribution[i].Probability;
                simulationSystem.InterarrivalDistribution[i].CummProbability = TotalCalcProp;
                Console.WriteLine(simulationSystem.InterarrivalDistribution[i].CummProbability);
                simulationSystem.InterarrivalDistribution[i].MaxRange = Convert.ToInt32(simulationSystem.InterarrivalDistribution[i].CummProbability * 100);
                if (i == 0)
                    simulationSystem.InterarrivalDistribution[i].MinRange = 1;
                else
                {
                    int lastindex = i-1;
                    simulationSystem.InterarrivalDistribution[i].MinRange = Convert.ToInt32((simulationSystem.InterarrivalDistribution[lastindex].CummProbability * 100) + 1);
                }
            }
        }

        public int GetIntrArrivalTime(int RandomDigit)
        {
            for (int i = 0; i < simulationSystem.InterarrivalDistribution.Count; i++)
            {
                if ((RandomDigit <= simulationSystem.InterarrivalDistribution[i].MaxRange) 
                    && (RandomDigit >= simulationSystem.InterarrivalDistribution[i].MinRange))
                {
                    return simulationSystem.InterarrivalDistribution[i].Time;
                }
                    
            }
            return 0;
        }

        //Service Time Distrbution Table Mathods
        public void ServiceTimeTable()
        {
            for(int i = 0; i < simulationSystem.Servers.Count; i++)
            {
                decimal TotalCalcProp = 0;
                for (int j = 0; j < simulationSystem.Servers[i].TimeDistribution.Count; j++)
                {
                    TotalCalcProp += simulationSystem.Servers[i].TimeDistribution[j].Probability;
                    simulationSystem.Servers[i].TimeDistribution[j].CummProbability = TotalCalcProp;
                    simulationSystem.Servers[i].TimeDistribution[j].MaxRange = Convert.ToInt32(simulationSystem.Servers[i].TimeDistribution[j].CummProbability * 100);
                    if (j == 0)
                        simulationSystem.Servers[i].TimeDistribution[j].MinRange = 1;
                    else
                    {
                        int lastindex = j - 1;
                        simulationSystem.Servers[i].TimeDistribution[j].MinRange = Convert.ToInt32((simulationSystem.Servers[i].TimeDistribution[lastindex].CummProbability * 100) + 1);
                    }
                }
            }
        }

        public int GetServiceTime(int RandomDigt, Server AssignedServer)
        {
            for (int i = 0; i < AssignedServer.TimeDistribution.Count; i++)
            {
                if (RandomDigt <= AssignedServer.TimeDistribution[i].MaxRange && RandomDigt >= AssignedServer.TimeDistribution[i].MinRange)
                {
                    AssignedServer.TotalWorkingTime += AssignedServer.TimeDistribution[i].Time;
                    return AssignedServer.TimeDistribution[i].Time;
                } 
            }
            return 0;

        }

        // Calculate performance per server and for system
        public void Performance(int cus_num)
        {
            int num_customer_waited = 0, time_inqeue = 0, m = 0;
            Dictionary<int, int> d = new Dictionary<int, int>();
            for (int i = 1; i <= getTotalTime(); i++)
            {
                d[i] = 0;
            }
            for (int i = 0; i < simulationSystem.SimulationTable.Count; i++)
            {
                if (simulationSystem.SimulationTable[i].TimeInQueue > 0)
                {
                    num_customer_waited++;
                    time_inqeue = time_inqeue + simulationSystem.SimulationTable[i].TimeInQueue;

                    for (int j = simulationSystem.SimulationTable[i].ArrivalTime; j < simulationSystem.SimulationTable[i].StartTime; j++)
                    {
                        d[j]++;
                    }
                    foreach (KeyValuePair<int, int> x in d)
                    {
                        m = Math.Max(m, x.Value);
                    }
                }
            }
            simulationSystem.PerformanceMeasures.AverageWaitingTime = (decimal)((decimal)time_inqeue / (decimal)cus_num);
            simulationSystem.PerformanceMeasures.WaitingProbability = (decimal)((decimal)num_customer_waited / (decimal)cus_num);
            simulationSystem.PerformanceMeasures.MaxQueueLength = m;
        }

        // fill system object with data in file
        public void ReadFile(String Path)
        {
            simulationSystem.SimulationTable.Clear();
            simulationSystem.Servers.Clear();
            simulationSystem.InterarrivalDistribution.Clear();

            string[] lines = File.ReadAllLines(Path);

            for(int i = 0; i < lines.Length; i++)
            {
                
                if (lines[i] == "NumberOfServers")
                    simulationSystem.NumberOfServers = Convert.ToInt32(lines[i+1]);

                else if (lines[i] == "StoppingNumber")
                    simulationSystem.StoppingNumber = Convert.ToInt32(lines[i + 1]);

                else if (lines[i] == "StoppingCriteria")
                    simulationSystem.StoppingCriteria = (Enums.StoppingCriteria)Convert.ToInt32(lines[i + 1]);

                else if (lines[i] == "SelectionMethod")
                    simulationSystem.SelectionMethod = (Enums.SelectionMethod)Convert.ToInt32(lines[i + 1]);

                else if (lines[i] == "InterarrivalDistribution")
                    GetTimeDistrbutionDataFromFile(i, lines, simulationSystem.InterarrivalDistribution);

                for(int j=0; j < simulationSystem.NumberOfServers; j++)
                {
                    if (lines[i] == ("ServiceDistribution_Server" + (j + 1)))
                    {
                        Server tempS = new Server();
                        tempS.ID = j + 1;
                        GetTimeDistrbutionDataFromFile(i,lines, tempS.TimeDistribution);
                        simulationSystem.Servers.Add(tempS);
                    }
                }
            }
        }

        public void GetTimeDistrbutionDataFromFile( int Loopindex, String[] lines, List<TimeDistribution> timeDistributions)
        {
            int LoopCounter = Loopindex + 1;
            while (lines[LoopCounter] != "")
            {
                //splite string by , to array of strings
                Regex.Replace(lines[LoopCounter], @"\s+", "");
                string[] timeAndProp = lines[LoopCounter].Split(',');

                //craete temprory time distrbution object
                TimeDistribution tempTAP = new TimeDistribution();
                tempTAP.Time = Convert.ToInt32(timeAndProp[0]);
                tempTAP.Probability = Convert.ToDecimal(timeAndProp[1]);

                //pass temprory time distrbution object to simulation system
                timeDistributions.Add(tempTAP);
                LoopCounter++;
                if (LoopCounter >= lines.Length)
                    break;
            }
        }
    }
}
