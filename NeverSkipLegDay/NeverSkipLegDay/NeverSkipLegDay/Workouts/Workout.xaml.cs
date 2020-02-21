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
            Models.Workout workout = (Models.Workout)this.BindingContext;
            listView.ItemsSource = await App.ExerciseDAL.GetExercisesByWorkoutIdAsync(workout.ID);
        }

        async void OnAddOrEdit(object sender, EventArgs e)
        {
            Models.Exercise exercise;
            string[] args = ((Button)sender).BindingContext.ToString().Split(',');
            string operation = args[0];
            int id = Int32.Parse(args[1]);
            if(operation == "Add")
            {
                exercise = new Models.Exercise
                {
                    WorkoutID = id
                };
            }
            else
            {
                exercise = await App.ExerciseDAL.GetExerciseAsync(id);
            }

            await Navigation.PushAsync(new AddEditExercise
            {
                BindingContext = exercise as Models.Exercise
            });
        }
        async void OnDelete(object sender, EventArgs e)
        {
            var id = ((Button)sender).BindingContext;
            Models.Exercise exercise = await App.ExerciseDAL.GetExerciseAsync((int)id);
            await App.ExerciseDAL.DeleteExerciseAsync(exercise);
            Models.Workout workout = await App.WorkoutDAL.GetWorkoutAsync(exercise.WorkoutID);
            await Navigation.PushAsync(new Workout
            {
                BindingContext =  workout as Models.Workout
            });
        }

        async void OnExerciseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ExerciseProgress
                {
                    BindingContext = e.SelectedItem as Models.Exercise
                });
            }
        }
    }
}