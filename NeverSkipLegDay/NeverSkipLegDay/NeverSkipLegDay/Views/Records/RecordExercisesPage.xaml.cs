using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordExercisesPage : ContentPage
    {
        public ExercisesPageViewModel ViewModel
        {
            get { return BindingContext as ExercisesPageViewModel; }
            set { BindingContext = value; }
        }
        public RecordExercisesPage(WorkoutViewModel workout)
        {
            var workoutDal = new ExerciseDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new ExercisesPageViewModel(workout, workoutDal, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
            helpLabel.IsVisible = ViewModel.IsExercisesEmpty();
        }

        void OnExerciseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectCommand.Execute(e.SelectedItem);
        }
    }
}