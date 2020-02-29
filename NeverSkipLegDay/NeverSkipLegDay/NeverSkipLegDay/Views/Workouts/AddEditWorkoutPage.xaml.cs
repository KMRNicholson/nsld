using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditWorkoutPage : ContentPage
    {
        public AddEditWorkoutPage(ViewModels.WorkoutViewModel workoutViewModel)
        {
            InitializeComponent();
        }

        async void OnSave(object sender, EventArgs e)
        {
            var workout = (Models.Workout)BindingContext;
            workout.Date = DateTime.UtcNow;
            await App.WorkoutDAL.SaveWorkoutAsync(workout);
            await Navigation.PopAsync();
        }
    }
}