using System;
using System.ComponentModel;
using Xamarin.Forms;
using NeverSkipLegDay.Views;

namespace NeverSkipLegDay
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void OnEnter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Dashboard());
        }
    }
}
