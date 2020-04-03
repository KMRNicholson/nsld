using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditExercisePage : ContentPage
    {
        public AddEditExercisePageViewModel ViewModel
        {
            get { return BindingContext as AddEditExercisePageViewModel; }
            set { BindingContext = value; }
        }
        public AddEditExercisePage(ExerciseViewModel exerciseViewModel)
        {
            if (exerciseViewModel == null)
                throw new ArgumentNullException(nameof(exerciseViewModel));

            // If the exerciseViewModel's workout id is 0, we know that we must set the color to the records background color. Otherwise, workouts.
            BackgroundColor = exerciseViewModel.WorkoutId == 0 ? Color.LightSteelBlue : Color.LightSkyBlue;

            var exerciseDal = new ExerciseDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new AddEditExercisePageViewModel(exerciseViewModel, exerciseDal, pageService);
            InitializeComponent();
        }
    }
}