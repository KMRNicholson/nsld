using System;
using System.ComponentModel;
using Xamarin.Forms;
using NeverSkipLegDay.Views;
using System.Threading.Tasks;

namespace NeverSkipLegDay
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            await FadeInLabel(never);
            await FadeInLabel(skip);
            await FadeInLabel(leg);
            await FadeInLabel(day);

            base.OnAppearing();
        }

        private async Task FadeInLabel(StackLayout label)
        {
            uint fade = 800;
            int delay = 100;

            label.FadeTo(1, fade).ConfigureAwait(true).GetAwaiter();
            await Task.Delay(delay).ConfigureAwait(false);
        }

        public async void OnEnter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }
    }
}
