using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class Server
    {
        public Server()
        {
            this.TimeDistribution = new List<TimeDistribution>();
            this.TotalWorkingTime = 0;
            this.numberOfCustomers = 0;
        }

        public int ID { get; set; }
        public decimal IdleProbability { get; set; }
        public decimal AverageServiceTime { get; set; } 
        public decimal Utilization { get; set; }

        public List<TimeDistribution> TimeDistribution;

        //optional if needed use them
        public int FinishTime { get; set; }
        public int TotalWorkingTime { get; set; }
        public int numberOfCustomers { get; set; }

        public void calculateMeasures(int simulation_time)
        {
            try
            {
                this.IdleProbability = (decimal)((decimal)((decimal)simulation_time - (decimal)this.TotalWorkingTime) / (decimal)simulation_time);
                if (this.numberOfCustomers == 0)
                    this.AverageServiceTime = 0;
                else
                    this.AverageServiceTime = Convert.ToDecimal(this.TotalWorkingTime) / Convert.ToDecimal(this.numberOfCustomers);
                this.Utilization = Convert.ToDecimal(this.TotalWorkingTime) / Convert.ToDecimal(simulation_time);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
