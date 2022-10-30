using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiQueueSimulation
{
    public partial class PerformanceForm : Form
    {
        public PerformanceForm()
        {
            InitializeComponent();
        }

        private void PerformanceForm_Load(object sender, EventArgs e)
        {
            if (FrontForm.sysmethods.simulationSystem.SimulationTable.Count > 0)
            {
                AWTtextbox.Text = FrontForm.sysmethods.simulationSystem.PerformanceMeasures.AverageWaitingTime.ToString();
                MQLtextbox.Text = FrontForm.sysmethods.simulationSystem.PerformanceMeasures.MaxQueueLength.ToString();
                WPtextbox.Text = FrontForm.sysmethods.simulationSystem.PerformanceMeasures.WaitingProbability.ToString();
            }
            else
            {
                AWTtextbox.Text = "No Data";
                MQLtextbox.Text = "No Data";
                WPtextbox.Text = "No Data";
            }
        }
        private void Okbtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
