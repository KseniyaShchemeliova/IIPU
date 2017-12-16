namespace CDBurn
{
    partial class ProgressWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressWindow));
            this.bOK = new System.Windows.Forms.Button();
            this.lProgress = new System.Windows.Forms.Label();
            this.prbarProgress = new System.Windows.Forms.ProgressBar();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(197, 137);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(141, 23);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "ok";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // lProgress
            // 
            this.lProgress.Location = new System.Drawing.Point(13, 87);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(137, 23);
            this.lProgress.TabIndex = 1;
            // 
            // prbarProgress
            // 
            this.prbarProgress.Location = new System.Drawing.Point(16, 45);
            this.prbarProgress.Name = "prbarProgress";
            this.prbarProgress.Size = new System.Drawing.Size(487, 23);
            this.prbarProgress.TabIndex = 2;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.ShowWindow);
            this.notifyIcon.DoubleClick += new System.EventHandler(this.ShowWindow);
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 172);
            this.Controls.Add(this.prbarProgress);
            this.Controls.Add(this.lProgress);
            this.Controls.Add(this.bOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProgressWindow";
            this.Text = "CDBurn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressWindow_FormClosing);
            this.Resize += new System.EventHandler(this.ProgressWindow_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label lProgress;
        private System.Windows.Forms.ProgressBar prbarProgress;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}