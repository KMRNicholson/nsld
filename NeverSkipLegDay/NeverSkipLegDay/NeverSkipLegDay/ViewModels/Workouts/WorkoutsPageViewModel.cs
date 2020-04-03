using System;
using System.Globalization;
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
    /*
     * The ViewModel for the WorkoutsPage.xaml.cs.
     * Contains all methods for retrieving information from the model
     * and displaying this information to the view.
     */
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
        public string ButtonText { get; private set; }
        public ObservableCollection<WorkoutViewModel> Workouts { get; private set; }
            = new ObservableCollection<WorkoutViewModel>();
        public bool ShowHelpLabel
        {
            get { return _showHelpLabel; }
            set
            {
                SetValue(ref _showHelpLabel, value);
                OnPropertyChanged(nameof(ShowHelpLabel));
            }
        }
        public WorkoutViewModel SelectedWorkout
        {
            get { return _selectedWorkout; }
            set
            {
                SetValue(ref _selectedWorkout, value);
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
            ButtonText = "Add Workout";

            _workoutDal = workoutDal ?? throw new ArgumentNullException(nameof(workoutDal));
            _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddWorkout().ConfigureAwait(false));
            EditCommand = new Command<WorkoutViewModel>(async workout => await EditWorkout(workout).ConfigureAwait(false));
            DeleteCommand = new Command<WorkoutViewModel>(async workout => await DeleteWorkout(workout).ConfigureAwait(false));
            SelectCommand = new Command<WorkoutViewModel>(async workout => await SelectWorkout(workout).ConfigureAwait(false));            
        }
        #endregion

        #region public methods
        // Asynchronous task for deleting a workout from the database and removing it from the list.
        // params: WorkoutViewModel - required for removing the viewmodel from the list, and to delete the record in the database.
        public async Task DeleteWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            string warningMessage = string.Format(new CultureInfo("en-US"), DisplayAlerts.DeleteWarning, workout.Name);

            if (await _pageService.DisplayAlert(DisplayAlerts.Warning, warningMessage, DisplayAlerts.Yes, DisplayAlerts.No).ConfigureAwait(false))
            {
                var workoutModel = _workoutDal.GetWorkout(workout.Id);
                Workouts.Remove(workout);
                _workoutDal.DeleteWorkout(workoutModel);
            }

            ShowHelpLabel = IsWorkoutsEmpty();
        }
        #endregion

        #region private methods
        // Method which returns a list of workouts. If _isDataLoaded is true we do nothing.
        private void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            List<Workout> workouts = _workoutDal.GetWorkouts();
            foreach (var workout in workouts)
            {
                Workouts.Add(new WorkoutViewModel(workout));
            }

            ShowHelpLabel = IsWorkoutsEmpty();
        }

        // Method which is triggered when the WorkoutSaved event occurs. It simply takes the saved workout and updates/adds it to the list.
        // params: AddEditWorkoutPageViewModel - Passed in by the MessagingCenter.subscribe(). This is the source which triggered the event.
        //         Workout - The model which is being saved.
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

            ShowHelpLabel = IsWorkoutsEmpty();
        }

        // Method which sends the user to the page to add a new workout.
        private async Task AddWorkout()
        {
            await _pageService.PushAsync(new AddEditWorkoutPage(new WorkoutViewModel())).ConfigureAwait(false);
        }

        // Method which sends the user to the page to edit a workout.
        // params: WorkoutViewModel - The bounded viewmodel in the list which is being chosen for edit.
        private async Task EditWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            await _pageService.PushAsync(new AddEditWorkoutPage(workout)).ConfigureAwait(false);
        }

        // Method which is triggered when a user selects a workout from the list.
        // params: WorkoutViewModel - The selected item from the WorkoutViewModel list.
        private async Task SelectWorkout(WorkoutViewModel workout)
        {
            if (workout == null) return;

            SelectedWorkout = null;
            await _pageService.PushAsync(new ExercisesPage(workout)).ConfigureAwait(false);
        }

        // Method which checks to see if the workout list is empty, in order to display the help label in the view.
        private bool IsWorkoutsEmpty()
        {
            return Workouts.Count == 0 ? true : false;
        }
        #endregion
    }
}
