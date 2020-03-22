using System;
using System.Globalization;
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
     * The ViewModel for the ExercisesPage.xaml.cs.
     * Contains all methods for retrieving information from the model
     * and displaying this information to the view.
     */
    public class ExercisesPageViewModel : BaseViewModel
    {
        #region private properties
        // Holds the data for the selected exercise.
        private ExerciseViewModel _selectedExercise;

        // Used for database operations.
        private readonly IExerciseDal _exerciseDal;

        // Used for page services such as display alerts and navigation.
        private readonly IPageService _pageService;

        // Set to the total reps, used to trigger property changed events for two-way binding.
        private int _repsTotal;

        // Set to the total sets, used to trigger property changed events for two-way binding.
        private int _setsTotal;

        // Set to whether the list of exercises is empty for the selected workout, used to trigger property changed events for two-way binding.
        private bool _showHelpLabel;
        #endregion

        #region public properties
        // Property which is binded to the title in the app bar.
        public string PageTitle { get; private set; }

        // Property which is binded to the text attribute for the Add button in the view.
        public string ButtonText { get; private set; }

        // The viewmodel which contains important information, such as workout id, for the selected workout.
        public WorkoutViewModel Workout { get; private set; }

        // List of viewmodels which are binded as items in the ListView.
        public ObservableCollection<ExerciseViewModel> Exercises { get; private set; }
            = new ObservableCollection<ExerciseViewModel>();

        // Property which is binded to the view to display the total reps for the exercises in the ListView.
        public int RepsTotal
        {
            get { return _repsTotal; }
            set
            {
                SetValue(ref _repsTotal, value);
                OnPropertyChanged(nameof(_repsTotal));
            }
        }

        // Property which is binded to the view to display the total sets for the exercises in the ListView.
        public int SetsTotal
        {
            get { return _setsTotal; }
            set
            {
                SetValue(ref _setsTotal, value);
                OnPropertyChanged(nameof(_setsTotal));
            }
        }

        // Property which is binded to the IsVisible attribute for the HelpLabel in the view.
        public bool ShowHelpLabel
        {
            get { return _showHelpLabel; }
            set
            {
                SetValue(ref _showHelpLabel, value);
                OnPropertyChanged(nameof(ShowHelpLabel));
            }
        }

        // ViewModel for the selected exercise in the ListView.
        public ExerciseViewModel SelectedExercise
        {
            get { return _selectedExercise; }
            set 
            { 
                SetValue(ref _selectedExercise, value); 
            }
        }
        #endregion

        #region commands
        // Command which is set to the LoadData() method, triggered when the page exercises page appears.
        public ICommand LoadDataCommand { get; set; }

        // Command which is set to the AddExercise() method, binded to the "Add" button in the view.
        public ICommand AddCommand { get; set; }

        // Command which is set to the EditExercise() method, binded to the "Edit" button on the viewcell of the list in the view.
        public ICommand EditCommand { get; set; }

        // Command which is set to the DeleteExercise() method, binded to the "Delete" button on the viewcell of the list in the view.
        public ICommand DeleteCommand { get; set; }

        // Command which is set to the SelectExercise() method, which is triggered when the user selects the item in the list in the view.
        public ICommand SelectCommand { get; set; }
        #endregion

        #region constructor
        // Constructor for the ExercisesPageViewModel.
        // params: WorkoutViewModel - required to link the exercises to the selected workout.
        //         IExerciseDal - required for database operations.
        //         IPageService - required for display alerts and navigation between pages.
        public ExercisesPageViewModel(WorkoutViewModel workout, IExerciseDal exerciseDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditExercisePageViewModel, Exercise>
                (this, Events.ExerciseSaved, OnExerciseSaved);

            PageTitle = "EXERCISES";
            ButtonText = "Add Exercise";

            Workout = workout ?? throw new ArgumentNullException(nameof(workout));

            _exerciseDal = exerciseDal ?? throw new ArgumentNullException(nameof(exerciseDal));
            _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddExercise().ConfigureAwait(false));
            EditCommand = new Command<ExerciseViewModel>(async exercise => await EditExercise(exercise).ConfigureAwait(false));
            DeleteCommand = new Command<ExerciseViewModel>(async exercise => await DeleteExercise(exercise).ConfigureAwait(false));
            SelectCommand = new Command<ExerciseViewModel>(async exercise => await SelectExercise(exercise).ConfigureAwait(false));
        }
        #endregion

        #region public methods
        // Asynchronous task for deleting an exercise from the database and removing it from the list.
        // params: ExerciseViewModel - required for removing the viewmodel from the list, and to delete the record in the database.
        public async Task DeleteExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            string warningMessage = string.Format(new CultureInfo("en-US"), DisplayAlerts.DeleteWarning, exercise.Name);

            if (await _pageService.DisplayAlert(DisplayAlerts.Warning, warningMessage, DisplayAlerts.Yes, DisplayAlerts.No).ConfigureAwait(false))
            {
                var exerciseModel = _exerciseDal.GetExercise(exercise.Id);
                Exercises.Remove(exercise);
                _exerciseDal.DeleteExercise(exerciseModel);
            }

            ShowHelpLabel = IsExercisesEmpty();

            SetTotals();
        }
        #endregion

        #region private methods
        // Method which returns a list of exercises associated to the selected workout by using the workout id.
        // We clear the old list of Exercises so that we can recalculate the total Reps and Sets.
        private void LoadData()
        {
            Exercises.Clear();

            var exercises = _exerciseDal.GetExercisesByWorkoutId(Workout.Id);
            foreach (var exercise in exercises)
            {
                Exercises.Add(new ExerciseViewModel(exercise));
            }

            ShowHelpLabel = IsExercisesEmpty();

            SetTotals();
        }

        // Method which is triggered when the ExerciseSaved event occurs. It simply takes the saved exercise and updates/adds it to the list.
        // params: AddEditExercisePageViewModel - Passed in by the MessagingCenter.subscribe(). This is the source which triggered the event.
        //         Exercise - The model which is being saved.
        private void OnExerciseSaved(AddEditExercisePageViewModel source, Exercise exercise)
        {
            ExerciseViewModel exerciseInList = Exercises.Where(e => e.Id == exercise.Id).ToList().FirstOrDefault();

            if(exerciseInList == null)
            {
                Exercises.Add(new ExerciseViewModel(exercise));
            }
            else
            {
                exerciseInList.Id = exercise.Id;
                exerciseInList.Name = exercise.Name;
            }
        }

        // Method which sends the user to the page to add a new exercise. It must at least set the ExerciseViewModel's workout id.
        private async Task AddExercise()
        {
            ExerciseViewModel viewModel = new ExerciseViewModel() { WorkoutId = Workout.Id };
            await _pageService.PushAsync(new AddEditExercisePage(viewModel)).ConfigureAwait(false);
        }

        // Method which sends the user to the page to edit an exercise.
        // params: ExerciseViewModel - The bounded viewmodel in the list which is being chosen for edit.
        private async Task EditExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            await _pageService.PushAsync(new AddEditExercisePage(exercise)).ConfigureAwait(false);
        }

        // Method which is triggered when a user selects an exercise from the list.
        // This method has two branches:
        //     1. If the workout id is not equal to 0, the exercise was created in the workouts feature,
        //        because the workout id will be equal to the id of the selected workout.
        //     2. Else, the exercise was created in the records feature, because the exercise will not be linked
        //        to a selected workout.
        // params: ExerciseViewModel - The selected item from the ExerciseViewModel list.
        private async Task SelectExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            SelectedExercise = null;

            //If the exercise does not have a workout id, it was created in the "Records" feature.
            if (exercise.WorkoutId == 0)
            {
                await _pageService.PushAsync(new RecordsPage(exercise)).ConfigureAwait(false);
            }
            else
            {
                await _pageService.PushAsync(new SetsPage(exercise)).ConfigureAwait(false);
            }
        }

        // Method which checks to see if the exercise list is empty, in order to display the help label in the view.
        private bool IsExercisesEmpty()
        {
            return Exercises.Count == 0 ? true : false;
        }

        // Method which gets the total reps and sets for all the exercises to display in the view.
        private void SetTotals()
        {
            RepsTotal = Exercises.Select(x => x.Reps).Sum();
            SetsTotal = Exercises.Select(x => x.Sets).Sum();
        }
        #endregion
    }
}
