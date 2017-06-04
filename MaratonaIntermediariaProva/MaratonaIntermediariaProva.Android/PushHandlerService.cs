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
using Gcm.Client;
using WindowsAzure.Messaging;
using Android.Util;
using Android.Support.V7.App;
using Android.Media;

namespace MaratonaIntermediariaProva.Droid
{
    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        private NotificationHub Hub { get; set; }
        public PushHandlerService() : base(NotificationConstants.SenderId)
        {

        }

        public static string RegistrationId { get; set; }

        protected override void OnRegistered(Context context, string registrationId)
        {
            RegistrationID = registrationId;

            this.Hub = new NotificationHub(
                NotificationConstants.HubName,
                NotificationConstants.ListenConnectionString,
                context);

            try
            {
                this.Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                Log.Error("NotificationTest", ex.Message);
            }

            var tags = new List<string>() { };

            try
            {
                var hubRegistration = this.Hub.Register(
                    registrationId,
                    tags.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error("NotificationTest", ex.Message);
            }
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                {
                    msg.AppendLine(
                        key +
                        "=" +
                        intent.Extras.Get(key).ToString());
                }
            }

            string messageText = intent.Extras.GetString("message");

            if (string.IsNullOrWhiteSpace(messageText))
            {
                this.CreateNotification("Unknown message details", msg.ToString());
            }
            else
            {
                this.CreateNotification("New message!", messageText);
            }

        }

        void CreateNotification(string title, string desc)
        {
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            var uiIntent = new Intent(this, typeof(MainActivity));

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetTicker(title)
                .SetContentTitle(title)
                .SetContentText(desc)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true).Build();

            notificationManager.Notify(1, notification);
        }

        protected override void OnError(Context context, string errorId)
        {
            throw new NotImplementedException();
        }        

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            throw new NotImplementedException();
        }
    }
}