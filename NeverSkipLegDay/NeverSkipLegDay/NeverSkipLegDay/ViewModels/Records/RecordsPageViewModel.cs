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
    public class RecordsPageViewModel : BaseViewModel
    {
        private IRecordDal _recordDal;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public string AddButtonText
        {
            get { return "Add Record"; }
        }
        public ExerciseViewModel Exercise { get; private set; }

        public ObservableCollection<RecordViewModel> Records { get; private set; }
            = new ObservableCollection<RecordViewModel>();

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public RecordsPageViewModel(ExerciseViewModel exercise, IRecordDal recordDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditRecordPageViewModel, Record>
                (this, Events.RecordSaved, OnRecordSaved);

            _recordDal = recordDal;
            _pageService = pageService;

            Exercise = exercise;

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddRecord());
            EditCommand = new Command<RecordViewModel>(async record => await EditRecord(record));
            DeleteCommand = new Command<RecordViewModel>(async record => await DeleteRecord(record));
        }

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

        public void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            List<Record> records = _recordDal.GetRecordsByExerciseId(Exercise.Id);
            foreach (var record in records)
            {
                Records.Add(new RecordViewModel(record));
            }
        }

        private async Task AddRecord()
        {
            await _pageService.PushAsync(new AddEditRecordPage(new RecordViewModel() { ExerciseId = Exercise.Id }));
        }

        private async Task EditRecord(RecordViewModel record)
        {
            if (record == null) return;

            await _pageService.PushAsync(new AddEditRecordPage(record));
        }

        public async Task DeleteRecord(RecordViewModel record)
        {
            if (record == null) return;

            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete this record?", "Yes", "No"))
            {
                var recordModel = _recordDal.GetRecord(record.Id);
                Records.Remove(record);
                _recordDal.DeleteRecord(recordModel);
            }
        }

        public bool IsRecordsEmpty()
        {
            return Records.Count == 0 ? true : false;
        }
    }
}
