using Realms;
using Realms.Sync;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading.Tasks;
using static Test.Constants;

namespace Test
{
    static public class MyRealm
    {
        static public Task<Realm> GetInstanceAsync() => Realm.GetInstanceAsync(GetConfig());
        static public SyncConfiguration GetConfig(User user = null) => new SyncConfiguration(user ?? User.Current, RealmUri);
        static public async Task<Realm> OpenRealm(bool createUser = false)
        {
            try
            {
                foreach (var user in User.AllLoggedIn)
                    await user.LogOutAsync();
                var realm = await Realm.GetInstanceAsync(GetConfig(await User.LoginAsync(Credentials.UsernamePassword(API_USERNAME, API_PASSWORD, createUser), AuthServerUri)));
                using (var transaction = realm.BeginWrite())
                {
                    realm.RemoveAll<CounterMessage>();
                    realm.Add(new CounterMessage { Text = Helper.ServiceStarted ? "Already started" : "Please wait, service starting" });
                    transaction.Commit();
                }
                return realm;
            }
            catch (AuthenticationException) { Console.WriteLine($"{TAG} Erreur Unknown Username and Password combination"); }
            catch (SocketException sockEx) { Console.WriteLine($"{TAG} Erreur Network error: {sockEx}"); }
            catch (WebException webEx) { Console.WriteLine($"{TAG} Erreur {(webEx.Status == WebExceptionStatus.ConnectFailure ? $"Unable to connect to Server" : "Error trying to login")} {webEx.Message}"); }
            catch (Exception e) { Console.WriteLine($"{TAG} Erreur {(User.Current == null ? "Error trying to login" : "Credentials accepted but then failed to open Realm")} {e.GetType().FullName} {e.Message}"); }
            return null;
        }
    }
}
