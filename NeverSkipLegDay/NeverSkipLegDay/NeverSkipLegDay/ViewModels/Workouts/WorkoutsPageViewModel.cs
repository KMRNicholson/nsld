using System.Collections.Generic;
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
        private IWorkoutDal _workoutDal;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public string AddButtonText
        {
            get { return "Add Workout"; }
        }

        public ObservableCollection<WorkoutViewModel> Workouts { get; private set; }
            = new ObservableCollection<WorkoutViewModel>();

        public WorkoutViewModel SelectedWorkout
        {
            get { return _selectedWorkout; }
            set { SetValue(ref _selectedWorkout, value); }
        }

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        public WorkoutsPageViewModel(IWorkoutDal workoutDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditWorkoutPageViewModel, Workout>
                (this, Events.WorkoutSaved, OnWorkoutSaved);

            _workoutDal = workoutDal;
            _pageService = pageService;

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddWorkout());
            EditCommand = new Command<WorkoutViewModel>(async workout => await EditWorkout(workout));
            DeleteCommand = new Command<WorkoutViewModel>(async workout => await DeleteWorkout(workout));
            SelectCommand = new Command<WorkoutViewModel>(async workout => await SelectWorkout(workout));            
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

        public void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            List<Workout> workouts = _workoutDal.GetWorkouts();
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

        public async Task DeleteWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            if(await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {workout.Name}?", "Yes", "No"))
            {
                var workoutModel = _workoutDal.GetWorkout(workout.Id);
                Workouts.Remove(workout);
                _workoutDal.DeleteWorkout(workoutModel);
            }
        }

        public async Task SelectWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            SelectedWorkout = null;
            await _pageService.PushAsync(new ExercisesPage(workout));
        }

        public bool IsWorkoutsEmpty()
        {
            return Workouts.Count == 0 ? true : false;
        }
    }
}
