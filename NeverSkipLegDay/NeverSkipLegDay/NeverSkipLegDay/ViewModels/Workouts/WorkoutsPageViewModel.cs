using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models;
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
            = new ObservableCollection<WorkoutViewModel>();

        public WorkoutViewModel SelectedWorkout
        {
            get { return _selectedWorkout; }
            set { SetValue(ref _selectedWorkout, value); }
        }

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddWorkoutCommand { get; set; }
        public ICommand EditWorkoutCommand { get; set; }
        public ICommand DeleteWorkoutCommand { get; set; }
        public ICommand SelectWorkoutCommand { get; set; }

        public WorkoutsPageViewModel()
        {
            
        }

        public WorkoutsPageViewModel(WorkoutDal workoutDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditWorkoutPageViewModel, Workout>
                (this, Events.WorkoutSaved, OnWorkoutSaved);

            _workoutDal = workoutDal;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddWorkoutCommand = new Command(async () => await AddWorkout());
            EditWorkoutCommand = new Command<WorkoutViewModel>(async workout => await EditWorkout(workout));
            DeleteWorkoutCommand = new Command<WorkoutViewModel>(async workout => await DeleteWorkout(workout));
            SelectWorkoutCommand = new Command<WorkoutViewModel>(async workout => await SelectWorkout(workout));
        }

        private void OnWorkoutSaved(AddEditWorkoutPageViewModel source, Workout workout)
        {
            WorkoutViewModel workoutInList = Workouts.Where(w => w.Id == workout.Id).ToList().FirstOrDefault();

            if(workoutInList == null)
            {
                Workouts.Add(new WorkoutViewModel(workout));
            }
            else
            {
                workoutInList.Id = workout.Id;
                workoutInList.Name = workout.Name;
            }
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
            Workouts.Remove(workout);
            await _workoutDal.DeleteWorkoutAsync(workoutModel);
        }

        private async Task SelectWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            await _pageService.PushAsync(new ExercisesPage(workout));
        }
    }
}
