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
            if(exercise.Sets == 0 || exercise.Sets == null)
            {
                exercise.Sets = 1; //Must have atleast 1 set.
            }
            await App.ExerciseDAL.SaveExerciseAsync(exercise);
            for (int i = 0; i < exercise.Sets; i++)
            {
                Set set = new Set();
                set.ExerciseID = exercise.ID;
                set.Reps = 0;
                set.Weight = 0;
                await App.SetDAL.SaveSetAsync(set);
            }
            await Navigation.PopAsync();
        }
    }
}