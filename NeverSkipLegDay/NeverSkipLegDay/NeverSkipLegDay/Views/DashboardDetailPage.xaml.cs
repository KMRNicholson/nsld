using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardDetailPage : ContentPage
    {
        public DashboardDetailPage()
        {
            InitializeComponent();
        }

        async void OnWorkoutsSelected(object sender, EventArgs e)
        {
            WorkoutsPageViewModel WorkoutsViewModel = new WorkoutsPageViewModel();
            await Navigation.PushAsync(new WorkoutsPage()
            {
                BindingContext = WorkoutsViewModel as WorkoutsPageViewModel
            });
        }
    }
}