namespace MultiQueueSimulation
{
    partial class ServerChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.IdleProbabilitylbl = new System.Windows.Forms.Label();
            this.Utilizationlbl = new System.Windows.Forms.Label();
            this.AverageServiceTimelbl = new System.Windows.Forms.Label();
            this.IDtextbox = new System.Windows.Forms.TextBox();
            this.UtiTextBox = new System.Windows.Forms.TextBox();
            this.ASTextBox = new System.Windows.Forms.TextBox();
            this.ServerBeasyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ServerIdtextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ServerBeasyChart)).BeginInit();
            this.SuspendLayout();
            // 
            // IdleProbabilitylbl
            // 
            this.IdleProbabilitylbl.AutoSize = true;
            this.IdleProbabilitylbl.Font = new System.Drawing.Font("Arial Narrow", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdleProbabilitylbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.IdleProbabilitylbl.Location = new System.Drawing.Point(65, 9);
            this.IdleProbabilitylbl.Name = "IdleProbabilitylbl";
            this.IdleProbabilitylbl.Size = new System.Drawing.Size(120, 22);
            this.IdleProbabilitylbl.TabIndex = 1;
            this.IdleProbabilitylbl.Text = "Idle Probability:";
            this.IdleProbabilitylbl.Click += new System.EventHandler(this.label2_Click);
            // 
            // Utilizationlbl
            // 
            this.Utilizationlbl.AutoSize = true;
            this.Utilizationlbl.Font = new System.Drawing.Font("Arial Narrow", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Utilizationlbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Utilizationlbl.Location = new System.Drawing.Point(100, 70);
            this.Utilizationlbl.Name = "Utilizationlbl";
            this.Utilizationlbl.Size = new System.Drawing.Size(85, 22);
            this.Utilizationlbl.TabIndex = 2;
            this.Utilizationlbl.Text = "Utilization:";
            // 
            // AverageServiceTimelbl
            // 
            this.AverageServiceTimelbl.AutoSize = true;
            this.AverageServiceTimelbl.Font = new System.Drawing.Font("Arial Narrow", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageServiceTimelbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.AverageServiceTimelbl.Location = new System.Drawing.Point(21, 42);
            this.AverageServiceTimelbl.Name = "AverageServiceTimelbl";
            this.AverageServiceTimelbl.Size = new System.Drawing.Size(164, 22);
            this.AverageServiceTimelbl.TabIndex = 3;
            this.AverageServiceTimelbl.Text = "Average Service Time:";
            // 
            // IDtextbox
            // 
            this.IDtextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.IDtextbox.Location = new System.Drawing.Point(215, 16);
            this.IDtextbox.Multiline = true;
            this.IDtextbox.Name = "IDtextbox";
            this.IDtextbox.ReadOnly = true;
            this.IDtextbox.Size = new System.Drawing.Size(97, 22);
            this.IDtextbox.TabIndex = 4;
            // 
            // UtiTextBox
            // 
            this.UtiTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.UtiTextBox.Location = new System.Drawing.Point(215, 72);
            this.UtiTextBox.Multiline = true;
            this.UtiTextBox.Name = "UtiTextBox";
            this.UtiTextBox.ReadOnly = true;
            this.UtiTextBox.Size = new System.Drawing.Size(97, 22);
            this.UtiTextBox.TabIndex = 5;
            // 
            // ASTextBox
            // 
            this.ASTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ASTextBox.Location = new System.Drawing.Point(215, 44);
            this.ASTextBox.Multiline = true;
            this.ASTextBox.Name = "ASTextBox";
            this.ASTextBox.ReadOnly = true;
            this.ASTextBox.Size = new System.Drawing.Size(97, 22);
            this.ASTextBox.TabIndex = 6;
            // 
            // ServerBeasyChart
            // 
            chartArea1.Name = "ChartArea1";
            this.ServerBeasyChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ServerBeasyChart.Legends.Add(legend1);
            this.ServerBeasyChart.Location = new System.Drawing.Point(25, 127);
            this.ServerBeasyChart.Name = "ServerBeasyChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ServerBeasyChart.Series.Add(series1);
            this.ServerBeasyChart.Size = new System.Drawing.Size(734, 266);
            this.ServerBeasyChart.TabIndex = 7;
            this.ServerBeasyChart.Text = "ServerBeasyShart";
            // 
            // ServerIdtextbox
            // 
            this.ServerIdtextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ServerIdtextbox.Location = new System.Drawing.Point(520, 42);
            this.ServerIdtextbox.Multiline = true;
            this.ServerIdtextbox.Name = "ServerIdtextbox";
            this.ServerIdtextbox.ReadOnly = true;
            this.ServerIdtextbox.Size = new System.Drawing.Size(53, 22);
            this.ServerIdtextbox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(435, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "Server ID:";
            // 
            // ServerChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ServerIdtextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerBeasyChart);
            this.Controls.Add(this.ASTextBox);
            this.Controls.Add(this.UtiTextBox);
            this.Controls.Add(this.IDtextbox);
            this.Controls.Add(this.AverageServiceTimelbl);
            this.Controls.Add(this.Utilizationlbl);
            this.Controls.Add(this.IdleProbabilitylbl);
            this.Name = "ServerChartForm";
            this.Text = "ServerChartForm";
            this.Load += new System.EventHandler(this.ServerChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ServerBeasyChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IdleProbabilitylbl;
        private System.Windows.Forms.Label Utilizationlbl;
        private System.Windows.Forms.Label AverageServiceTimelbl;
        private System.Windows.Forms.TextBox IDtextbox;
        private System.Windows.Forms.TextBox UtiTextBox;
        private System.Windows.Forms.TextBox ASTextBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart ServerBeasyChart;
        private System.Windows.Forms.TextBox ServerIdtextbox;
        private System.Windows.Forms.Label label1;
    }
}