using Xamarin.Forms;

namespace Test
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new ServicePage());
        }
    }
}