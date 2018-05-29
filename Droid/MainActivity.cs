using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using Xamarin.Forms;
using static Test.Constants;

namespace Test.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal CounterServiceConnection ServiceConnection;
        public static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Instance = this;
            Log.Info(TAG, $"OnCreate MainActivity Pid={Process.MyPid()}");
            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        protected override void OnStart()
        {
            base.OnStart();
            BindService((new Intent()).SetComponent(new ComponentName(PACKAGE_NAME, $"{PACKAGE_NAME}.{nameof(CounterService)}")), ServiceConnection = new CounterServiceConnection(), Bind.AutoCreate);
        }
        protected override void OnResume()
        {
            base.OnResume();
            MessagingCenter.Subscribe<ServiceMessage>(this, nameof(ServiceMessage), (message) => {
                if (message.MessageType == TypeMessage.ServiceStarted) return;
                if (ServiceConnection.Messenger != null)
                    ServiceConnection.Messenger.Send(new Message { What = (int)message.MessageType });
                else Toast.MakeText(this, "Not starting", ToastLength.Short).Show();
            });
        }
        protected override void OnPause()
        {
            MessagingCenter.Unsubscribe<string>(this, nameof(ServiceMessage));
            base.OnPause();
        }
        protected override void OnStop()
        {
            UnbindService(ServiceConnection);
            base.OnStop();
        }
        public void ServiceStarted() => MessagingCenter.Send(new ServiceMessage(), nameof(ServiceMessage));
    }
}