namespace MultiQueueSimulation
{
    partial class FrontForm
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
            this.simulationGridView = new System.Windows.Forms.DataGridView();
            this.loadDataFilebtn = new System.Windows.Forms.Button();
            this.sysPerformancebtn = new System.Windows.Forms.Button();
            this.serversChartsbtn = new System.Windows.Forms.Button();
            this.Simulationbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.simulationGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // simulationGridView
            // 
            this.simulationGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.simulationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.simulationGridView.Location = new System.Drawing.Point(220, 12);
            this.simulationGridView.Name = "simulationGridView";
            this.simulationGridView.ReadOnly = true;
            this.simulationGridView.RowHeadersWidth = 51;
            this.simulationGridView.RowTemplate.Height = 24;
            this.simulationGridView.Size = new System.Drawing.Size(950, 529);
            this.simulationGridView.TabIndex = 0;
            this.simulationGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // loadDataFilebtn
            // 
            this.loadDataFilebtn.BackColor = System.Drawing.Color.Beige;
            this.loadDataFilebtn.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadDataFilebtn.ForeColor = System.Drawing.Color.Chocolate;
            this.loadDataFilebtn.Location = new System.Drawing.Point(0, 0);
            this.loadDataFilebtn.Name = "loadDataFilebtn";
            this.loadDataFilebtn.Size = new System.Drawing.Size(190, 60);
            this.loadDataFilebtn.TabIndex = 1;
            this.loadDataFilebtn.Text = "Load Data File";
            this.loadDataFilebtn.UseVisualStyleBackColor = false;
            this.loadDataFilebtn.Click += new System.EventHandler(this.loadDataFilebtn_Click);
            // 
            // sysPerformancebtn
            // 
            this.sysPerformancebtn.BackColor = System.Drawing.Color.Beige;
            this.sysPerformancebtn.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sysPerformancebtn.ForeColor = System.Drawing.Color.Chocolate;
            this.sysPerformancebtn.Location = new System.Drawing.Point(0, 100);
            this.sysPerformancebtn.Name = "sysPerformancebtn";
            this.sysPerformancebtn.Size = new System.Drawing.Size(190, 60);
            this.sysPerformancebtn.TabIndex = 3;
            this.sysPerformancebtn.Text = "System Performance";
            this.sysPerformancebtn.UseVisualStyleBackColor = false;
            this.sysPerformancebtn.Click += new System.EventHandler(this.sysPerformancebtn_Click);
            // 
            // serversChartsbtn
            // 
            this.serversChartsbtn.BackColor = System.Drawing.Color.Beige;
            this.serversChartsbtn.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serversChartsbtn.ForeColor = System.Drawing.Color.Chocolate;
            this.serversChartsbtn.Location = new System.Drawing.Point(0, 200);
            this.serversChartsbtn.Name = "serversChartsbtn";
            this.serversChartsbtn.Size = new System.Drawing.Size(190, 60);
            this.serversChartsbtn.TabIndex = 4;
            this.serversChartsbtn.Text = "Servers Charts";
            this.serversChartsbtn.UseVisualStyleBackColor = false;
            this.serversChartsbtn.Click += new System.EventHandler(this.serversChartsbtn_Click);
            // 
            // Simulationbtn
            // 
            this.Simulationbtn.BackColor = System.Drawing.Color.Beige;
            this.Simulationbtn.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Simulationbtn.ForeColor = System.Drawing.Color.Chocolate;
            this.Simulationbtn.Location = new System.Drawing.Point(0, 309);
            this.Simulationbtn.Name = "Simulationbtn";
            this.Simulationbtn.Size = new System.Drawing.Size(190, 60);
            this.Simulationbtn.TabIndex = 5;
            this.Simulationbtn.Text = "Simulation";
            this.Simulationbtn.UseVisualStyleBackColor = false;
            this.Simulationbtn.Click += new System.EventHandler(this.Simulationbtn_Click);
            // 
            // FrontForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 553);
            this.Controls.Add(this.Simulationbtn);
            this.Controls.Add(this.serversChartsbtn);
            this.Controls.Add(this.sysPerformancebtn);
            this.Controls.Add(this.loadDataFilebtn);
            this.Controls.Add(this.simulationGridView);
            this.Name = "FrontForm";
            this.Text = "Front Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.simulationGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView simulationGridView;
        private System.Windows.Forms.Button loadDataFilebtn;
        private System.Windows.Forms.Button sysPerformancebtn;
        private System.Windows.Forms.Button serversChartsbtn;
        private System.Windows.Forms.Button Simulationbtn;
    }
}

