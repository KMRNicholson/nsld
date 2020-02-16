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
            
            listView.ItemsSource = await App.ExerciseDAL.GetExercisesAsync();
        }

        async void OnAddOrEdit(object sender, EventArgs e)
        {
            var id = ((Button)sender).BindingContext;
            if (Int32.TryParse(id.ToString(), out int result))
            {
                Models.Exercise exercise = await App.ExerciseDAL.GetExerciseAsync((int)id);
                await Navigation.PushAsync(new AddEditExercise
                {
                    BindingContext = exercise as Models.Exercise
                });
            }
            else
            {
                await Navigation.PushAsync(new AddEditExercise
                {
                    BindingContext = new Models.Exercise()
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