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
            for (int i = 1; i < simulationSystem.NumberOfServers; i++)
                ServerPerformance(simulationSystem.Servers[i]);
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
            simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);
            
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
                simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
                simulationSystem.Servers[Highestpriorityindex].IsBeasy = true;
                simulationSystem.Servers[Highestpriorityindex].FinishTime = simulationCaseTemp.EndTime;
                simulationSystem.Servers[Highestpriorityindex].TotalWorkingTime = simulationCaseTemp.EndTime - simulationSystem.Servers[Highestpriorityindex].TotalIdleTime;
            }
            else if (simulationSystem.SelectionMethod == Enums.SelectionMethod.Random)
            {
                int randomServer = random.Next(0, simulationSystem.Servers.Count);

                simulationCaseTemp.AssignedServer = simulationSystem.Servers[randomServer];
                simulationCaseTemp.StartTime = simulationCaseTemp.ArrivalTime;
                simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
                simulationSystem.Servers[randomServer].IsBeasy = true;
                simulationSystem.Servers[randomServer].FinishTime = simulationCaseTemp.EndTime;
                simulationSystem.Servers[randomServer].TotalWorkingTime = simulationCaseTemp.EndTime - simulationSystem.Servers[randomServer].TotalIdleTime;
            }
            else
            {
                simulationCaseTemp.AssignedServer = simulationSystem.Servers[0];
                simulationCaseTemp.StartTime = simulationCaseTemp.ArrivalTime;
                simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
                simulationSystem.Servers[0].IsBeasy = true;
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
                simulationCaseTemp.InterArrival = GetIntrArrivalTime(simulationCaseTemp.RandomService, simulationSystem); ;
                simulationCaseTemp.ArrivalTime = simulationCaseTemp.InterArrival + simulationSystem.SimulationTable[i - 1].ArrivalTime;
                simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);
                AssigningServer(simulationCaseTemp, queue);
                DepartureEvent();

                simulationSystem.SimulationTable.Add(simulationCaseTemp);
            }
        }

        //Departure Event
        public void DepartureEvent()
        {

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
            int Highestpriorityindex = 0;
            bool AllServerBeasy = true;

            for (int j = 0; j < simulationSystem.NumberOfServers; j++)
            {
                if (!simulationSystem.Servers[j].IsBeasy)
                {
                    Highestpriorityindex = j;
                    Highestpriority = simulationSystem.Servers[j].ID;
                    break;
                }
            }

            for (int j = 0; j < simulationSystem.NumberOfServers; j++)
            {
                if ((!simulationSystem.Servers[j].IsBeasy) && (simulationSystem.Servers[j].ID <= Highestpriority))
                {
                    Highestpriority = simulationSystem.Servers[j].ID;
                    Highestpriorityindex = j;
                    AllServerBeasy = false;
                } 
            }

            if (AllServerBeasy)
            {
                queue.Enqueue(simulationCase);
                simulationCase.StartTime = FindStartTime(queue);
                simulationCase.TimeInQueue = simulationCase.StartTime - simulationCase.ArrivalTime;
                simulationSystem.PerformanceMeasures.MaxQueueLength = Math.Max(simulationSystem.PerformanceMeasures.MaxQueueLength, queue.Count);
                
            }
            else
            {
                simulationCase.AssignedServer = simulationSystem.Servers[Highestpriorityindex];
                simulationCase.StartTime = simulationCase.ArrivalTime;
                simulationCase.EndTime = simulationCase.StartTime + simulationCase.ServiceTime;
                simulationCase.TimeInQueue = 0;
                simulationSystem.Servers[Highestpriorityindex].IsBeasy = true;
                simulationSystem.Servers[Highestpriorityindex].FinishTime = simulationCase.EndTime;
                simulationSystem.Servers[Highestpriorityindex].TotalWorkingTime = simulationCase.EndTime - simulationSystem.Servers[Highestpriorityindex].TotalIdleTime;
            }
        }

        public void RandomAssigingServer(SimulationCase simulationCase, Queue<SimulationCase> queue)
        {
            int randomServer = random.Next(0, simulationSystem.Servers.Count);
            bool AllServerBeasy = true;

            for (int j = 0; j < simulationSystem.NumberOfServers; j++)
            {
                if (!simulationSystem.Servers[j].IsBeasy)
                    AllServerBeasy = false;
            }

            while ((!simulationSystem.Servers[randomServer].IsBeasy) && (!AllServerBeasy));
                randomServer = random.Next(0, simulationSystem.Servers.Count);

            if (AllServerBeasy)
            {
                queue.Enqueue(simulationCase);
                simulationCase.StartTime = FindStartTime(queue);
                simulationCase.TimeInQueue = simulationCase.StartTime - simulationCase.ArrivalTime;
                simulationSystem.PerformanceMeasures.MaxQueueLength = Math.Max(simulationSystem.PerformanceMeasures.MaxQueueLength, queue.Count);
            }
            else
            {
                simulationCase.AssignedServer = simulationSystem.Servers[randomServer];
                simulationCase.StartTime = simulationCase.ArrivalTime;
                simulationCase.EndTime = simulationCase.StartTime + simulationCase.ServiceTime;
                simulationCase.TimeInQueue = 0;
                simulationSystem.Servers[randomServer].IsBeasy = true;
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
                if (!simulationSystem.Servers[j].IsBeasy)
                {
                    LeastUtilizationServer = j;
                    LeastUtilization = simulationSystem.Servers[j].Utilization;
                    break;
                }
            }

            for (int j = 0; j < simulationSystem.NumberOfServers; j++)
            {
                if ((!simulationSystem.Servers[j].IsBeasy) && (simulationSystem.Servers[j].Utilization <= LeastUtilization))
                {
                    LeastUtilization = simulationSystem.Servers[j].Utilization;
                    LeastUtilizationServer = j;
                    AllServerBeasy = false;
                }
            }

            if (AllServerBeasy)
            {
                queue.Enqueue(simulationCase);
                simulationCase.StartTime = FindStartTime(queue);
                simulationCase.TimeInQueue = simulationCase.StartTime - simulationCase.ArrivalTime;  
                simulationSystem.PerformanceMeasures.MaxQueueLength = Math.Max(simulationSystem.PerformanceMeasures.MaxQueueLength, queue.Count);
            }
            else
            {
                simulationCase.AssignedServer = simulationSystem.Servers[LeastUtilizationServer];
                simulationCase.StartTime = simulationCase.ArrivalTime;
                simulationCase.EndTime = simulationCase.StartTime + simulationCase.ServiceTime;
                simulationCase.TimeInQueue = 0;
                simulationSystem.Servers[LeastUtilizationServer].IsBeasy = true;
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

        public int FindStartTime(Queue<SimulationCase> queue)
        {
            int minFinishTime = simulationSystem.Servers[0].FinishTime;
            List<Server> TempServers = simulationSystem.Servers;
            for (int k = 0; k < (queue.Count - 1); k++)
            {
                int minFinishTimeindex = 0;
                for (int j = 0; j < TempServers.Count; j++)
                {
                    if (minFinishTime <= TempServers[j].FinishTime)
                    {
                        minFinishTimeindex = j;
                        minFinishTime = TempServers[j].FinishTime;
                    }
                }
                TempServers[minFinishTimeindex].FinishTime += simulationSystem.SimulationTable[queue.ElementAt(k).CustomerNumber - 1].ServiceTime;
            }
            return minFinishTime;
        }

        //InterArrival Time Distrbution Table Mathods
        public void IntrArrivalTimeTable()
        {

        }

        public int GetIntrArrivalTime(int RandomDigit, SimulationSystem Systeminter)
        {
            for (int i = 0; i < Systeminter.InterarrivalDistribution.Count; i++)
            {
                if (RandomDigit <= Systeminter.InterarrivalDistribution[i].MaxRange && RandomDigit >= Systeminter.InterarrivalDistribution[i].MinRange)
                    return Systeminter.InterarrivalDistribution[i].Time;
            }
            return 0;
        }

        //Service Time Distrbution Table Mathods
        public void ServiceTimeTable()
        {

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

        }

        public void ServerPerformance(Server server)
        {

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
