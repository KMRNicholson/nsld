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
        public WorkoutsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            NavigationPage page = (NavigationPage)this.Parent;
            page.BarBackgroundColor = Color.FromHex("#99aabb");
            page.BarTextColor = Color.White;

            ViewModels.WorkoutsPageViewModel workoutList = new ViewModels.WorkoutsPageViewModel(await App.WorkoutDAL.GetWorkoutsAsync());
            helpLabel.IsVisible = workoutList.IsEmpty();
            listView.ItemsSource = workoutList.WorkoutList;
            
        }

        async void OnAddOrEdit(object sender, EventArgs e)
        {
            var id = ((Button)sender).BindingContext;
            if(id != null)
            {
                Models.Workout workout = await App.WorkoutDAL.GetWorkoutAsync((int)id);
                await Navigation.PushAsync(new AddEditWorkoutPage
                {
                    BindingContext = workout as Models.Workout
                });
            }
            else
            {
                await Navigation.PushAsync(new AddEditWorkoutPage
                {
                    BindingContext = new Models.Workout()
                });
            }
        }

        async void OnDelete(object sender, EventArgs e)
        {
            var id = ((Button)sender).BindingContext;
            Models.Workout workout = await App.WorkoutDAL.GetWorkoutAsync((int)id);
            await App.WorkoutDAL.DeleteWorkoutAsync(workout);

            ViewModels.WorkoutsPageViewModel workoutList = new ViewModels.WorkoutsPageViewModel(await App.WorkoutDAL.GetWorkoutsAsync());
            helpLabel.IsVisible = workoutList.IsEmpty();
            listView.ItemsSource = workoutList.WorkoutList;
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