using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            listView.ItemsSource = await App.WorkoutDAL.GetWorkoutsAsync();
        }

        async void OnAddOrEdit(object sender, EventArgs e)
        {
            var id = ((Button)sender).BindingContext;
            if(id != null)
            {
                Workout workout = await App.WorkoutDAL.GetWorkoutAsync((int)id);
                await Navigation.PushAsync(new AddEditWorkout
                {
                    BindingContext = workout as Workout
                });
            }
            else
            {
                await Navigation.PushAsync(new AddEditWorkout
                {
                    BindingContext = new Workout()
                });
            }
        }

        async void OnDelete(object sender, EventArgs e)
        {
            var id = ((Button)sender).BindingContext;            
            Workout workout = await App.WorkoutDAL.GetWorkoutAsync((int)id);
            await App.WorkoutDAL.DeleteWorkoutAsync(workout);
            await Navigation.PushAsync(new Workouts());
            
        }

        async void OnWorkoutSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AddEditWorkout
                {
                    BindingContext = e.SelectedItem as Workout
                });
            }
        }
    }
}