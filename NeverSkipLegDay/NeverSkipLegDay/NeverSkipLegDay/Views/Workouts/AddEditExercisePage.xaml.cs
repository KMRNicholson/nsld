using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditExercisePage : ContentPage
    {
        public AddEditExercisePage(ExerciseViewModel exerciseViewModel)
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
                Set set = new Set
                {
                    ExerciseID = exercise.ID,
                    Reps = 0,
                    Weight = 0
                };
                await App.SetDAL.SaveSetAsync(set);
            }
            await Navigation.PopAsync();
            
        }
    }
}