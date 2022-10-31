using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Queue<int> queue = new Queue<int>();
            IntrArrivalTimeTable();
            ServiceTimeTable();
            FillFirstRow();
            FillOthersRows();

            //Calculate Performance for systems and per Server
            Performance();
            for (int i = 1; i < simulationSystem.NumberOfServers; i++)
                ServerPerformance(simulationSystem.Servers[i]);
        }

        public void AssigningServer(SimulationCase simulationCase)
        {
            if (simulationSystem.SelectionMethod == Enums.SelectionMethod.HighestPriority)
                HighestPriorityAssigingServer(simulationCase);

            else if (simulationSystem.SelectionMethod == Enums.SelectionMethod.Random)
                RandomAssigingServer(simulationCase);

            else
                LeastUtiliziationAssigingServer(simulationCase);
        }

        public void HighestPriorityAssigingServer(SimulationCase simulationCase)
        {
            int Highestpriority = simulationSystem.Servers[0].ID;
            int Highestpriorityindex = 0;
            bool AllServerBeasy = true;

            for (int j = 0; j < simulationSystem.NumberOfServers; j++)
            {
                if ((!simulationSystem.Servers[j].IsBeasy) && (simulationSystem.Servers[j].ID >= Highestpriority))
                {
                    Highestpriorityindex = j;
                    AllServerBeasy = false;
                } 
            }

            if (AllServerBeasy)
            {

            }
            else
            {
                simulationCase.AssignedServer = simulationSystem.Servers[Highestpriorityindex];
                simulationSystem.Servers[Highestpriorityindex].IsBeasy = true;
            }  
        }

        public void RandomAssigingServer(SimulationCase simulationCase)
        {
            int randomServer = random.Next(0, simulationSystem.Servers.Count);
            bool AllServerBeasy = true;

            for (int j = 0; j < simulationSystem.NumberOfServers; j++)
            {
                if (!simulationSystem.Servers[j].IsBeasy)
                    AllServerBeasy = false;
            }

            while ((simulationSystem.Servers[randomServer].IsBeasy) && (!AllServerBeasy));
                randomServer = random.Next(0, simulationSystem.Servers.Count);

            if (AllServerBeasy)
            {

            }
            else
            {
                simulationCase.AssignedServer = simulationSystem.Servers[randomServer];
                simulationSystem.Servers[randomServer].IsBeasy = true;
            }
            
        }

        public void LeastUtiliziationAssigingServer(SimulationCase simulationCase)
        {
            int LeastUtilizationServer = 0;

            for (int j = 0; j < simulationSystem.Servers.Count; j++)
            {
                if (!simulationSystem.Servers[j].IsBeasy)
                {
                    LeastUtilizationServer = j;
                }
            }

            simulationCase.AssignedServer = simulationSystem.Servers[LeastUtilizationServer];
            simulationSystem.Servers[LeastUtilizationServer].IsBeasy = true;
        }

        public void FillFirstRow()
        {
            SimulationCase simulationCaseTemp = new SimulationCase();

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
                simulationSystem.Servers[Highestpriorityindex].IsBeasy = true;
            }
            else if (simulationSystem.SelectionMethod == Enums.SelectionMethod.Random)
            {
                int randomServer = random.Next(0, simulationSystem.Servers.Count);
                simulationCaseTemp.AssignedServer = simulationSystem.Servers[randomServer];
                simulationSystem.Servers[randomServer].IsBeasy = true;
            }
            else
            {
                simulationCaseTemp.AssignedServer = simulationSystem.Servers[0];
                simulationSystem.Servers[0].IsBeasy = true;
            }

            simulationCaseTemp.CustomerNumber = 1;
            simulationCaseTemp.RandomInterArrival = 1;
            simulationCaseTemp.InterArrival = 0;
            simulationCaseTemp.ArrivalTime = 0;
            simulationCaseTemp.RandomService = random.Next(1, 101);
            simulationCaseTemp.StartTime = 0;
            simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);
            simulationCaseTemp.EndTime = simulationCaseTemp.StartTime + simulationCaseTemp.ServiceTime;
            simulationCaseTemp.TimeInQueue = 0;
            simulationSystem.SimulationTable.Add(simulationCaseTemp);
        }

        public void FillOthersRows()
        {
            for (int i = 1; i < simulationSystem.StoppingNumber; i++)
            {
                SimulationCase simulationCaseTemp = new SimulationCase();
                //Assigning Server Algorithm
                AssigningServer(simulationCaseTemp);
                simulationCaseTemp.CustomerNumber = i + 1;
                simulationCaseTemp.RandomInterArrival = random.Next(1, 101);
                simulationCaseTemp.RandomService = random.Next(1, 101);
                simulationCaseTemp.InterArrival = GetIntrArrivalTime(simulationCaseTemp.RandomService, simulationSystem); ;
                simulationCaseTemp.ArrivalTime = simulationCaseTemp.InterArrival + simulationSystem.SimulationTable[i - 1].ArrivalTime;
                simulationCaseTemp.ServiceTime = GetServiceTime(simulationCaseTemp.RandomService, simulationCaseTemp.AssignedServer);

                simulationSystem.SimulationTable.Add(simulationCaseTemp);
            }
        }

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

        public void Performance()
        {

        }

        public void ServerPerformance(Server server)
        {

        }

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
