namespace Hooks
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
            this.HideCheckBox = new System.Windows.Forms.CheckBox();
            this.FileSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.HideButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FileSizeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // HideCheckBox
            // 
            this.HideCheckBox.AutoSize = true;
            this.HideCheckBox.Location = new System.Drawing.Point(29, 40);
            this.HideCheckBox.Name = "HideCheckBox";
            this.HideCheckBox.Size = new System.Drawing.Size(242, 21);
            this.HideCheckBox.TabIndex = 0;
            this.HideCheckBox.Text = "Скрыть при следующем запуске";
            this.HideCheckBox.UseVisualStyleBackColor = true;
            // 
            // FileSizeNumericUpDown
            // 
            this.FileSizeNumericUpDown.Location = new System.Drawing.Point(29, 86);
            this.FileSizeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FileSizeNumericUpDown.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.FileSizeNumericUpDown.Name = "FileSizeNumericUpDown";
            this.FileSizeNumericUpDown.Size = new System.Drawing.Size(120, 22);
            this.FileSizeNumericUpDown.TabIndex = 1;
            this.FileSizeNumericUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.Location = new System.Drawing.Point(29, 132);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(166, 22);
            this.EmailTextBox.TabIndex = 2;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(81, 209);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(88, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // HideButton
            // 
            this.HideButton.Location = new System.Drawing.Point(195, 209);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(75, 23);
            this.HideButton.TabIndex = 4;
            this.HideButton.Text = "Скрыть";
            this.HideButton.UseVisualStyleBackColor = true;
            this.HideButton.Click += new System.EventHandler(this.HideButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 268);
            this.Controls.Add(this.HideButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.EmailTextBox);
            this.Controls.Add(this.FileSizeNumericUpDown);
            this.Controls.Add(this.HideCheckBox);
            this.Name = "MainWindow";
            this.Text = "Hooks";
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.FileSizeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox HideCheckBox;
        private System.Windows.Forms.NumericUpDown FileSizeNumericUpDown;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button HideButton;
    }
}

