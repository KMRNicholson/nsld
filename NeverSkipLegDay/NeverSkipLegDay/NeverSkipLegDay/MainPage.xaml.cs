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
            uint wordFade = 800;
            int delay = 100;

            never.FadeTo(1, wordFade).ConfigureAwait(true).GetAwaiter();
            await Task.Delay(delay).ConfigureAwait(true);
            skip.FadeTo(1, wordFade).ConfigureAwait(true).GetAwaiter();
            await Task.Delay(delay).ConfigureAwait(true);
            leg.FadeTo(1, wordFade).ConfigureAwait(true).GetAwaiter();
            await Task.Delay(delay).ConfigureAwait(true);
            day.FadeTo(1, wordFade).ConfigureAwait(true).GetAwaiter();
            await Task.Delay(delay).ConfigureAwait(true);
            base.OnAppearing();
        }

        async void OnEnter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }
    }
}
