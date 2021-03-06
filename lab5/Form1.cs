﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace lab5
{
    public partial class Form1 : Form
    {
        private readonly List<Device> _devices;

        private int _item;

        public Form1()
        {
            InitializeComponent();
            _devices = DeviceManager.GetDevices();
            foreach (var device in _devices)
            {
                ListOfDevices.Items.Add(device.Name);
            }
            TurnOn.Click += TurnOnClicked;
            TurnOff.Click += TurnOffClicked;
            ListOfDevices.Click += ItemSelected;
        }

        private void ShowInformation(Device device)
        {
            StringBuilder stringBuilder = new StringBuilder();
           
            stringBuilder
                .AppendLine("GUID: " + device.ClassGuid)
                .AppendLine("HardwareID: " + device.HardwareId)
                .AppendLine("Manufacturer: " + device.Manufacturer)
                .AppendLine("Provider: " + device.Provider)
                .AppendLine("Driver description: " + device.Description)
                .AppendLine("Device path: " + device.DevicePath)
                .AppendLine("System path: " + device.SysPath)
                .AppendLine("Status: " + device.Status);
            DeviceDescription.Text = stringBuilder.ToString();
            TurnOn.Enabled = !device.Status;
            TurnOff.Enabled = device.Status;
        }

        private void ItemSelected(object sender, EventArgs e)
        {
            ShowInformation(_devices[ListOfDevices.SelectedItems[0].Index]);
            _item = ListOfDevices.SelectedItems[0].Index;
        }

        private void TurnOnClicked(object sender, EventArgs e)
        {
            try
            {
                _devices[_item].ChangeConnection("Enable");
                _devices[_item].Status = !_devices[_item].Status;
                ShowInformation(_devices[ListOfDevices.SelectedItems[0].Index]);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Устройство не может быть включено.");
            }
        }

        private void TurnOffClicked(object sender, EventArgs e)
        {
            try
            {
                _devices[_item].ChangeConnection("Disable");
                _devices[_item].Status = !_devices[_item].Status;
                ShowInformation(_devices[ListOfDevices.SelectedItems[0].Index]);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Устройство не может быть отключено.");
            }
        }
    }
}
