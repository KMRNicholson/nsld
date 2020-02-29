using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.Views;
using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class WorkoutsPageViewModel : BaseViewModel
    {
        private WorkoutViewModel _selectedWorkout;
        private WorkoutDal _workoutDal;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public ObservableCollection<WorkoutViewModel> Workouts { get; private set; }

        public WorkoutViewModel SelectedWorkout
        {
            get { return _selectedWorkout; }
            set { SetValue(ref _selectedWorkout, value); }
        }

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddWorkoutCommand { get; set; }
        public ICommand EditWorkoutCommand { get; set; }
        public ICommand DeleteWorkoutCommand { get; set; }
        public ICommand SelectedWorkoutCommand { get; set; }

        public WorkoutsPageViewModel(WorkoutDal workoutDal, IPageService pageService)
        {
            _workoutDal = workoutDal;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddWorkoutCommand = new Command(async () => await AddWorkout());
            EditWorkoutCommand = new Command<WorkoutViewModel>(async workout => await EditWorkout(workout));
            DeleteWorkoutCommand = new Command<WorkoutViewModel>(async workout => await DeleteWorkout(workout));
        }

        private async Task LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var workouts = await _workoutDal.GetWorkoutsAsync();
            foreach(var workout in workouts)
            {
                Workouts.Add(new WorkoutViewModel(workout));
            }
        }

        private async Task AddWorkout()
        {
            await _pageService.PushAsync(new AddEditWorkoutPage(new WorkoutViewModel()));
        }

        private async Task EditWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            await _pageService.PushAsync(new AddEditWorkoutPage(workout));
        }

        private async Task DeleteWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            var workoutModel = await _workoutDal.GetWorkoutAsync(workout.Id);
            await _workoutDal.DeleteWorkoutAsync(workoutModel);
        }
    }
}
