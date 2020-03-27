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
     * The ViewModel for the RecordsPage.xaml.cs.
     * Contains all methods for retrieving information from the model
     * and displaying this information to the view.
     */
    public class RecordsPageViewModel : BaseViewModel
    {
        #region private properties
        private ExerciseViewModel _exercise;
        private readonly IRecordDal _recordDal;
        private readonly IPageService _pageService;
        private bool _isDataLoaded;
        private bool _showHelpLabel;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public string ButtonText { get; private set; }
        public ObservableCollection<RecordViewModel> Records { get; private set; }
            = new ObservableCollection<RecordViewModel>();
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
        #endregion

        #region constructors
        public RecordsPageViewModel(ExerciseViewModel exercise, IRecordDal recordDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditRecordPageViewModel, Record>
                (this, Events.RecordSaved, OnRecordSaved);

            PageTitle = "RECORDS";
            ButtonText = "Add Record";

            Exercise = exercise;

            _recordDal = recordDal;
            _pageService = pageService;

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddRecord().ConfigureAwait(false));
            EditCommand = new Command<RecordViewModel>(async record => await EditRecord(record).ConfigureAwait(false));
            DeleteCommand = new Command<RecordViewModel>(async record => await DeleteRecord(record).ConfigureAwait(false));
        }
        #endregion

        #region public methods
        // Asynchronous task for deleting a record from the database and removing it from the list.
        // params: RecordViewModel - required for removing the viewmodel from the list, and to delete the record in the database.
        public async Task DeleteRecord(RecordViewModel record)
        {
            if (record == null) return;

            string warningMessage = string.Format(new CultureInfo("en-US"), DisplayAlerts.DeleteWarning, nameof(record));

            if (await _pageService.DisplayAlert(DisplayAlerts.Warning, warningMessage, DisplayAlerts.Yes, DisplayAlerts.No).ConfigureAwait(false))
            {
                var recordModel = _recordDal.GetRecord(record.Id);
                Records.Remove(record);
                _recordDal.DeleteRecord(recordModel);
            }

            ShowHelpLabel = IsRecordsEmpty();
        }
        #endregion

        #region private methods
        // Method which returns a list of records associated to the selected exercise by using the exercise id.
        private void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            List<Record> records = _recordDal.GetRecordsByExerciseId(Exercise.Id);
            foreach (var record in records)
            {
                Records.Add(new RecordViewModel(record));
            }

            ShowHelpLabel = IsRecordsEmpty();
        }

        // Method which is triggered by saving a record to the database. Updates the list.
        private void OnRecordSaved(AddEditRecordPageViewModel source, Record record)
        {
            RecordViewModel recordInList = Records.Where(w => w.Id == record.Id).ToList().FirstOrDefault();

            if (recordInList == null)
            {
                Records.Add(new RecordViewModel(record));
            }
            else
            {
                recordInList.Id = record.Id;
                recordInList.Reps = record.Reps;
                recordInList.Weight = record.Weight;
            }
        }

        // Method which adds and saves a new record to the list and database.
        private async Task AddRecord()
        {
            await _pageService.PushAsync(new AddEditRecordPage(new RecordViewModel() { ExerciseId = Exercise.Id })).ConfigureAwait(false);
        }

        // Method which edits and saves a record to the list and database.
        private async Task EditRecord(RecordViewModel record)
        {
            if (record == null) return;

            await _pageService.PushAsync(new AddEditRecordPage(record)).ConfigureAwait(false);
        }

        // Method which checks to see if the record list is empty, in order to display the help label in the view.
        private bool IsRecordsEmpty()
        {
            return Records.Count == 0 ? true : false;
        }
        #endregion
    }
}
