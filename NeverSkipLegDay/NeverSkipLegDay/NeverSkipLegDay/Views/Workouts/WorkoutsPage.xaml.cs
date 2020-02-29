using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutsPage : ContentPage
    {
        public WorkoutsPageViewModel ViewModel 
        { 
            get { return BindingContext as WorkoutsPageViewModel; }
            set { BindingContext = value; }
        }
        public WorkoutsPage()
        {
            var workoutDal = new WorkoutDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new WorkoutsPageViewModel(workoutDal, pageService);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadData();
            helpLabel.IsVisible = ViewModel.IsWorkoutsEmpty();
        }

        void OnWorkoutSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectWorkoutCommand.Execute(e.SelectedItem);
        }
    }
}