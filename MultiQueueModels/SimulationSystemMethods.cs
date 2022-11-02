using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MultiQueueModels
{
    public class SimulationSystemMethods
    {
        public SimulationSystem simulationSystem;
        Random random;

        public SimulationSystemMethods()
        {
            this.simulationSystem = new SimulationSystem();
            this.random = new Random();
        }

        public void Simulateion()
        {
            IntrArrivalTimeTable();
            ServiceTimeTable();
            FillFirstRow();
            FillOthersRows();

            //Calculate Performance for systems and per Server
            Performance();
            decimal TotalRunTime= 0;
            for (int i = 0; i < simulationSystem.NumberOfServers; i++)
                TotalRunTime = Math.Max(TotalRunTime, simulationSystem.Servers[i].FinishTime);
            for (int i = 0; i < simulationSystem.NumberOfServers; i++)
                ServerPerformance(TotalRunTime,simulationSystem.Servers[i]);
        }

        //Fill Simulation Table
        public void FillFirstRow()
        {
            SimulationCase simulationCaseTemp = new SimulationCase();

            simulationCaseTemp.CustomerNumber = 1;
            simulationCaseTemp.RandomInterArrival = 1;
            simulationCaseTemp.InterArrival = 0;
            simulationCaseTemp.ArrivalTime = 0;
            simulationCaseTemp.RandomService = random.Next(1, 101);
            
            //Assigning Server For First Row
            if (simulationSystem.SelectionMethod == Enums.SelectionMethod.HighestPriority)
            {
                int Highestpriority = simulationSystem.Servers[0].ID;
                int Highestpriorityindex = 0;
                for (int i = 0; i < simulationSystem.Servers.Count; i++)
                {
                    if (Highestpriority >= simulationSystem.Servers[i].ID)
                        Highestpriorityindex = i;
                }

                simulationCaseTemp.AssignedServer = simulationSystem.Servers[Highestpriorityindex];
                simulationCaseTemp.StartTime = simulationCaseTemp.ArrivalTime;
                simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);
                simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
                simulationSystem.Servers[Highestpriorityindex].FinishTime = simulationCaseTemp.EndTime;
                simulationSystem.Servers[Highestpriorityindex].TotalWorkingTime = simulationCaseTemp.EndTime - simulationSystem.Servers[Highestpriorityindex].TotalIdleTime;
            }
            else if (simulationSystem.SelectionMethod == Enums.SelectionMethod.Random)
            {
                int randomServer = random.Next(0, simulationSystem.Servers.Count);

                simulationCaseTemp.AssignedServer = simulationSystem.Servers[randomServer];
                simulationCaseTemp.StartTime = simulationCaseTemp.ArrivalTime;
                simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);
                simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
                simulationSystem.Servers[randomServer].FinishTime = simulationCaseTemp.EndTime;
                simulationSystem.Servers[randomServer].TotalWorkingTime = simulationCaseTemp.EndTime - simulationSystem.Servers[randomServer].TotalIdleTime;
            }
            else
            {
                simulationCaseTemp.AssignedServer = simulationSystem.Servers[0];
                simulationCaseTemp.StartTime = simulationCaseTemp.ArrivalTime;
                simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);
                simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
                simulationSystem.Servers[0].FinishTime = simulationCaseTemp.EndTime;
                simulationSystem.Servers[0].TotalWorkingTime = simulationCaseTemp.EndTime;
                simulationSystem.Servers[0].Utilization = simulationSystem.Servers[0].TotalWorkingTime / simulationCaseTemp.EndTime;
            }
            
            simulationCaseTemp.TimeInQueue = 0;
            simulationSystem.SimulationTable.Add(simulationCaseTemp);
        }

        public void FillOthersRows()
        {
            Queue<SimulationCase> queue = new Queue<SimulationCase>();
            for (int i = 1; i < simulationSystem.StoppingNumber; i++)
            {
                SimulationCase simulationCaseTemp = new SimulationCase();
                //Assigning Server Algorithm
                simulationCaseTemp.CustomerNumber = i + 1;
                simulationCaseTemp.RandomInterArrival = random.Next(1, 101);
                simulationCaseTemp.RandomService = random.Next(1, 101);
                simulationCaseTemp.InterArrival = GetIntrArrivalTime(simulationCaseTemp.RandomInterArrival, simulationSystem.InterarrivalDistribution);
                simulationCaseTemp.ArrivalTime = simulationCaseTemp.InterArrival + simulationSystem.SimulationTable[i - 1].ArrivalTime;
                simulationSystem.SimulationTable.Add(simulationCaseTemp);
            }

            SimulationCase simulationCase;
            for (int i = 1; i < simulationSystem.StoppingNumber; i++)
            {
                int nextindex = i + 1;
                simulationCase = DepartureEvent(nextindex, queue);
                if (simulationCase != null)
                {
                    if(simulationCase.StartTime >= simulationSystem.SimulationTable[i].StartTime)
                    {
                        AssigningServer(simulationSystem.SimulationTable[i], queue);
                    }
                    else
                    {
                        AssigningServer(simulationCase, queue);
                    }
                }
                else
                {
                    AssigningServer(simulationSystem.SimulationTable[i], queue);
                }
                
            }
        }

        //Departure Event
        public SimulationCase DepartureEvent(int nextIndex, Queue<SimulationCase> queue)
        {
            int MinFinishTimeServerIndex = FindMinFinishTime(simulationSystem.Servers);
            if(queue.Count > 0)
            {
                SimulationCase simulationCase = queue.Dequeue();
                int Highestpriority = 0;
                for (int j = 0; j < simulationSystem.NumberOfServers; j++)
                {
                    if ((simulationSystem.Servers[j].FinishTime <= (simulationCase.ArrivalTime + simulationCase.TimeInQueue)) && (queue.Count == 0))
                    {
                        Highestpriority = j;
                        break;
                    }
                }
                simulationCase.AssignedServer = simulationSystem.Servers[Highestpriority];
                simulationCase.ServiceTime = GetServiceTime(simulationCase.RandomService, simulationCase.AssignedServer);
                simulationCase.EndTime = simulationCase.StartTime + simulationCase.ServiceTime;
                return simulationCase;
            }
            else
            {
                if(nextIndex < simulationSystem.SimulationTable.Count)
                {
                    if(simulationSystem.SimulationTable[nextIndex].ArrivalTime > simulationSystem.Servers[MinFinishTimeServerIndex].FinishTime)
                        simulationSystem.Servers[MinFinishTimeServerIndex].TotalIdleTime = simulationSystem.SimulationTable[nextIndex].ArrivalTime - simulationSystem.Servers[MinFinishTimeServerIndex].FinishTime;
                }
                    
                return null;
            }
        }

        //New Arrivall Event
        public void AssigningServer(SimulationCase simulationCase, Queue<SimulationCase> queue)
        {
            if (simulationSystem.SelectionMethod == Enums.SelectionMethod.HighestPriority)
                HighestPriorityAssigingServer(simulationCase, queue);

            else if (simulationSystem.SelectionMethod == Enums.SelectionMethod.Random)
                RandomAssigingServer(simulationCase, queue);

            else
                LeastUtiliziationAssigingServer(simulationCase, queue);
        }

        public void HighestPriorityAssigingServer(SimulationCase simulationCase, Queue<SimulationCase> queue)
        {
            int Highestpriority = 0;
            bool AllServerBeasy = true;
            for (int j = 0; j < simulationSystem.NumberOfServers; j++)
            {
                if ((simulationSystem.Servers[j].FinishTime <= (simulationCase.ArrivalTime + simulationCase.TimeInQueue)) && (queue.Count == 0))
                {
                    Highestpriority = j;
                    AllServerBeasy = false;
                    break;
                }
            }

            if (AllServerBeasy)
            {
                queue.Enqueue(simulationCase);
                simulationCase.TimeInQueue += FindTimeInQueue(queue);
                simulationCase.StartTime = simulationCase.TimeInQueue + simulationCase.ArrivalTime;
                simulationSystem.PerformanceMeasures.MaxQueueLength = Math.Max(simulationSystem.PerformanceMeasures.MaxQueueLength, queue.Count);
            }
            else
            {
                simulationCase.AssignedServer = simulationSystem.Servers[Highestpriority];
                simulationCase.StartTime = simulationCase.ArrivalTime + simulationCase.TimeInQueue;
                simulationCase.ServiceTime = GetServiceTime(simulationCase.RandomService, simulationCase.AssignedServer);
                simulationCase.EndTime = simulationCase.StartTime + simulationCase.ServiceTime;
                simulationSystem.Servers[Highestpriority].FinishTime = simulationCase.EndTime;
                simulationSystem.Servers[Highestpriority].TotalWorkingTime = simulationCase.EndTime - simulationSystem.Servers[Highestpriority].TotalIdleTime;
            }
        }

        public void RandomAssigingServer(SimulationCase simulationCase, Queue<SimulationCase> queue)
        {
            int randomServer = random.Next(0, simulationSystem.Servers.Count);
            bool AllServerBeasy = true;

            while ((simulationSystem.Servers[randomServer].FinishTime <= (simulationCase.ArrivalTime + simulationCase.TimeInQueue)) && (queue.Count == 0))
            {
                randomServer = random.Next(0, simulationSystem.Servers.Count);
                AllServerBeasy = false;
            }
                

            if (AllServerBeasy)
            {
                queue.Enqueue(simulationCase);
                simulationCase.TimeInQueue = FindTimeInQueue(queue);
                simulationCase.StartTime = simulationCase.TimeInQueue + simulationCase.ArrivalTime;
                simulationSystem.PerformanceMeasures.MaxQueueLength = Math.Max(simulationSystem.PerformanceMeasures.MaxQueueLength, queue.Count);
            }
            else
            {
                simulationCase.AssignedServer = simulationSystem.Servers[randomServer];
                simulationCase.StartTime = simulationCase.ArrivalTime;
                simulationCase.EndTime = simulationCase.StartTime + simulationCase.ServiceTime;
                simulationCase.TimeInQueue = 0;
                simulationSystem.Servers[randomServer].FinishTime = simulationCase.EndTime;
                simulationSystem.Servers[randomServer].TotalWorkingTime = simulationCase.EndTime - simulationSystem.Servers[randomServer].TotalIdleTime;
            }
            
        }

        public void LeastUtiliziationAssigingServer(SimulationCase simulationCase, Queue<SimulationCase> queue)
        {
            int LeastUtilizationServer = 0;
            decimal LeastUtilization = 0;
            bool AllServerBeasy = true;

            for (int j = 0; j < simulationSystem.NumberOfServers; j++)
            {
                if ((simulationSystem.Servers[j].FinishTime <= (simulationCase.ArrivalTime + simulationCase.TimeInQueue)) && (queue.Count == 0) && (simulationSystem.Servers[j].Utilization <= LeastUtilization))
                {
                    LeastUtilizationServer = j;
                    LeastUtilization = simulationSystem.Servers[j].Utilization;
                    AllServerBeasy = false;
                }
            }

            if (AllServerBeasy)
            {
                queue.Enqueue(simulationCase);
                simulationCase.TimeInQueue = FindTimeInQueue(queue);
                simulationCase.StartTime = simulationCase.TimeInQueue + simulationCase.ArrivalTime;
                simulationSystem.PerformanceMeasures.MaxQueueLength = Math.Max(simulationSystem.PerformanceMeasures.MaxQueueLength, queue.Count);
            }
            else
            {
                simulationCase.AssignedServer = simulationSystem.Servers[LeastUtilizationServer];
                simulationCase.StartTime = simulationCase.ArrivalTime;
                simulationCase.EndTime = simulationCase.StartTime + simulationCase.ServiceTime;
                simulationCase.TimeInQueue = 0;
                simulationSystem.Servers[LeastUtilizationServer].FinishTime = simulationCase.EndTime;
                simulationSystem.Servers[LeastUtilizationServer].TotalWorkingTime = simulationCase.EndTime - simulationSystem.Servers[LeastUtilizationServer].TotalIdleTime;
                int simulatuinTotalTime = simulationCase.EndTime;
                for (int j = 0; j < simulationSystem.NumberOfServers; j++)
                {
                    if (simulationSystem.Servers[j].FinishTime >= simulatuinTotalTime)
                        simulatuinTotalTime = simulationSystem.Servers[j].FinishTime;
                }
                simulationSystem.Servers[LeastUtilizationServer].Utilization = simulationSystem.Servers[LeastUtilizationServer].TotalWorkingTime / simulatuinTotalTime;
            }

        }

        public int FindTimeInQueue(Queue<SimulationCase> queue)
        {
            int MinFinishIndex = FindMinFinishTime(simulationSystem.Servers);
            int TotalWaitingTime;

            if (simulationSystem.Servers[MinFinishIndex].FinishTime > queue.ElementAt(queue.Count - 1).ArrivalTime)
                TotalWaitingTime = simulationSystem.Servers[MinFinishIndex].FinishTime - queue.ElementAt(queue.Count - 1).ArrivalTime;
            else
                TotalWaitingTime = 0;

            return TotalWaitingTime;
        }

        public int FindMinFinishTime(List<Server> servers)
        {
            int minFinishTime = servers[0].FinishTime;
            int minFinishTimeindex = 0;
            for (int j = 0; j < servers.Count; j++)
            {
                if (minFinishTime < servers[j].FinishTime)
                {
                    minFinishTimeindex = j;
                    minFinishTime = servers[j].FinishTime;
                }
            }
            return (minFinishTimeindex);
        }

        //InterArrival Time Distrbution Table Mathods
        public void IntrArrivalTimeTable()
        {
            Decimal TotalCalcProp = 0;
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

        public int GetIntrArrivalTime(int RandomDigit, List<TimeDistribution> Systeminter)
        {
            for (int i = 0; i < Systeminter.Count; i++)
            {
                if ((RandomDigit <= Systeminter[i].MaxRange) && (RandomDigit >= Systeminter[i].MinRange))
                    return Systeminter[i].Time;
            }
            return 0;
        }

        //Service Time Distrbution Table Mathods
        public void ServiceTimeTable()
        {
            for(int i = 0; i < simulationSystem.Servers.Count; i++)
            {
                Decimal TotalCalcProp = 0;
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
                    return AssignedServer.TimeDistribution[i].Time;
            }
            return 0;
        }

        // Calculate performance per server and for system
        public void Performance()
        {
            decimal TotalWaitedTime =0;
            decimal WaitedCustomerCounter =0;
            for(int i=0; i < simulationSystem.SimulationTable.Count; i++)
            {
                TotalWaitedTime += (decimal)simulationSystem.SimulationTable[i].TimeInQueue;
                if (simulationSystem.SimulationTable[i].TimeInQueue > 0)
                    WaitedCustomerCounter++;
            }
            simulationSystem.PerformanceMeasures.AverageWaitingTime = TotalWaitedTime / simulationSystem.SimulationTable.Count;
            simulationSystem.PerformanceMeasures.WaitingProbability = WaitedCustomerCounter / simulationSystem.SimulationTable.Count;
        }

        public void ServerPerformance(decimal TotalRunTime,Server server)
        {
            decimal customersofServer = 0;
            for(int i=0;i < simulationSystem.SimulationTable.Count; i++)
            {
                if(simulationSystem.SimulationTable[i].AssignedServer.ID == server.ID)
                    customersofServer++;
            }

            server.IdleProbability = Convert.ToDecimal(server.TotalIdleTime) / TotalRunTime;
            server.AverageServiceTime = Convert.ToDecimal(server.TotalWorkingTime) / customersofServer;
            server.Utilization = Convert.ToDecimal(server.TotalWorkingTime) / TotalRunTime;
        }

        // fill system object with data in file
        public void ReadFile(String Path)
        {
            simulationSystem.SimulationTable.Clear();
            simulationSystem.Servers.Clear();
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
            timeDistributions.Clear();
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
