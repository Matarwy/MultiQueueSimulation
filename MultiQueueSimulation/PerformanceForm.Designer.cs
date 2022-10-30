namespace MultiQueueSimulation
{
    partial class PerformanceForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.AWTtextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MQLtextbox = new System.Windows.Forms.TextBox();
            this.WPtextbox = new System.Windows.Forms.TextBox();
            this.Okbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(112, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Average Waiting Time:";
            // 
            // AWTtextbox
            // 
            this.AWTtextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.AWTtextbox.Location = new System.Drawing.Point(401, 62);
            this.AWTtextbox.Multiline = true;
            this.AWTtextbox.Name = "AWTtextbox";
            this.AWTtextbox.ReadOnly = true;
            this.AWTtextbox.Size = new System.Drawing.Size(111, 30);
            this.AWTtextbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(150, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "Max Queue Length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(148, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 35);
            this.label3.TabIndex = 3;
            this.label3.Text = "Waiting Probability:";
            // 
            // MQLtextbox
            // 
            this.MQLtextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.MQLtextbox.Location = new System.Drawing.Point(401, 177);
            this.MQLtextbox.Multiline = true;
            this.MQLtextbox.Name = "MQLtextbox";
            this.MQLtextbox.ReadOnly = true;
            this.MQLtextbox.Size = new System.Drawing.Size(111, 30);
            this.MQLtextbox.TabIndex = 4;
            // 
            // WPtextbox
            // 
            this.WPtextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.WPtextbox.Location = new System.Drawing.Point(401, 294);
            this.WPtextbox.Multiline = true;
            this.WPtextbox.Name = "WPtextbox";
            this.WPtextbox.ReadOnly = true;
            this.WPtextbox.Size = new System.Drawing.Size(111, 30);
            this.WPtextbox.TabIndex = 5;
            // 
            // Okbtn
            // 
            this.Okbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Okbtn.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Okbtn.Location = new System.Drawing.Point(250, 391);
            this.Okbtn.Name = "Okbtn";
            this.Okbtn.Size = new System.Drawing.Size(125, 47);
            this.Okbtn.TabIndex = 6;
            this.Okbtn.Text = "OK";
            this.Okbtn.UseVisualStyleBackColor = false;
            this.Okbtn.Click += new System.EventHandler(this.Okbtn_Click);
            // 
            // PerformanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 450);
            this.Controls.Add(this.Okbtn);
            this.Controls.Add(this.WPtextbox);
            this.Controls.Add(this.MQLtextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AWTtextbox);
            this.Controls.Add(this.label1);
            this.Name = "PerformanceForm";
            this.Text = "System Performance";
            this.Load += new System.EventHandler(this.PerformanceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AWTtextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MQLtextbox;
        private System.Windows.Forms.TextBox WPtextbox;
        private System.Windows.Forms.Button Okbtn;
    }
}