using Realms;
using System.Threading.Tasks;
using static Test.Constants;

namespace Test
{
    public class Helper
    {
        static public bool ServiceStarted;
    }
    public interface ICounter
    {
        void Stop();
        Task Start(bool isBoot = false);
    }
    public class CounterMessage : RealmObject
    {
        public string Text { get; set; } = "";
        public CounterMessage() { }
        public override string ToString() => Text;
    }
    public class ServiceMessage
    {
        public readonly TypeMessage MessageType;
        public ServiceMessage() => MessageType = TypeMessage.ServiceStarted;
        public ServiceMessage(bool start) => MessageType = start ? TypeMessage.StartCount : TypeMessage.StopCount;
    }
}