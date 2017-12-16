using System;
using System.Threading;
using System.Windows.Forms;
using IMAPI2;

namespace CDBurn
{
    public partial class ProgressWindow : Form
    {
        private FormatDataWriteAction _writeAction;

        public ProgressWindow(BurnManager burnManager)
        {
            InitializeComponent();
            burnManager.UpdateBurn += UpdateProgressBar;
            new Thread(burnManager.Burn).Start();
        }

        private void ProgressWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_writeAction == FormatDataWriteAction.Completed)
                e.Cancel = false;
            else
            {
                e.Cancel = true;
                MessageBox.Show(@"Дождитесь завершения записи на диск", @"Ошибка");
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProgressWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            notifyIcon.Visible = true;
            ShowInTaskbar = false;
        }

        private void ShowWindow(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            ShowInTaskbar = true;
        }

        private void UpdateProgressBar(FormatWriteUpdateEventArgs e)
        {
            prbarProgress.Value = (e.LastWrittenLba - e.StartLba + 1) * 100 / e.SectorCount;
            UpdateBurnState(e._currentAction);
        }

        private void UpdateBurnState(FormatDataWriteAction action)
        {
            if (action.Equals(_writeAction))
                return;
            _writeAction = action;
            bOK.Enabled = _writeAction == FormatDataWriteAction.Completed;
            lProgress.Text = action.ToString();
            if (WindowState != FormWindowState.Minimized) return;
            notifyIcon.BalloonTipText = action.ToString();
            notifyIcon.ShowBalloonTip(70);
        }
    }
}
