using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;

namespace MaratonaIntermediariaProva.Droid
{

    public static class NotificationConstants
    {
        public const string SenderId = "970554866187";
        public const string ListenConnectionString = "Endpoint=sb://provamaratonanotificaiion.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=STkApqLpRMzXS3vXFtW0b3e3Fe9bgpfI0M5hfLBFR/g=";
        public const string HubName = "ProvaMaratonaNotification";
    }

    [Activity(Label = "MaratonaIntermediariaProva", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        void RegisterGCM()
        {
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            GcmClient.Register(this, NotificationConstants.SenderId);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            MainActivity.Instance = this;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            LoadApplication(new App());

            this.RegisterGCM();
        }

        public static MainActivity Instance { get; private set; }
    }
}

