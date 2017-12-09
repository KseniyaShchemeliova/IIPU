using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace lab5
{
    class Device
    {
        public string Name { get; set; }

        public string ClassGuid { get; set; }

        public string HardwareId { get; set; } //айди железа

        public string Manufacturer { get; set; } //произволдитель

        public string Description { get; set; } // описание устройства

        public string Provider { get; set; } 

        public string SysPath { get; set; } // путь к драйверу

        public string DevicePath { get; set; } // пусть к устрйству

        public bool Status { get; set; }

        public void ChangeConnection(string method)
        {
            var device = new ManagementObjectSearcher("SELECT * FROM Win32_PNPEntity").Get().OfType<ManagementObject>()
                .FirstOrDefault(x => x["DeviceID"].ToString().Equals(DevicePath));
            device?.InvokeMethod(method, new object[] { false });
        }
    }
}
