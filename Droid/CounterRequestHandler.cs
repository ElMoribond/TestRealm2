using System;
using Android.OS;
using static Test.Constants;

namespace Test.Droid
{
    public class CounterRequestHandler : Handler
    {
        private WeakReference<CounterService> serviceRef;
        private CounterService Service => serviceRef.TryGetTarget(out CounterService service) ? service : null;
        public CounterRequestHandler(CounterService service) => serviceRef = new WeakReference<CounterService>(service);
        public override void HandleMessage(Message message)
        {
            if ((TypeMessage)message.What == TypeMessage.StartCount)
                Service?.Start();
            else
                Service?.Stop();
        }
    }
}