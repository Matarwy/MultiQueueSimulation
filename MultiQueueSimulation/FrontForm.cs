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
        public FrontForm()
        {
            InitializeComponent();
            sysmethods = new SimulationSystemMethods();
            FilePath = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
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

            simulationGridView.DataSource = dt;
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
            Server a = new Server();
            Server b = new Server();
            sysmethods.simulationSystem.Servers.Add(a);
            sysmethods.simulationSystem.Servers.Add(b);
            for (int i=0;i< sysmethods.simulationSystem.Servers.Count; i++)
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
    }
}
