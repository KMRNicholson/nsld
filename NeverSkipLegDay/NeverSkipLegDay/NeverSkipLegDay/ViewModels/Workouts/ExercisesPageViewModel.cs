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
    public class ExercisesPageViewModel : BaseViewModel
    {
        private ExerciseViewModel _selectedExercise;
        private IExerciseDal _exerciseDal;
        private IPageService _pageService;

        private bool _isDataLoaded;
        
        private int? _repsTotal;
        public int? RepsTotal
        {
            get { return _repsTotal; }
            set
            {
                SetValue(ref _repsTotal, value);
                OnPropertyChanged(nameof(_repsTotal));
            }
        }

        private int _setsTotal;
        public int SetsTotal
        {
            get { return _setsTotal; }
            set
            {
                SetValue(ref _setsTotal, value);
                OnPropertyChanged(nameof(_setsTotal));
            }
        }

        public WorkoutViewModel Workout { get; private set; }

        public string AddButtonText
        {
            get { return "Add Exercise"; }
        }

        public ObservableCollection<ExerciseViewModel> Exercises { get; private set; }
            = new ObservableCollection<ExerciseViewModel>();

        public ExerciseViewModel SelectedExercise
        {
            get { return _selectedExercise; }
            set { SetValue(ref _selectedExercise, value); }
        }

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        public ExercisesPageViewModel(WorkoutViewModel workout, IExerciseDal exerciseDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditExercisePageViewModel, Exercise>
                (this, Events.ExerciseSaved, OnExerciseSaved);

            _exerciseDal = exerciseDal;
            _pageService = pageService;

            Workout = workout;

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddExercise());
            EditCommand = new Command<ExerciseViewModel>(async exercise => await EditExercise(exercise));
            DeleteCommand = new Command<ExerciseViewModel>(async exercise => await DeleteExercise(exercise));
            SelectCommand = new Command<ExerciseViewModel>(async exercise => await SelectExercise(exercise));
        }

        private void OnExerciseSaved(AddEditExercisePageViewModel source, Exercise exercise)
        {
            ExerciseViewModel exerciseInList = Exercises.Where(e => e.Id == exercise.Id).ToList().FirstOrDefault();

            SetTotals();    

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
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var exercises = _exerciseDal.GetExercisesByWorkoutId(Workout.Id);
            foreach (var exercise in exercises)
            {
                Exercises.Add(new ExerciseViewModel(exercise));
            }
        }

        private async Task AddExercise()
        {
            await _pageService.PushAsync(new AddEditExercisePage(new ExerciseViewModel() { WorkoutId = Workout.Id }));
        }

        private async Task EditExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            await _pageService.PushAsync(new AddEditExercisePage(exercise));
        }

        public async Task DeleteExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            if(await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {exercise.Name}?", "Yes", "No"))
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
                await _pageService.PushAsync(new RecordsPage(exercise));
            }
            else
            {
                await _pageService.PushAsync(new SetsPage(exercise));
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
