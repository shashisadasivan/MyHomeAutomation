using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Shs.HomeAuto.Droid.Bluetooth;
using System;

namespace Shs.HomeAuto.Droid
{
    //[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/garage")]
    public class MainActivity : Activity
    {
        ImageButton _settingsButton;
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
            //this._settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
            this._settingsButton = FindViewById<ImageButton>(Resource.Id.settingsImageButton);
            
            this._garageButton = FindViewById<Button>(Resource.Id.GarageDoorButton);

            this._settingsButton.Click += _settingsButton_Click;
            this._garageButton.Click += _garageButton_Click;
        }

        private async void _garageButton_Click(object sender, System.EventArgs e)
        {
            this._garageButton.Enabled = false;
            try
            {
                // Send signal to BLuetooth
                var defaultDevice = Storage.Settings.GetDefaultDeviceAddress();
                BluetoothHelper helper = new BluetoothHelper();

                await helper.SendSignal(defaultDevice);

                Toast.MakeText(this, "Garage door called", ToastLength.Short)
                        .Show();
            }
            catch(Exception ex)
            {
                //Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long);
                Toast.MakeText(ApplicationContext, "Error: " + ex.Message, ToastLength.Long)
                    .Show();
            }
            finally
            {
                this._garageButton.Enabled = true;
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

