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

        }

        public void IntrArrivalTimeTable()
        {

        }

        public void ServiceTimeTable()
        {

        }

        public void Performance()
        {

        }

        public void ReadFile(String Path)
        {
            string[] lines = File.ReadAllLines(Path);

            for(int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
                if(lines[i] == "NumberOfServers")
                    simulationSystem.NumberOfServers = Convert.ToInt32(lines[i+1]);

                else if (lines[i] == "StoppingNumber")
                    simulationSystem.StoppingNumber = Convert.ToInt32(lines[i + 1]);

                else if (lines[i] == "StoppingCriteria")
                    simulationSystem.StoppingCriteria = (Enums.StoppingCriteria)Convert.ToInt32(lines[i + 1]);

                else if (lines[i] == "SelectionMethod")
                    simulationSystem.SelectionMethod = (Enums.SelectionMethod)Convert.ToInt32(lines[i + 1]);

                else if (lines[i] == "InterarrivalDistribution")
                {
                    int LoopCounter = i +1;
                    while(lines[LoopCounter] != "")
                    {
                        //splite string by , to array of strings
                        Regex.Replace(lines[LoopCounter], @"\s+", "");
                        string[] timeAndProp = lines[LoopCounter].Split(',');

                        //craete temprory time distrbution object
                        TimeDistribution temp = new TimeDistribution();
                        temp.Time = Convert.ToInt32(timeAndProp[0]);
                        temp.Probability = Convert.ToDecimal(timeAndProp[1]);

                        //pass temprory time distrbution object to simulation system
                        simulationSystem.InterarrivalDistribution.Add(temp);
                        LoopCounter++;
                    }
                }

                for(int j=0; j < simulationSystem.NumberOfServers; j++)
                {
                    if (lines[i] == ("ServiceDistribution_Server" + (j + 1)))
                    {
                        Server tempS = new Server();
                        tempS.ID = j + 1;

                        int LoopCounter = i + 1;
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
                            tempS.TimeDistribution.Add(tempTAP);

                            LoopCounter++;
                        }
                        simulationSystem.Servers.Add(tempS);
                    }
                }

                

            }
        }
    }
}
