using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IMAPI2;

namespace CDBurn
{
    public partial class MainWindow : Form
    {
        private readonly BurnManager _burnManager = new BurnManager();
        private readonly FileManager _fileManager = new FileManager();
        private const long BytesInMegabyte = 1024 * 1024;

        public MainWindow()
        {
            InitializeComponent();
            UpdateGui();
        }

        private void ShowWindow(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            ShowInTaskbar = true;
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            notifyIcon.Visible = true;
            ShowInTaskbar = false;
        }

        private void cbRecorders_SelectedIndexChanged(object sender, EventArgs e)
        {
            var recorderInfo = cbRecorders.SelectedItem.ToString()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            FillDiscInfo(_burnManager.GetDiscInfo(recorderInfo[0]));
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            btRemove.Enabled = lbFiles.SelectedIndex != -1;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (!CheckDiscIsAviable()) return;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            if (!CheckDiscIsAviable()) return;
            _fileManager.AddNewFiles(openFileDialog.FileNames);
            UpdateFilesBox();
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            _fileManager.RemoveFile(lbFiles.SelectedIndex);
            UpdateFilesBox();
        }

        private void bBurn_Click(object sender, EventArgs e)
        {
            if (!CheckDiscIsAviable()) return;
            _burnManager.SetFilesToBurning(_fileManager.FileToBurningList.ToList());
            Hide();
            var progressWindow = new ProgressWindow(_burnManager);
            progressWindow.FormClosed += ShowW;
            progressWindow.Show();
        }

        private void UpdateGui()
        {
            cbRecorders.Items.Clear();
            cbRecorders.Items.AddRange(DiscRecordersToStringArray(_burnManager.RecordersList));
            tbType.Text = string.Empty;
            tbSize.Text = string.Empty;
            gbFiles.Enabled = false;
            lbFiles.Items.Clear();
            progressbarSize.Value = 0;
        }

        private object[] DiscRecordersToStringArray(List<DiscRecorder> recorders)
        {
            var list = new List<string>();
            foreach (var recorder in recorders)
            {
                list.Add(string.Join(" ", recorder.VolumePath, recorder.RecorderId));
            }
            return list.ToArray();
        }

        private void FillDiscInfo(Disc disc)
        {
            tbType.Text = disc.Type.ToString();
            tbSize.Text = $@"{disc.Size / BytesInMegabyte} MB";
            if (disc.Type == PhysicalMedia.Unknown)
            {
                ShowMessageBox(@"Disk is not aviable");
                gbFiles.Enabled = false;
                return;
            }
            if (disc.State != MediaState.Blank)
            {
                ShowMessageBox(@"Диск уже занят. Перед записью новых данных необходимо удалить старые");
                gbFiles.Enabled = false;
            }
            else
            {
                InitializeFilesBox(disc.Size);
            }
        }

        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message, @"Warning");
        }

        private void InitializeFilesBox(long discSize)
        {
            gbFiles.Enabled = true;
            lbFiles.Items.Clear();
            _fileManager.RemoveFiles();
            _fileManager.DiscSize = discSize;
            progressbarSize.Value = 0;
            progressbarSize.Maximum = (int)(discSize / BytesInMegabyte);
            btAdd.Enabled = true;
            btRemove.Enabled = false;
            btBurn.Enabled = false;
        }

        private bool CheckDiscIsAviable()
        {
            if (_burnManager.DiskIsAviable()) return true;
            ShowMessageBox(@"Диск не доступен");
            return false;
        }
        
        private void UpdateFilesBox()
        {
            lbFiles.Items.Clear();
            lbFiles.Items.AddRange(FileListToStringArray(_fileManager.FileToBurningList));
            progressbarSize.Value = (int)(_fileManager.FilesSize / BytesInMegabyte);
            btBurn.Enabled = _fileManager.FilesSize > 0;
            btRemove.Enabled = false;
        }

        private object[] FileListToStringArray(IReadOnlyCollection<FileNode> fileList)
        {
            var list = new List<string>();
            foreach (var file in fileList)
            {
                list.Add(file.Name);
            }
            return list.ToArray();
        }

        private void ShowW(object sender, EventArgs e)
        {
            Show();
            UpdateGui();
        }
    }
}
