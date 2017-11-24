using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Battery
{
    public class BatteryManager
    {
        public int PreviousScreenTime { get; set; }
        public string Charging { get; set; }
        public string PercentBattery { get; set; }
        public string WorkTime { get; set; }
        public string PreviousCharging { get; set; }
        public bool StartApp { get; set; }

        public void Init()
        {
            // Запуск нашего приложения и получение состояния батареи
            StartApp = true;
            PreviousScreenTime = GetScreenTime();
            // Обновление приложения
            UpdateData();
        }

        // метод получения текущего состояния времени
        public int GetScreenTime()
        {
            //Описание процесса для cmd
            var procCmd = new Process();
            procCmd.StartInfo.UseShellExecute = false;
            procCmd.StartInfo.RedirectStandardOutput = true;
            procCmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            procCmd.StartInfo.FileName = "cmd.exe";
            procCmd.StartInfo.Arguments = "/c powercfg /q";
            procCmd.Start();
            //Из всего вывода нам нужен только VIDEOIDLE и последняя строка
            var powerConfig = procCmd.StandardOutput.ReadToEnd();
            var lastString = new Regex("VIDEOIDLE.*\\n.*\\n.*\\n.*\\n.*\\n.*\\n.*");
            var videoIdle = lastString.Match(powerConfig).Value;
            // После этого нам понадобится только 16 значений
            var batteryState = videoIdle.Substring(videoIdle.Length - 11).TrimEnd();
            //Преобразование string в Int
            return Convert.ToInt32(batteryState, 16) / 60;
        }
        public void UpdateData()
        {
            // Получение текущего состояния
            Charging = SystemInformation.PowerStatus.PowerLineStatus.ToString();
            if (StartApp)
            {
                PreviousCharging = Charging;
                StartApp = false;
            }
            // Получение текущего процента батареи
            PercentBattery = SystemInformation.PowerStatus.BatteryLifePercent * 100 + "%";
            // Если мы не используем AC
            if (Charging == "Offline")
            {
                // Тогда мы можем начать вычислять время до разрядки
                var calcLife = SystemInformation.PowerStatus.BatteryLifeRemaining;
                // Если мы не получили значение, продолжаем вычислять
                if (calcLife != -1)
                {
                    // Или инициализировать переменную для ее форматирования
                    var span = new TimeSpan(0, 0, calcLife);
                    WorkTime = span.ToString("g");
                }                                                                      
                else
                {
                    WorkTime = "Подсчет времени...";
                }
            }
            else
            {
                WorkTime = "Устройство работает от сети.";
            }
        }

        // Метод для изменения времени до отключения экрана
        public void SetDisplayOff(int value)
        {
            // Описание процесса для cmd
            var p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.Arguments = "/c powercfg /x -monitor-timeout-dc " + value;
            p.Start();
        }
    }
}