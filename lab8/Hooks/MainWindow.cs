using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hooks
{
    public partial class MainWindow : Form
    {
        private readonly ConfigManager _configManager = new ConfigManager();
        private readonly HooksManager _hooksManager;
        private readonly Configuration _configuration;

        public MainWindow()
        {
            InitializeComponent();
            _configuration = _configManager.Read();
            _hooksManager = new HooksManager(_configuration);
            _hooksManager.ShowWindow += Show;
            InitializeGui();
        }

        private void InitializeGui()
        {
            HideCheckBox.Checked = _configuration.Hide;
            FileSizeNumericUpDown.Value = _configuration.FileSize;
            EmailTextBox.Text = _configuration.Email ?? string.Empty;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!new Regex("[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}").IsMatch(EmailTextBox.Text.ToLower()))
            {
                MessageBox.Show(@"Email");
                return;
            }
            _configuration.Email = EmailTextBox.Text;
            _configuration.FileSize = (long) FileSizeNumericUpDown.Value;
            _configuration.Hide = HideCheckBox.Checked;
            _configManager.Save(_configuration);
        }

        private void HideButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            if(_configuration.Hide)
                Hide();
        }
    }
}
