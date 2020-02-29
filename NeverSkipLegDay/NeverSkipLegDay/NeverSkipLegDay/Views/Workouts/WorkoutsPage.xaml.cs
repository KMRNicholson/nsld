using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutsPage : ContentPage
    {
        public WorkoutsPageViewModel ViewModel 
        { 
            get { return BindingContext as WorkoutsPageViewModel; }
            set { BindingContext = value; }
        }
        public WorkoutsPage()
        {
            var workoutDal = new WorkoutDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new WorkoutsPageViewModel(workoutDal, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();

            NavigationPage page = (NavigationPage)this.Parent;
            page.BarBackgroundColor = Color.FromHex("#99aabb");
            page.BarTextColor = Color.White;
        }

        async void OnWorkoutSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new WorkoutPage
                {
                    BindingContext = e.SelectedItem as Models.Workout
                });
            }
        }
    }
}