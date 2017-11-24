using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MediaDevices;

namespace Laba_4
{
    // Класс для выполнения действий по перезагрузке
    class Manager
    {
        // Метод получения результатов в списке устройств
        public List<Usb> DeviseListCreate()
        {
            List<Usb> usbDevices = new List<Usb>();
            // Получение USB и MTP-накопителей
            List<DriveInfo> diskDrives = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Removable).ToList();
            List<MediaDevice> mtpDrives = MediaDevice.GetDevices().ToList();
            // Мы не можем извлечь MTP, и у нас нет его объема, поэтому мы должны работать с ним по-другому
            foreach (MediaDevice device in mtpDrives)
            {
                // Подключение к устройству MTP
                device.Connect();
                // Если наше устройство не является обычным USB
                if (device.DeviceType != DeviceType.Generic)
                {
                    // Мы добавляем это устройство в список MTP
                    usbDevices.Add(new Usb(device.FriendlyName, null, null, null, true));
                }
            }
            // После MTP мы подсчитываем и добавляем другие USB-устройства
            foreach (DriveInfo drive in diskDrives)
            {
                // Добавить USB-устройство для отображения и расчета размеров
                usbDevices.Add(new Usb(drive.Name, Convert(drive.TotalFreeSpace),
                    Convert(drive.TotalSize - drive.TotalFreeSpace),
                    Convert(drive.TotalSize), false));
            }
            return usbDevices;
        }

        // Мы получаем размер в байтах, поэтому нам нужно преобразовать его в MB
        private string Convert(long value)
        {
            double megaBytes = (value / 1024) / 1024;
            return megaBytes.ToString() + " mb";
        }
    }
}
