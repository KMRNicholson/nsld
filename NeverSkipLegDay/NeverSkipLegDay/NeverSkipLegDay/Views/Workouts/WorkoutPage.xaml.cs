using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        public WorkoutPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Models.Workout workout = (Models.Workout)this.BindingContext;
            listView.ItemsSource = await App.ExerciseDAL.GetExercisesByWorkoutIdAsync(workout.ID);
            List<Models.Exercise> exercises = (List<Models.Exercise>)listView.ItemsSource;
            helpLabel.IsVisible = exercises.Count == 0 ? true : false;
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

                await Navigation.PushAsync(new AddExercise
                {
                    BindingContext = exercise as Models.Exercise
                });
            }
            else
            {
                exercise = await App.ExerciseDAL.GetExerciseAsync(id);

                await Navigation.PushAsync(new EditExercise
                {
                    BindingContext = exercise as Models.Exercise
                });
            }
        }
        async void OnDelete(object sender, EventArgs e)
        {
            var id = ((Button)sender).BindingContext;
            Models.Exercise exercise = await App.ExerciseDAL.GetExerciseAsync((int)id);
            await App.ExerciseDAL.DeleteExerciseAsync(exercise);
            listView.ItemsSource = await App.ExerciseDAL.GetExercisesByWorkoutIdAsync((int)id);
            List<Models.Exercise> exercises = (List<Models.Exercise>)listView.ItemsSource;
            helpLabel.IsVisible = exercises.Count == 0 ? true : false;
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