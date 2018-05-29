using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using System.Threading;
using System.Threading.Tasks;
using static Test.Constants;

namespace Test.Droid
{
    [Service(Name = PACKAGE_NAME + "." + nameof(CounterService), Process = PACKAGE_NAME + ".counter")]
    public class CounterService : Service, ICounter
    {
        private Messenger Messenger;
        private CancellationTokenSource TaskCanceler;
        public CounterService() { }
        public override IBinder OnBind(Intent intent) => Messenger.Binder;
        public override void OnCreate()
		{
			base.OnCreate();
            Messenger = new Messenger(new CounterRequestHandler(this));
            Log.Info(TAG, $"{nameof(CounterService)} Pid={Process.MyPid()}");
        }
        public override void OnDestroy()
        {
            Messenger.Dispose();
            Stop();
            base.OnDestroy();
        }
        public void Stop() => TaskCanceler.Cancel(true);
        public async Task Start(bool isBoot = false) => await new Counter().Run((TaskCanceler = new CancellationTokenSource()).Token, isBoot);
    }
}