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

namespace Shs.HomeAuto.Android
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        ListView _BluetoothDevicesListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Settings);

            this.SetControls();
        }

        private void SetControls()
        {
            _BluetoothDevicesListView = FindViewById<ListView>(Android.Resource.Id.BluetoothDevicesList);
        }

        private void SetBluetoothList()
        {

        }

        private void SetDefaultBluetooth()
        {

        }



    }
}