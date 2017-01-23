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

namespace Shs.HomeAuto.Droid.Storage
{
    public static class Settings
    {
        public const string APP_PREFS_NAME = "BL_PREFS";
        public const string BL_DEFAULT_DEVICE_ADDRESS = "BL_DEFAULT_DEVICE_ADDRESS";

        private static ISharedPreferences GetPreference()
        {
            var prefs = Application.Context.GetSharedPreferences(APP_PREFS_NAME, FileCreationMode.Private);
            return prefs;
        }

        public static string GetDefaultDeviceAddress()
        {
            var prefs = GetPreference();
            var device = prefs.GetString(BL_DEFAULT_DEVICE_ADDRESS, string.Empty);
            return device;
        }

        public static void SetDefaultDevice(string deviceName)
        {
            var prefs = GetPreference();
            var editor = prefs.Edit();
            editor.PutString(BL_DEFAULT_DEVICE_ADDRESS, deviceName);
            editor.Apply();
        }
    }
}