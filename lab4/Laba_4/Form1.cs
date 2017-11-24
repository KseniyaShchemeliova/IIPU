using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_4
{
    
    public partial class Form1 : Form
    {
        // Постоянная для системного метода перезаписи
        private const int WM_DEVICECHANGE = 0X219;
        private static readonly Manager _manager = new Manager();
        private List<Usb> _deviceList;
        //Наша таблица выбора
        private readonly DataTable _table = new DataTable();

        // Мы следим за системными сообщениями
        protected override void WndProc(ref Message m)
        {
            // Перенаправление сообщения в нашу программу
            base.WndProc(ref m);
            // Если конфигурация наших устройств изменилась
            if (m.Msg == WM_DEVICECHANGE)
            {
                // Перезагрузить наши данные
                ReloadForm();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        // Когда мы в первый раз загружаем нашу форму
        private void LoadForm(object sender, EventArgs e)
        {
            _deviceList = new List<Usb>();
            _table.Columns.Add("Название", typeof(string));
            // Получаем все наши устройства
            ReloadForm();
            usbList.DataSource = _table;
            removeButton.Enabled = false;
            timer.Enabled = true;
        }
        //Метод перезагрузки программы
        private void ReloadForm()
        {
            int currentPosition = 0;
            // Проверка выбранной строки
            if (usbList.CurrentRow != null)
            {
                currentPosition = usbList.CurrentRow.Index;
            }
            // Удалить все прошлые данные
            _table.Clear();
            _deviceList = _manager.DeviseListCreate();
            foreach(Usb device in _deviceList)
            {
                _table.Rows.Add(device.DeviceName);
            }
            // Если нет индекса границы
            if (usbList.RowCount - 1 > currentPosition)
            {
                //Затем мы выбираем эту строку
                usbList.Rows[currentPosition].Selected = true;
            }
            label1.Text = "";
        }

        // Таймер для перезагрузки каждые 5 секунд
       /* private void TickTimer(object sender, EventArgs e)
        {
            ReloadForm();
        }*/

        // Событие для выбора некоторой строки в списке
        private void ChangeSelect(object sender, EventArgs e)
        {
            // Если строка существует
            if (usbList.CurrentRow != null)
            {
                // Если нет индекса границ
                if (usbList.CurrentRow.Index >= 0 && usbList.CurrentRow.Index < _deviceList.Count)
                {
                    // Мы можем извлечь только USB, а не MTP
                    removeButton.Enabled = !_deviceList[usbList.CurrentRow.Index].IsMtpDevice;
                    // То же самое с выводом информации
                    if (!_deviceList[usbList.CurrentRow.Index].IsMtpDevice)
                    {
                        spaceTextBox.Text = "Свободно памяти: " + _deviceList[usbList.CurrentRow.Index].FreeSpace + "\r\n" +
                                        "Занято памяти: " + _deviceList[usbList.CurrentRow.Index].UsedSpace + "\r\n" +
                                        "Общая память: " + _deviceList[usbList.CurrentRow.Index].TotalSpace;
                    }
                }
                else
                {
                    // В другом случае блокируются все поля
                    removeButton.Enabled = false;
                    spaceTextBox.Text = "";
                }
            }
        }

        // Событие для нажатия кнопки удаления
        private void OnclickButton(object sender, EventArgs e)
        {
            // Если мы выберем, какое устройство мы хотим выбросить
            if (usbList.CurrentRow != null)
            {
                // Затем мы должны вызвать метод Eject_Device()
                bool isEjected = _deviceList[usbList.CurrentRow.Index].EjectDevice();
                if (isEjected == false)
                {
                    label1.Text = "Устройство занято.";
                }
                else
                {
                    spaceTextBox.Text = "";
                }
            }
        }
    }
}
