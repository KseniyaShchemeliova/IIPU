using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbEject;

namespace Laba_4
{
    // Общий класс для USB-устройств
    class Usb
    {
        public string DeviceName { get; set; }
        public string FreeSpace { get; set; }
        public string UsedSpace { get; set; }
        public string TotalSpace { get; set; }
        public bool IsMtpDevice { get; set; }

        public Usb(string name, string freeSize, string usedSize, string totalSize, bool check)
        {
            DeviceName = name;
            FreeSpace = freeSize;
            UsedSpace = usedSize;
            TotalSpace = totalSize;
            IsMtpDevice = check;
        }
        // Метод для извлечения устройства с утилитой RemoveDrive
        public bool EjectDevice()
        {
            var tempName = this.DeviceName.Remove(2);
            var ejectedDevice = new VolumeDeviceClass().SingleOrDefault(v => v.LogicalDrive == this.DeviceName.Remove(2));
            ejectedDevice.Eject(false);
            ejectedDevice = new VolumeDeviceClass().SingleOrDefault(v => v.LogicalDrive == tempName);
           
            if (ejectedDevice == null)
                return true;
            else
                return false;
        }
    }
}
