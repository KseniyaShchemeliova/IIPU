namespace CDBurn
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.gbFiles = new System.Windows.Forms.GroupBox();
            this.gbDisk = new System.Windows.Forms.GroupBox();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btBurn = new System.Windows.Forms.Button();
            this.btRemove = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.progressbarSize = new System.Windows.Forms.ProgressBar();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.lSize = new System.Windows.Forms.Label();
            this.lType = new System.Windows.Forms.Label();
            this.tbSize = new System.Windows.Forms.TextBox();
            this.tbType = new System.Windows.Forms.TextBox();
            this.cbRecorders = new System.Windows.Forms.ComboBox();
            this.gbFiles.SuspendLayout();
            this.gbDisk.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "CDBurn";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.ShowWindow);
            this.notifyIcon.DoubleClick += new System.EventHandler(this.ShowWindow);
            // 
            // gbFiles
            // 
            this.gbFiles.Controls.Add(this.lbFiles);
            this.gbFiles.Controls.Add(this.progressbarSize);
            this.gbFiles.Controls.Add(this.btAdd);
            this.gbFiles.Controls.Add(this.btRemove);
            this.gbFiles.Controls.Add(this.btBurn);
            this.gbFiles.Location = new System.Drawing.Point(256, 27);
            this.gbFiles.Name = "gbFiles";
            this.gbFiles.Size = new System.Drawing.Size(406, 337);
            this.gbFiles.TabIndex = 0;
            this.gbFiles.TabStop = false;
            this.gbFiles.Text = "Files";
            // 
            // gbDisk
            // 
            this.gbDisk.Controls.Add(this.cbRecorders);
            this.gbDisk.Controls.Add(this.tbType);
            this.gbDisk.Controls.Add(this.tbSize);
            this.gbDisk.Controls.Add(this.lType);
            this.gbDisk.Controls.Add(this.lSize);
            this.gbDisk.Controls.Add(this.btUpdate);
            this.gbDisk.Location = new System.Drawing.Point(17, 27);
            this.gbDisk.Name = "gbDisk";
            this.gbDisk.Size = new System.Drawing.Size(224, 336);
            this.gbDisk.TabIndex = 1;
            this.gbDisk.TabStop = false;
            this.gbDisk.Text = "Disc";
            // 
            // btUpdate
            // 
            this.btUpdate.Location = new System.Drawing.Point(27, 303);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(163, 23);
            this.btUpdate.TabIndex = 0;
            this.btUpdate.Text = "Update";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btBurn
            // 
            this.btBurn.Location = new System.Drawing.Point(31, 307);
            this.btBurn.Name = "btBurn";
            this.btBurn.Size = new System.Drawing.Size(353, 23);
            this.btBurn.TabIndex = 0;
            this.btBurn.Text = "Burn";
            this.btBurn.UseVisualStyleBackColor = true;
            this.btBurn.Click += new System.EventHandler(this.bBurn_Click);
            // 
            // btRemove
            // 
            this.btRemove.Location = new System.Drawing.Point(31, 278);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(353, 23);
            this.btRemove.TabIndex = 1;
            this.btRemove.Text = "Remove File";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(31, 249);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(353, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "Add File";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // progressbarSize
            // 
            this.progressbarSize.Location = new System.Drawing.Point(31, 220);
            this.progressbarSize.Name = "progressbarSize";
            this.progressbarSize.Size = new System.Drawing.Size(353, 23);
            this.progressbarSize.TabIndex = 3;
            // 
            // lbFiles
            // 
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.ItemHeight = 16;
            this.lbFiles.Location = new System.Drawing.Point(31, 31);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(353, 180);
            this.lbFiles.TabIndex = 4;
            this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            // 
            // lSize
            // 
            this.lSize.AutoSize = true;
            this.lSize.Location = new System.Drawing.Point(7, 245);
            this.lSize.Name = "lSize";
            this.lSize.Size = new System.Drawing.Size(35, 17);
            this.lSize.TabIndex = 1;
            this.lSize.Text = "Size";
            // 
            // lType
            // 
            this.lType.AutoSize = true;
            this.lType.Location = new System.Drawing.Point(7, 210);
            this.lType.Name = "lType";
            this.lType.Size = new System.Drawing.Size(40, 17);
            this.lType.TabIndex = 2;
            this.lType.Text = "Type";
            // 
            // tbSize
            // 
            this.tbSize.Location = new System.Drawing.Point(67, 245);
            this.tbSize.Name = "tbSize";
            this.tbSize.ReadOnly = true;
            this.tbSize.Size = new System.Drawing.Size(151, 22);
            this.tbSize.TabIndex = 3;
            // 
            // tbType
            // 
            this.tbType.Location = new System.Drawing.Point(67, 210);
            this.tbType.Name = "tbType";
            this.tbType.ReadOnly = true;
            this.tbType.Size = new System.Drawing.Size(151, 22);
            this.tbType.TabIndex = 4;
            // 
            // cbRecorders
            // 
            this.cbRecorders.FormattingEnabled = true;
            this.cbRecorders.Location = new System.Drawing.Point(10, 46);
            this.cbRecorders.Name = "cbRecorders";
            this.cbRecorders.Size = new System.Drawing.Size(208, 24);
            this.cbRecorders.TabIndex = 5;
            this.cbRecorders.SelectedIndexChanged += new System.EventHandler(this.cbRecorders_SelectedIndexChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 376);
            this.Controls.Add(this.gbDisk);
            this.Controls.Add(this.gbFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "CDBurn";
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.gbFiles.ResumeLayout(false);
            this.gbDisk.ResumeLayout(false);
            this.gbDisk.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.GroupBox gbFiles;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.ProgressBar progressbarSize;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btBurn;
        private System.Windows.Forms.GroupBox gbDisk;
        private System.Windows.Forms.ComboBox cbRecorders;
        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.TextBox tbSize;
        private System.Windows.Forms.Label lType;
        private System.Windows.Forms.Label lSize;
        private System.Windows.Forms.Button btUpdate;
    }
}

