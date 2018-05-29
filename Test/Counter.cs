using Realms;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Test.Constants;

namespace Test
{
    public class Counter
    {
        private int iCounter;
        public async Task Run(CancellationToken token, bool isBoot)
        {
            try
            {
                await Task.Run(async () =>
                {
                    //Realm realm = await MyRealm.GetInstanceAsync();
                    Realm realm = null;
                    for (iCounter = 1; true; iCounter++)
                    {
                        if (!token.IsCancellationRequested)
                        {
                            Console.WriteLine($"{TAG} Message {iCounter}");
                            if (realm != null)
                                realm.Write(() => realm.Add(new CounterMessage { Text = $"Message {iCounter}" }));
                        }
                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine($"{TAG} Ok exit.");
                            if (realm != null)
                            {
                                realm.Write(() => realm.Add(new CounterMessage { Text = "Last message" }));
                                realm.Dispose();
                            }
                            break;
                        }
                        else Task.Delay(5000).Wait();
                    }
                }, token);
            }
            catch (Exception ex) { Console.WriteLine($"{TAG} Erreur {ex.Message}"); }
        }
    }
}