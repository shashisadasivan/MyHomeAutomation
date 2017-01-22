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

namespace Shs.HomeAuto.Android
{
    public class BluetoothHelper
    {
        private BluetoothAdapter _adapter;

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

            var device = this._adapter.BondedDevices.Where(bt => bt.Address.Equals(address)).FirstOrDefault();
            if(device != null)
            {
                try
                {
                    var socket = device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                    await socket.ConnectAsync();
                    if (socket.IsConnected == true)
                        ret = true;
                }
                catch
                {
                    ret = false;
                }
            }

            return ret;
        }
    }
}