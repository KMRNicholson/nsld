using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditWorkoutPage : ContentPage
    {
        public AddEditWorkoutPageViewModel ViewModel
        {
            get { return BindingContext as AddEditWorkoutPageViewModel; }
            set { BindingContext = value; }
        }
        public AddEditWorkoutPage(WorkoutViewModel workoutViewModel)
        {
            var workoutDal = new WorkoutDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new AddEditWorkoutPageViewModel(workoutViewModel, workoutDal, pageService);
            InitializeComponent();
        }
    }
}