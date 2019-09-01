namespace Endeavour
{
    partial class EVF
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar_BS = new System.Windows.Forms.ProgressBar();
            this.checkBox_upd = new System.Windows.Forms.CheckBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label_TF = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_LuckSteps = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_Args = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_calculated = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_avgtime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_stopcriterion = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_historyLength = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_timestart = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label_argsqty = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_avgtime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label_calculated);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label_Args);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label_LuckSteps);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label_TF);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.progressBar_BS);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Reaching  \"stop criterion\" at the current point";
            // 
            // progressBar_BS
            // 
            this.progressBar_BS.Location = new System.Drawing.Point(6, 32);
            this.progressBar_BS.Name = "progressBar_BS";
            this.progressBar_BS.Size = new System.Drawing.Size(237, 10);
            this.progressBar_BS.TabIndex = 0;
            // 
            // checkBox_upd
            // 
            this.checkBox_upd.AutoSize = true;
            this.checkBox_upd.Location = new System.Drawing.Point(6, 256);
            this.checkBox_upd.Name = "checkBox_upd";
            this.checkBox_upd.Size = new System.Drawing.Size(87, 17);
            this.checkBox_upd.TabIndex = 1;
            this.checkBox_upd.Text = "Don\'t update";
            this.checkBox_upd.UseVisualStyleBackColor = true;
            this.checkBox_upd.CheckedChanged += new System.EventHandler(this.checkBox_upd_CheckedChanged);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(168, 256);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Function best value:";
            // 
            // label_TF
            // 
            this.label_TF.AutoSize = true;
            this.label_TF.Location = new System.Drawing.Point(115, 60);
            this.label_TF.Name = "label_TF";
            this.label_TF.Size = new System.Drawing.Size(40, 13);
            this.label_TF.TabIndex = 5;
            this.label_TF.Text = "waiting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Luck steps quantity:";
            // 
            // label_LuckSteps
            // 
            this.label_LuckSteps.AutoSize = true;
            this.label_LuckSteps.Location = new System.Drawing.Point(115, 81);
            this.label_LuckSteps.Name = "label_LuckSteps";
            this.label_LuckSteps.Size = new System.Drawing.Size(40, 13);
            this.label_LuckSteps.TabIndex = 7;
            this.label_LuckSteps.Text = "waiting";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Arguments on border:";
            // 
            // label_Args
            // 
            this.label_Args.AutoSize = true;
            this.label_Args.Location = new System.Drawing.Point(115, 102);
            this.label_Args.Name = "label_Args";
            this.label_Args.Size = new System.Drawing.Size(40, 13);
            this.label_Args.TabIndex = 9;
            this.label_Args.Text = "waiting";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Function calculated:";
            // 
            // label_calculated
            // 
            this.label_calculated.AutoSize = true;
            this.label_calculated.Location = new System.Drawing.Point(115, 123);
            this.label_calculated.Name = "label_calculated";
            this.label_calculated.Size = new System.Drawing.Size(40, 13);
            this.label_calculated.TabIndex = 11;
            this.label_calculated.Text = "waiting";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Calculation time, sec:";
            // 
            // label_avgtime
            // 
            this.label_avgtime.AutoSize = true;
            this.label_avgtime.Location = new System.Drawing.Point(115, 144);
            this.label_avgtime.Name = "label_avgtime";
            this.label_avgtime.Size = new System.Drawing.Size(40, 13);
            this.label_avgtime.TabIndex = 13;
            this.label_avgtime.Text = "waiting";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Stop criterion:";
            // 
            // label_stopcriterion
            // 
            this.label_stopcriterion.AutoSize = true;
            this.label_stopcriterion.Location = new System.Drawing.Point(115, 210);
            this.label_stopcriterion.Name = "label_stopcriterion";
            this.label_stopcriterion.Size = new System.Drawing.Size(40, 13);
            this.label_stopcriterion.TabIndex = 4;
            this.label_stopcriterion.Text = "waiting";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "History length:";
            // 
            // label_historyLength
            // 
            this.label_historyLength.AutoSize = true;
            this.label_historyLength.Location = new System.Drawing.Point(116, 228);
            this.label_historyLength.Name = "label_historyLength";
            this.label_historyLength.Size = new System.Drawing.Size(40, 13);
            this.label_historyLength.TabIndex = 6;
            this.label_historyLength.Text = "waiting";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Process started:";
            // 
            // label_timestart
            // 
            this.label_timestart.AutoSize = true;
            this.label_timestart.Location = new System.Drawing.Point(114, 174);
            this.label_timestart.Name = "label_timestart";
            this.label_timestart.Size = new System.Drawing.Size(40, 13);
            this.label_timestart.TabIndex = 8;
            this.label_timestart.Text = "waiting";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Arguments qty:";
            // 
            // label_argsqty
            // 
            this.label_argsqty.AutoSize = true;
            this.label_argsqty.Location = new System.Drawing.Point(115, 192);
            this.label_argsqty.Name = "label_argsqty";
            this.label_argsqty.Size = new System.Drawing.Size(40, 13);
            this.label_argsqty.TabIndex = 10;
            this.label_argsqty.Text = "waiting";
            // 
            // EVF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(250, 285);
            this.Controls.Add(this.label_argsqty);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label_timestart);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label_historyLength);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label_stopcriterion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.checkBox_upd);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EVF";
            this.Text = "Search process status";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EVF_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar_BS;
        private System.Windows.Forms.CheckBox checkBox_upd;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_TF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_LuckSteps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_Args;
        private System.Windows.Forms.Label label_calculated;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_avgtime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_stopcriterion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_historyLength;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_timestart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label_argsqty;
    }
}