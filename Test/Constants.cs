using System;

namespace Test
{
    static public class Constants
    {
        public const string API_SERVER = "";
        public const string API_USERNAME = "";
        public const string API_PASSWORD = "";
        public static Uri AuthServerUri => new Uri($"https://{API_SERVER}:9443");
        public static Uri RealmUri => new Uri($"realm://{API_SERVER}:9080/~/default");

        public enum TypeMessage { ServiceStarted = 771, StartCount = 772, StopCount = 773 };
        public const string TAG = "App-Test";
        public const string PACKAGE_NAME = "fr.test.aps";
    }
}