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
using Shs.HomeAuto.Droid.Bluetooth;

namespace Shs.HomeAuto.Droid
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        ListView _BluetoothDevicesListView;
        private BluetoothHelper _bluetoothHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Settings);

            this._bluetoothHelper = new BluetoothHelper();
            this.SetControls();
        }

        private void SetControls()
        {
            try
            {
                _BluetoothDevicesListView = FindViewById<ListView>(Resource.Id.BluetoothDevicesList);

                //Puts data into the control
                this.SetBluetoothList();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long)
                    .Show();
            }
        }

        private void SetBluetoothList()
        {
            var devices = this._bluetoothHelper.GetPairedDevices();
            this._BluetoothDevicesListView.Adapter = new BluetoothListView2Adapter(this, devices.ToList());
            this._BluetoothDevicesListView.ChoiceMode = ChoiceMode.Single;
            this._BluetoothDevicesListView.ItemClick += _BluetoothDevicesListView_ItemClick;

            this.SetDefaultBluetooth();
        }

        private void _BluetoothDevicesListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                var adapter = this._BluetoothDevicesListView.Adapter as BluetoothListView2Adapter;
                var selectedDevice = adapter[e.Position];

                Storage.Settings.SetDefaultDevice(selectedDevice.Address);

                Toast.MakeText(this, "Default device set", ToastLength.Short)
                        .Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long)
                        .Show();
            }
        }

        private void SetDefaultBluetooth()
        {
            // Get the default device - Make sure its in the list
            var defaultDeviceAddress = Storage.Settings.GetDefaultDeviceAddress();
            // Set the default highlighted dvice in the list
            if(defaultDeviceAddress != string.Empty)
            {
                var adapter = this._BluetoothDevicesListView.Adapter as BluetoothListView2Adapter;
                var device = adapter.BLDevices.FirstOrDefault(d => d.Address.Equals(defaultDeviceAddress));
                if(device != null)
                {
                    var indexOfDefault = adapter.BLDevices.IndexOf(device);
                    this._BluetoothDevicesListView.SetItemChecked(indexOfDefault, true);
                }
            }
        }



    }
}