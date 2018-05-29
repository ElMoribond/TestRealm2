using Android.Content;
using Android.OS;

namespace Test.Droid
{
	public class CounterServiceConnection : Java.Lang.Object, IServiceConnection
	{
		public Messenger Messenger { get; private set; }
        public void OnServiceDisconnected(ComponentName name) => Messenger = null;
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Helper.ServiceStarted = true;
            MainActivity.Instance.ServiceStarted();
            Messenger = new Messenger(service);
        }
    }
}