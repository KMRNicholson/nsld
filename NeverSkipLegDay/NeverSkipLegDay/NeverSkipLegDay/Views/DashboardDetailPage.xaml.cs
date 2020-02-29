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
            WorkoutsViewModel WorkoutsViewModel = new WorkoutsViewModel(await App.WorkoutDAL.GetWorkoutsAsync());
            await Navigation.PushAsync(new Workouts()
            {
                BindingContext = WorkoutsViewModel as WorkoutsViewModel
            });
        }
    }
}