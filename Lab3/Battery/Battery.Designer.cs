namespace Battery
{
    partial class Battery
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
            this.components = new System.ComponentModel.Container();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.timeoutBox = new System.Windows.Forms.ComboBox();
            this.timeoutLabel = new System.Windows.Forms.Label();
            this.timeLeft = new System.Windows.Forms.TextBox();
            this.Percentage = new System.Windows.Forms.TextBox();
            this.State = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timeoutSeconds = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timeoutBox
            // 
            this.timeoutBox.DisplayMember = "300";
            this.timeoutBox.FormattingEnabled = true;
            this.timeoutBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "45",
            "60",
            "120",
            "180",
            "240",
            "300"});
            this.timeoutBox.Location = new System.Drawing.Point(10, 156);
            this.timeoutBox.Name = "timeoutBox";
            this.timeoutBox.Size = new System.Drawing.Size(211, 21);
            this.timeoutBox.TabIndex = 30;
            this.timeoutBox.SelectionChangeCommitted += new System.EventHandler(this.timeoutBox_ValueChanged);
            // 
            // timeoutLabel
            // 
            this.timeoutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.timeoutLabel.AutoSize = true;
            this.timeoutLabel.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeoutLabel.ForeColor = System.Drawing.Color.Green;
            this.timeoutLabel.Location = new System.Drawing.Point(8, 138);
            this.timeoutLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeoutLabel.Name = "timeoutLabel";
            this.timeoutLabel.Size = new System.Drawing.Size(162, 15);
            this.timeoutLabel.TabIndex = 29;
            this.timeoutLabel.Text = "Время отключения дисплея";
            // 
            // timeLeft
            // 
            this.timeLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLeft.Location = new System.Drawing.Point(11, 114);
            this.timeLeft.Margin = new System.Windows.Forms.Padding(2);
            this.timeLeft.Name = "timeLeft";
            this.timeLeft.ReadOnly = true;
            this.timeLeft.Size = new System.Drawing.Size(211, 20);
            this.timeLeft.TabIndex = 24;
            // 
            // Percentage
            // 
            this.Percentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Percentage.Location = new System.Drawing.Point(11, 75);
            this.Percentage.Margin = new System.Windows.Forms.Padding(2);
            this.Percentage.Name = "Percentage";
            this.Percentage.ReadOnly = true;
            this.Percentage.Size = new System.Drawing.Size(211, 20);
            this.Percentage.TabIndex = 25;
            // 
            // State
            // 
            this.State.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.State.Location = new System.Drawing.Point(11, 38);
            this.State.Margin = new System.Windows.Forms.Padding(2);
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.Size = new System.Drawing.Size(211, 20);
            this.State.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(8, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Процент батареи:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(8, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Оставшееся время";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Статус зарядки:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(11, 180);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 15);
            this.label4.TabIndex = 31;
            this.label4.Text = "Время в секундах";
            // 
            // timeoutSeconds
            // 
            this.timeoutSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.timeoutSeconds.Location = new System.Drawing.Point(10, 197);
            this.timeoutSeconds.Margin = new System.Windows.Forms.Padding(2);
            this.timeoutSeconds.Name = "timeoutSeconds";
            this.timeoutSeconds.ReadOnly = true;
            this.timeoutSeconds.Size = new System.Drawing.Size(211, 20);
            this.timeoutSeconds.TabIndex = 32;
            this.timeoutSeconds.TextChanged += new System.EventHandler(this.timeoutSeconds_ValueChanged);
            // 
            // Battery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(233, 227);
            this.Controls.Add(this.timeoutSeconds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.timeoutBox);
            this.Controls.Add(this.timeoutLabel);
            this.Controls.Add(this.timeLeft);
            this.Controls.Add(this.Percentage);
            this.Controls.Add(this.State);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Battery";
            this.Text = "Power Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppClosing);
            this.Load += new System.EventHandler(this.BatteryLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.ComboBox timeoutBox;
        private System.Windows.Forms.Label timeoutLabel;
        private System.Windows.Forms.TextBox timeLeft;
        private System.Windows.Forms.TextBox Percentage;
        private System.Windows.Forms.TextBox State;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox timeoutSeconds;
    }
}

