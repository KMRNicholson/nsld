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
            await never.FadeTo(1, 500);
            await skip.FadeTo(1, 500);
            await leg.FadeTo(1, 500);
            await day.FadeTo(1, 500);
            base.OnAppearing();
        }

        async void OnEnter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }
    }
}
