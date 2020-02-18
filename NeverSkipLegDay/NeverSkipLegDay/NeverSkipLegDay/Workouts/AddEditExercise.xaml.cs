using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.Workouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditExercise : ContentPage
    {
        public AddEditExercise()
        {
            InitializeComponent();
        }

        async void OnSave(object sender, EventArgs e)
        {
            var exercise = (Models.Exercise)BindingContext;
            exercise.Date = DateTime.UtcNow;
            await App.ExerciseDAL.SaveExerciseAsync(exercise);
            await Navigation.PopAsync();
        }

        async void OnDelete(object sender, EventArgs e)
        {
            var exercise = (Models.Exercise)BindingContext;
            await App.ExerciseDAL.DeleteExerciseAsync(exercise);
            await Navigation.PopAsync();
        }
    }
}