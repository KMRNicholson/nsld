using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using System.Globalization;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * The ViewModel for the SetsPage.xaml.cs.
     * Contains all methods for retrieving information from the model
     * and displaying this information to the view.
     */
    public class SetsPageViewModel : BaseViewModel
    {
        #region private properties
        private ExerciseViewModel _exercise;
        private readonly ISetDal _setDal;
        private readonly IPageService _pageService;
        private bool _isDataLoaded;
        private bool _showHelpLabel;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public string ButtonText { get; private set; }
        public ObservableCollection<SetViewModel> Sets { get; private set; }
            = new ObservableCollection<SetViewModel>();
        public ExerciseViewModel Exercise
        {
            get { return _exercise; }
            set
            {
                SetValue(ref _exercise, value);
                OnPropertyChanged(nameof(Exercise));
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
        #endregion

        #region commands
        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand BatchSaveCommand { get; set; }
        #endregion

        #region constructors
        // Constructor for the SetsPageViewModel.
        // params: ExerciseViewModel - required to link the sets to the selected exercise.
        //         ISetDal - required for database operations.
        //         IPageService - required for display alerts and navigation between pages.
        public SetsPageViewModel(ExerciseViewModel exercise, ISetDal setDal, IPageService pageService)
        {
            PageTitle = "SETS";
            ButtonText = "Add Set";

            Exercise = exercise ?? throw new ArgumentNullException(nameof(exercise));

            _setDal = setDal ?? throw new ArgumentNullException(nameof(exercise));
            _pageService = pageService ?? throw new ArgumentNullException(nameof(exercise));

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(() => AddSet());
            EditCommand = new Command<SetViewModel>(set => EditSet(set));
            DeleteCommand = new Command<SetViewModel>(async set => await DeleteSet(set).ConfigureAwait(false));
            BatchSaveCommand = new Command(async () => await BatchSave().ConfigureAwait(false));
        }
        #endregion

        #region public methods
        // Asynchronous task for deleting a set from the database and removing it from the list.
        // params: SetViewModel - required for removing the viewmodel from the list, and to delete the record in the database.
        public async Task DeleteSet(SetViewModel set)
        {
            if (set == null) return;

            string warningMessage = string.Format(new CultureInfo("en-US"), DisplayAlerts.DeleteWarning, nameof(set));

            if (await _pageService.DisplayAlert(DisplayAlerts.Warning, warningMessage, DisplayAlerts.Yes, DisplayAlerts.No).ConfigureAwait(false))
            {
                var setModel = _setDal.GetSet(set.Id);
                Sets.Remove(set);
                _setDal.DeleteSet(setModel);
            }

            ShowHelpLabel = IsSetsEmpty();

            SetTotals();
        }
        #endregion

        #region private methods
        // Method which returns a list of sets associated to the selected exercise by using the exercise id.
        private void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var sets = _setDal.GetSetsByExerciseId(Exercise.Id);
            foreach (var set in sets)
            {
                Sets.Add(new SetViewModel(set));
            }

            ShowHelpLabel = IsSetsEmpty();

            SetTotals();
        }

        // Method which adds and saves a new set to the list and database.
        private void AddSet()
        {
            Set set = new Set() { ExerciseId = Exercise.Id };
            _setDal.SaveSet(set);
            Sets.Add(new SetViewModel(set));

            ShowHelpLabel = IsSetsEmpty();

            SetTotals();
        }

        // Method which edits a set in the list and saves it to the database.
        private void EditSet(SetViewModel set)
        {
            if (set == null) return;

            Set existingSet = _setDal.GetSet(set.Id);
            existingSet.Reps = set.Reps;
            existingSet.Weight = set.Weight;
            _setDal.SaveSet(existingSet);

            var setInList = Sets.Single(s => s.Id == set.Id);
            setInList.Reps = set.Reps;
            setInList.Weight = set.Weight;
        }

        // Method which saves a batch of sets, by recursively calling the EditSet method for every set in the ListView.
        private async Task BatchSave()
        {
            foreach (SetViewModel set in Sets)
            {
                EditSet(set);
            }

            await _pageService.DisplayAlert(DisplayAlerts.Saved, DisplayAlerts.SetsSaved, DisplayAlerts.Ok).ConfigureAwait(false);
            
            SetTotals();
        }

        // Method which checks to see if the set list is empty, in order to display the help label in the view.
        private bool IsSetsEmpty()
        {
            return Sets.Count == 0 ? true : false;
        }

        // Method which gets the total reps and sets for the exercise to display in the view.
        private void SetTotals()
        {
            //TODO: Make this logic better.
            var exerciseDal = new ExerciseDal(new SQLiteDB());
            var exercise = exerciseDal.GetExercise(Exercise.Id);

            Exercise = exercise != null ? new ExerciseViewModel(exercise) : Exercise;
        }
        #endregion
    }
}
