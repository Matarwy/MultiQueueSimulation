namespace MultiQueueSimulation
{
    partial class LoadDataFileForm
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
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.FilePathLabel = new System.Windows.Forms.Label();
            this.LoadFilebtn = new System.Windows.Forms.Button();
            this.DoneLoadingbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.FilePathTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.FilePathTextBox.Location = new System.Drawing.Point(95, 25);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(695, 22);
            this.FilePathTextBox.TabIndex = 0;
            this.FilePathTextBox.TextChanged += new System.EventHandler(this.FilePathTextBox_TextChanged);
            // 
            // FilePathLabel
            // 
            this.FilePathLabel.AutoSize = true;
            this.FilePathLabel.Location = new System.Drawing.Point(27, 25);
            this.FilePathLabel.Name = "FilePathLabel";
            this.FilePathLabel.Size = new System.Drawing.Size(62, 16);
            this.FilePathLabel.TabIndex = 1;
            this.FilePathLabel.Text = "File Path:";
            // 
            // LoadFilebtn
            // 
            this.LoadFilebtn.BackColor = System.Drawing.Color.Fuchsia;
            this.LoadFilebtn.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadFilebtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.LoadFilebtn.Location = new System.Drawing.Point(249, 70);
            this.LoadFilebtn.Name = "LoadFilebtn";
            this.LoadFilebtn.Size = new System.Drawing.Size(242, 39);
            this.LoadFilebtn.TabIndex = 2;
            this.LoadFilebtn.Text = "Load .txt File";
            this.LoadFilebtn.UseVisualStyleBackColor = false;
            this.LoadFilebtn.Click += new System.EventHandler(this.LoadFilebtn_Click);
            // 
            // DoneLoadingbtn
            // 
            this.DoneLoadingbtn.BackColor = System.Drawing.Color.Fuchsia;
            this.DoneLoadingbtn.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoneLoadingbtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.DoneLoadingbtn.Location = new System.Drawing.Point(671, 70);
            this.DoneLoadingbtn.Name = "DoneLoadingbtn";
            this.DoneLoadingbtn.Size = new System.Drawing.Size(102, 39);
            this.DoneLoadingbtn.TabIndex = 3;
            this.DoneLoadingbtn.Text = "Done";
            this.DoneLoadingbtn.UseVisualStyleBackColor = false;
            this.DoneLoadingbtn.Click += new System.EventHandler(this.DoneLoadingbtn_Click);
            // 
            // LoadDataFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 121);
            this.Controls.Add(this.DoneLoadingbtn);
            this.Controls.Add(this.LoadFilebtn);
            this.Controls.Add(this.FilePathLabel);
            this.Controls.Add(this.FilePathTextBox);
            this.Name = "LoadDataFileForm";
            this.Text = "Load Data File";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadDataFileForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.Label FilePathLabel;
        private System.Windows.Forms.Button LoadFilebtn;
        private System.Windows.Forms.Button DoneLoadingbtn;
    }
}