using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.Views;
using System;

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

        private void OnExerciseSaved(AddEditExercisePageViewModel source, Exercise exercise)
        {
            SetTotals();

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

        public void LoadData()
        {
            Exercises.Clear();
            var exercises = _exerciseDal.GetExercisesByWorkoutId(Workout.Id);
            foreach (var exercise in exercises)
            {
                Exercises.Add(new ExerciseViewModel(exercise));
            }

            SetTotals();
        }

        private async Task AddExercise()
        {
            await _pageService.PushAsync(new AddEditExercisePage(new ExerciseViewModel() { WorkoutId = Workout.Id })).ConfigureAwait(false);
        }

        private async Task EditExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            await _pageService.PushAsync(new AddEditExercisePage(exercise)).ConfigureAwait(false);
        }

        public async Task DeleteExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            if(await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {exercise.Name}?", "Yes", "No").ConfigureAwait(false))
            {
                var exerciseModel = _exerciseDal.GetExercise(exercise.Id);
                Exercises.Remove(exercise);
                _exerciseDal.DeleteExercise(exerciseModel);
            }

            SetTotals();
        }

        private async Task SelectExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            SelectedExercise = null;

            if (exercise.WorkoutId == 0)
            {
                await _pageService.PushAsync(new RecordsPage(exercise)).ConfigureAwait(false);
            }
            else
            {
                await _pageService.PushAsync(new SetsPage(exercise)).ConfigureAwait(false);
            }
        }

        public bool IsExercisesEmpty()
        {
            return Exercises.Count == 0 ? true : false;
        }

        public void SetTotals()
        {
            RepsTotal = Exercises.Select(x => x.Reps).Sum();
            SetsTotal = Exercises.Select(x => x.Sets).Sum();
        }
    }
}
