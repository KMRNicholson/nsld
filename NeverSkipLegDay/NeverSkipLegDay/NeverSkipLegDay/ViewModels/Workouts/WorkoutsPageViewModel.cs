using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.Views;

namespace NeverSkipLegDay.ViewModels
{
    public class WorkoutsPageViewModel : BaseViewModel
    {
        #region private properties
        private WorkoutViewModel _selectedWorkout;
        private readonly IWorkoutDal _workoutDal;
        private readonly IPageService _pageService;
        private bool _isDataLoaded;
        private bool _showHelpLabel;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public string AddButtonText { get; private set; }
        public ObservableCollection<WorkoutViewModel> Workouts { get; private set; }
            = new ObservableCollection<WorkoutViewModel>();
        public WorkoutViewModel SelectedWorkout
        {
            get { return _selectedWorkout; }
            set { SetValue(ref _selectedWorkout, value); }
        }
        public bool ShowHelpLabel
        {
            get { return _showHelpLabel; }
            set
            {
                SetValue(ref _showHelpLabel, value);
                OnPropertyChanged(nameof(ShowHelpLabel));
            }
        }
        #endregion

        #region commands
        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        #endregion

        #region constructor
        public WorkoutsPageViewModel(IWorkoutDal workoutDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditWorkoutPageViewModel, Workout>
                (this, Events.WorkoutSaved, OnWorkoutSaved);

            PageTitle = "WORKOUTS";
            AddButtonText = "Add Workouts";

            _workoutDal = workoutDal;
            _pageService = pageService;

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddWorkout().ConfigureAwait(false));
            EditCommand = new Command<WorkoutViewModel>(async workout => await EditWorkout(workout).ConfigureAwait(false));
            DeleteCommand = new Command<WorkoutViewModel>(async workout => await DeleteWorkout(workout).ConfigureAwait(false));
            SelectCommand = new Command<WorkoutViewModel>(async workout => await SelectWorkout(workout).ConfigureAwait(false));            
        }
        #endregion

        #region public methods
        public void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            List<Workout> workouts = _workoutDal.GetWorkouts();
            foreach (var workout in workouts)
            {
                Workouts.Add(new WorkoutViewModel(workout));
            }
        }
        public async Task DeleteWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {workout.Name}?", "Yes", "No").ConfigureAwait(false))
            {
                var workoutModel = _workoutDal.GetWorkout(workout.Id);
                Workouts.Remove(workout);
                _workoutDal.DeleteWorkout(workoutModel);
            }
        }
        public bool IsWorkoutsEmpty()
        {
            return Workouts.Count == 0 ? true : false;
        }
        #endregion

        #region private methods
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
        private async Task AddWorkout()
        {
            await _pageService.PushAsync(new AddEditWorkoutPage(new WorkoutViewModel())).ConfigureAwait(false);
        }
        private async Task EditWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            await _pageService.PushAsync(new AddEditWorkoutPage(workout)).ConfigureAwait(false);
        }
        private async Task SelectWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            SelectedWorkout = null;
            await _pageService.PushAsync(new ExercisesPage(workout)).ConfigureAwait(false);
        }
        #endregion

    }
}
