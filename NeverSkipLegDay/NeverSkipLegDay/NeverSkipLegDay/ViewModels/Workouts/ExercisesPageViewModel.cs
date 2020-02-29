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
        private ExerciseDal _exerciseDal;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public WorkoutViewModel Workout { get; private set; }

        public ObservableCollection<ExerciseViewModel> Exercises { get; private set; }
            = new ObservableCollection<ExerciseViewModel>();

        public ExerciseViewModel SelectedExercise
        {
            get { return _selectedExercise; }
            set { SetValue(ref _selectedExercise, value); }
        }

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddExerciseCommand { get; set; }
        public ICommand EditExerciseCommand { get; set; }
        public ICommand DeleteExerciseCommand { get; set; }
        public ICommand SelectExerciseCommand { get; set; }

        public ExercisesPageViewModel(WorkoutViewModel workout, ExerciseDal exerciseDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditExercisePageViewModel, Exercise>
                (this, Events.ExerciseSaved, OnExerciseSaved);

            _exerciseDal = exerciseDal;
            _pageService = pageService;

            Workout = workout;

            LoadDataCommand = new Command(async () => await LoadData());
            AddExerciseCommand = new Command(async () => await AddExercise());
            EditExerciseCommand = new Command<ExerciseViewModel>(async exercise => await EditExercise(exercise));
            DeleteExerciseCommand = new Command<ExerciseViewModel>(async exercise => await DeleteExercise(exercise));
            SelectExerciseCommand = new Command<ExerciseViewModel>(async exercise => await SelectExercise(exercise));
        }

        public void OnExerciseSaved(AddEditExercisePageViewModel source, Exercise exercise)
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

        private async Task LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var exercises = await _exerciseDal.GetExercisesByWorkoutIdAsync(Workout.Id);
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

        private async Task DeleteExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            if(await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {exercise.Name}?", "Yes", "No"))
            {
                var exerciseModel = await _exerciseDal.GetExerciseAsync(exercise.Id);
                Exercises.Remove(exercise);
                await _exerciseDal.DeleteExerciseAsync(exerciseModel);
            }
        }

        private async Task SelectExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            SelectedExercise = null;
            await _pageService.PushAsync(new SetsPage(exercise));
        }
    }
}
