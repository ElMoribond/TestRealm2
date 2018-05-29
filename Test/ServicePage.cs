using System;
using Xamarin.Forms;

namespace Test
{
    public class ServicePage : ContentPage
    {
        private ListView listView = new ListView();
        private Button StartButton = new Button { Text = "Start counter" };
        private Button StopButton = new Button { Text = "Stop" };
        private void Clicked(object sender, EventArgs e) => MessagingCenter.Send(new ServiceMessage(sender == StartButton), nameof(ServiceMessage));
        public ServicePage()
        {
            var buttonGrid = new Grid();
            buttonGrid.Children.Add(StartButton, 0, 0);
            buttonGrid.Children.Add(StopButton, 1, 0);
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
            grid.Children.Add(buttonGrid, 0, 0);
            grid.Children.Add(listView, 0, 1);
            Content = grid;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var realm = await MyRealm.OpenRealm();
            if (realm != null)
                listView.ItemsSource = realm.All<CounterMessage>();
            StartButton.Clicked += Clicked;
            StopButton.Clicked += Clicked;
            MessagingCenter.Subscribe<ServiceMessage>(this, nameof(ServiceMessage), (message) => {
                if (message.MessageType == Constants.TypeMessage.ServiceStarted && realm != null)
                    realm.Write(() => realm.Add(new CounterMessage { Text = "Service ready" }));
            });
        }
        protected override void OnDisappearing()
        {
            StartButton.Clicked -= Clicked;
            StopButton.Clicked -= Clicked;
            MessagingCenter.Unsubscribe<string>(this, nameof(ServiceMessage));
            base.OnDisappearing();
        }
    }
}