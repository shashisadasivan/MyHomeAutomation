using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Shs.HomeAuto.Droid.Bluetooth;
using System;

namespace Shs.HomeAuto.Droid
{
    [Activity(Label = "Garage door clicker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button _settingsButton;
        Button _garageButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            //Set our controls here
            this.SetControls();
        }

        private void SetControls()
        {
            this._settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
            this._garageButton = FindViewById<Button>(Resource.Id.GarageDoorButton);

            this._settingsButton.Click += _settingsButton_Click;
            this._garageButton.Click += _garageButton_Click;
        }

        private async void _garageButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Send signal to BLuetooth
                var defaultDevice = Storage.Settings.GetDefaultDeviceAddress();
                BluetoothHelper helper = new BluetoothHelper();

                await helper.SendSignal(defaultDevice);

                Toast.MakeText(this, "Garage door called", ToastLength.Short);
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long);
            }
        }

        private void _settingsButton_Click(object sender, System.EventArgs e)
        {
            // Call the settings page
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }
    }
}

