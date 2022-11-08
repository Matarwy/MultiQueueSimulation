using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MultiQueueModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MultiQueueSimulation
{
    public partial class ServerChartForm : Form
    {
        Server server;
        public ServerChartForm(Server _server)
        {
            InitializeComponent();
            this.server = _server;
        }

        private void ServerChartForm_Load(object sender, EventArgs e)
        {
            if (FrontForm.sysmethods.simulationSystem.SimulationTable.Count > 0)
            {
                IDtextbox.Text = server.IdleProbability.ToString();
                ASTextBox.Text = server.AverageServiceTime.ToString();
                UtiTextBox.Text = server.Utilization.ToString();
                ServerIdtextbox.Text = server.ID.ToString();
            }
            else
            {
                IDtextbox.Text = "No Data";
                ASTextBox.Text = "No Data";
                UtiTextBox.Text = "No Data";
                ServerIdtextbox.Text = "No Data";
            }

            DataSet ds = new DataSet();
            ds.Tables.Add("ServerBeasy");
            ds.Tables[0].Columns.Add("Time");
            ds.Tables[0].Columns.Add("Beasy");
            for(int i = 0; i < FrontForm.sysmethods.simulationSystem.SimulationTable.Count; i++)
            {
                if (FrontForm.sysmethods.simulationSystem.SimulationTable[i].AssignedServer.ID == server.ID) {
                    if(FrontForm.sysmethods.simulationSystem.SimulationTable[i].ArrivalTime > FrontForm.sysmethods.simulationSystem.SimulationTable[i].AssignedServer.FinishTime)
                    {
                        ds.Tables[0].Rows.Add(i,0);
                    }
                    else
                    {
                        ds.Tables[0].Rows.Add(i, 1);
                    }
                }
                else
                    ds.Tables[0].Rows.Add(i, 0);

            }
            ServerBeasyChart.Series[0].XValueMember = "Time";
            ServerBeasyChart.Series[0].YValueMembers = "Beasy";
            DataView source = new DataView(ds.Tables[0]);
            ServerBeasyChart.DataSource = source;
            ServerBeasyChart.DataBind();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
