using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Shs.HomeAuto.Android
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
            this._settingsButton = FindViewById<Button>(Android.Resource.Id.SettingsButton);
            this._garageButton = FindViewById<Button>(Android.Resource.Id.GarageDoorButton);

            this._settingsButton.Click += _settingsButton_Click;
        }

        private void _settingsButton_Click(object sender, System.EventArgs e)
        {
            // Call the settings page
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }
    }
}

