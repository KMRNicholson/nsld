using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.DAL;

namespace NeverSkipLegDay.Workouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Workouts : ContentPage
    {
        public Workouts()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            NavigationPage page = (NavigationPage)this.Parent;
            page.BarBackgroundColor = Color.FromHex("#99aabb");
            page.BarTextColor = Color.White;

            WorkoutList workoutList = new WorkoutList(await App.WorkoutDAL.GetWorkoutsAsync());
            if (workoutList.IsEmpty())
            {
                helpLabel.IsVisible = true;
            }
            else
            {
                listView.ItemsSource = workoutList.Workouts;
            }
        }

        async void OnAddOrEdit(object sender, EventArgs e)
        {
            var id = ((Button)sender).BindingContext;
            if(id != null)
            {
                Models.Workout workout = await App.WorkoutDAL.GetWorkoutAsync((int)id);
                await Navigation.PushAsync(new AddEditWorkout
                {
                    BindingContext = workout as Models.Workout
                });
            }
            else
            {
                await Navigation.PushAsync(new AddEditWorkout
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
            listView.ItemsSource = await App.WorkoutDAL.GetWorkoutsAsync();
            List<Models.Workout> workouts = (List<Models.Workout>)listView.ItemsSource;
            helpLabel.IsVisible = workouts.Count == 0 ? true : false;
        }

        async void OnWorkoutSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Workout
                {
                    BindingContext = e.SelectedItem as Models.Workout
                });
            }
        }
    }
}