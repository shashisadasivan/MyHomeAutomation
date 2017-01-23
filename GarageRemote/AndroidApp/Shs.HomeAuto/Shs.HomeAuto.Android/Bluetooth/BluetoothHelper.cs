using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Bluetooth;
using System.Threading.Tasks;

namespace Shs.HomeAuto.Droid.Bluetooth
{
    public class BluetoothHelper
    {
        private BluetoothAdapter _adapter;
        private BluetoothDevice _device;
        private BluetoothSocket _socket;

        public BluetoothHelper()
        {
            this._adapter = BluetoothAdapter.DefaultAdapter;
        }

        public bool IsActive()
        {
            return _adapter.IsEnabled;
        }

        /// <summary>
        /// Get Paired devices
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BluetoothDevice> GetPairedDevices()
        {
            return this._adapter.BondedDevices;
        }

        public async Task<bool> TryConnect(string address)
        {
            bool ret = false;

            _device = this._adapter.BondedDevices.Where(bt => bt.Address.Equals(address)).FirstOrDefault();
            if(_device != null)
            {
                try
                {
                    _socket = _device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                    await _socket.ConnectAsync();
                    if (_socket.IsConnected == true)
                        ret = true;
                }
                catch
                {
                    ret = false;
                }
            }

            return ret;
        }

        public async Task SendSignal(string deviceAddress)
        {
            // Get the device
            var connected = await this.TryConnect(deviceAddress);
            if(connected == true)
            {
                string value = "GarageClickerToggleDoor";
                try
                {
                    // var bytes = Convert.FromBase64String(value);
                    var bytes = System.Text.Encoding.UTF8.GetBytes(value);
                    await _socket.OutputStream.WriteAsync(bytes, 0, bytes.Length);
                }
                catch (Exception ex)
                {
                    throw;    
                }
            }
        }
    }
}