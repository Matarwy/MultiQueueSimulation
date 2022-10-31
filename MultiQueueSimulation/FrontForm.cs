using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using MultiQueueTesting;

namespace MultiQueueSimulation
{
    public partial class FrontForm : Form
    {
        public static SimulationSystemMethods sysmethods;
        public static string FilePath { get; set; }
        DataTable dt;

        public FrontForm()
        {
            InitializeComponent();
            sysmethods = new SimulationSystemMethods();
            dt = new DataTable();
            FilePath = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Customer Number");
            dt.Columns.Add("Random Interarrival");
            dt.Columns.Add("Inter Arrival");
            dt.Columns.Add("Arrival Time");
            dt.Columns.Add("Random Service");
            dt.Columns.Add("Assigned Server");
            dt.Columns.Add("Start time");
            dt.Columns.Add("Service Time");
            dt.Columns.Add("End Time");
            dt.Columns.Add("Time in Queue");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadDataFilebtn_Click(object sender, EventArgs e)
        {
            LoadDataFileForm ldff = new LoadDataFileForm();
            ldff.Show();
        }

        private void serversChartsbtn_Click(object sender, EventArgs e)
        {
            for (int i=0;i< sysmethods.simulationSystem.NumberOfServers; i++)
            {
                ServerChartForm serverchart = new ServerChartForm();
                serverchart.Show();
            }
        }

        private void sysPerformancebtn_Click(object sender, EventArgs e)
        {
            PerformanceForm spf = new PerformanceForm();
            spf.Show();
        }

        private void Simulationbtn_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();

            if (FilePath != null)
            {
                sysmethods.ReadFile(FilePath);
                sysmethods.Simulateion();
            }

            for (int i = 0; i < sysmethods.simulationSystem.SimulationTable.Count; i++)
            {
                dt.Rows.Add(
                    sysmethods.simulationSystem.SimulationTable[i].CustomerNumber,
                    sysmethods.simulationSystem.SimulationTable[i].RandomInterArrival,
                    sysmethods.simulationSystem.SimulationTable[i].InterArrival,
                    sysmethods.simulationSystem.SimulationTable[i].ArrivalTime,
                    sysmethods.simulationSystem.SimulationTable[i].RandomService,
                    sysmethods.simulationSystem.SimulationTable[i].AssignedServer.ID,
                    sysmethods.simulationSystem.SimulationTable[i].StartTime,
                    sysmethods.simulationSystem.SimulationTable[i].ServiceTime,
                    sysmethods.simulationSystem.SimulationTable[i].EndTime,
                    sysmethods.simulationSystem.SimulationTable[i].TimeInQueue);
            }

            simulationGridView.DataSource = dt;
        }
    }
}
