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

        public ObservableCollection<ExerciseViewModel> Exercises { get; private set; }

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
    
        public ExercisesPageViewModel()
        {
            //MessagingCenter.Subscribe<>
        }

        public ExercisesPageViewModel(ExerciseDal exerciseDal, IPageService pageService)
        {
            _exerciseDal = exerciseDal;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddExerciseCommand = new Command(async () => await AddExercise());
            EditExerciseCommand = new Command<ExerciseViewModel>(async exercise => await EditExercise(exercise));
            DeleteExerciseCommand = new Command<ExerciseViewModel>(async exercise => await DeleteExercise(exercise));
            SelectExerciseCommand = new Command<ExerciseViewModel>(async exercise => await SelectExercise(exercise));
        }

        private async Task LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var exercises = await _exerciseDal.GetExercisesAsync();
            foreach (var exercise in exercises)
            {
                Exercises.Add(new ExerciseViewModel(exercise));
            }
        }

        private async Task AddExercise()
        {
            await _pageService.PushAsync(new AddEditExercisePage(new ExerciseViewModel()));
        }

        private async Task EditExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            await _pageService.PushAsync(new AddEditExercisePage(exercise));
        }

        private async Task DeleteExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            var exerciseModel = await _exerciseDal.GetExerciseAsync(exercise.Id);
            await _exerciseDal.DeleteExerciseAsync(exerciseModel);
        }

        private async Task SelectExercise(ExerciseViewModel exercise)
        {
            if (exercise == null) return;

            await _pageService.PushAsync(new ExercisesPage());
        }
    }
}
