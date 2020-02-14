using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeverSkipLegDay.Workouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Workout : ContentPage
    {
        public Workout()
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
            if (id != null)
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
            
        }

        async void OnExerciseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }
    }
}