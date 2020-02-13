using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.Models;

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

        async void OnAdd(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddWorkout
            {
                BindingContext = new Workout()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AddWorkout
                {
                    BindingContext = e.SelectedItem as Workout
                });
            }
        }
    }
}