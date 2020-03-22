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
    public class ExercisesPageViewModel : BaseViewModel
    {
        #region private properties
        private ExerciseViewModel _selectedExercise;
        private readonly IExerciseDal _exerciseDal;
        private readonly IPageService _pageService;
        private int _repsTotal;
        private int _setsTotal;
        private bool _showHelpLabel;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public string ButtonText { get; private set; }
        public WorkoutViewModel Workout { get; private set; }
        public ObservableCollection<ExerciseViewModel> Exercises { get; private set; }
            = new ObservableCollection<ExerciseViewModel>();
        public int RepsTotal
        {
            get { return _repsTotal; }
            set
            {
                SetValue(ref _repsTotal, value);
                OnPropertyChanged(nameof(_repsTotal));
            }
        }
        public int SetsTotal
        {
            get { return _setsTotal; }
            set
            {
                SetValue(ref _setsTotal, value);
                OnPropertyChanged(nameof(_setsTotal));
            }
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
        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        #endregion

        #region constructor
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

        private async Task AddExercise()
        {
            ExerciseViewModel viewModel = new ExerciseViewModel() { WorkoutId = Workout.Id };
            await _pageService.PushAsync(new AddEditExercisePage(viewModel)).ConfigureAwait(false);
        }

        private async Task EditExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            await _pageService.PushAsync(new AddEditExercisePage(exercise)).ConfigureAwait(false);
        }

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

        private bool IsExercisesEmpty()
        {
            return Exercises.Count == 0 ? true : false;
        }

        private void SetTotals()
        {
            RepsTotal = Exercises.Select(x => x.Reps).Sum();
            SetsTotal = Exercises.Select(x => x.Sets).Sum();
        }
        #endregion
    }
}
