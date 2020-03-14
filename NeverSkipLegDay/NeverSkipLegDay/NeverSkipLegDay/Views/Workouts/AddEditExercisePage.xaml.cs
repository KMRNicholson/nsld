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
            var exerciseDal = new ExerciseDal(new SQLiteDB());
            var pageService = new PageService();
            this.BackgroundColor = exerciseViewModel.WorkoutId == 0 ? Color.LightSteelBlue : Color.LightSkyBlue;
            ViewModel = new AddEditExercisePageViewModel(exerciseViewModel, exerciseDal, pageService);
            InitializeComponent();
        }
    }
}