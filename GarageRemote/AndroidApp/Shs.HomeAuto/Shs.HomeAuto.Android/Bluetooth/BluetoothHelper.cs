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

        /// <summary>
        /// Starts the Bluetooth module if it is not already
        /// </summary>
        public void CheckBluetoothStatus()
        {
            var enable = false;
            if(this._adapter.IsEnabled == false
                || this._adapter.State == State.Off)
            {
                throw new Exception("Turn BLuetooth on");
                /*
                enable = this._adapter.Enable();
                while (enable == true && this._adapter.State != State.Connected)
                {
                    System.Threading.Thread.Sleep(500);
                }
                */
            }
        }
        public async Task<bool> TryConnect(string address)
        {
            bool ret = false;

            this.CheckBluetoothStatus(); // Start the Bluetooth if it is not already

            _device = this._adapter.BondedDevices.Where(bt => bt.Address.Equals(address)).FirstOrDefault();
            if(_device != null)
            {
                try
                {
                    if (_socket != null && _socket.IsConnected)
                    {
                        ret = true;
                    }
                    else
                    {
                        //_socket = _device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                        _socket = _device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));
                        await _socket.ConnectAsync();
                        if (_socket.IsConnected == true)
                            ret = true;
                    }
                }
                catch (Exception ex)
                {
                    //var clazz = _socket.RemoteDevice.Class;
                    //// Java.Lang.Class <?>[] paramTypes = new Java.Lang.Class<?>[] { Integer.TYPE };

                    //var  m = clazz.GetMethod("createRfcommSocket", new Java.Lang.Class[] { Java.Lang.Integer.Type } );
                    //var parameters = new Object[] { Java.Lang.Integer.ValueOf(1) };

                    //fallbackSocket = (BluetoothSocket)m.Invoke(_socket.RemoteDevice, parameters);
                    //fallbackSocket.connect();

                    _socket = (BluetoothSocket)_device.Class.GetMethod("createRfcommSocket", new Java.Lang.Class[] { Java.Lang.Integer.Type}).Invoke(_device,1);
                    await _socket.ConnectAsync();
                    if (_socket.IsConnected)
                    {
                        ret = true;
                    }
                    else
                    {
                        throw;
                        ret = false;
                    }
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
                string value = "ToggleMyGarageDoor";
                try
                {
                    // var bytes = Convert.FromBase64String(value);
                    var bytes = System.Text.Encoding.UTF8.GetBytes(value);
                    await _socket.OutputStream.WriteAsync(bytes, 0, bytes.Length);
                    _socket.Close();
                    _socket.Dispose();
                    _socket = null;
                }
                catch (Exception ex)
                {
                    throw;    
                }
            }
        }
    }
}