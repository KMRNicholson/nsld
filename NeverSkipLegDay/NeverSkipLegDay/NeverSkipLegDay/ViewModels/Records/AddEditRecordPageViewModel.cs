using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using System.Globalization;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * Class which defines the ViewModel for the AddEditRecordPage.xaml.cs.
     */
    public class AddEditRecordPageViewModel : BaseViewModel
    {
        #region private properties
        private readonly IRecordDal _recordDal;
        private readonly IPageService _pageService;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public Record Record { get; private set; }
        #endregion

        #region commands
        public ICommand SaveCommand { get; private set; }
        #endregion

        #region constructor
        public AddEditRecordPageViewModel(RecordViewModel record, IRecordDal recordDal, IPageService pageService)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            PageTitle = "RECORD";

            _recordDal = recordDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save().ConfigureAwait(false));

            Record = new Record()
            {
                Id = record.Id,
                ExerciseId = record.ExerciseId,
                Reps = record.Reps,
                Weight = record.Weight
            };
        }
        #endregion

        #region public methods
        // Method which saves a record to the database and sends the saved event using MessagingCenter.
        public async Task Save()
        {
            if (Record.Reps < 1)
            {
                await _pageService.DisplayAlert(DisplayAlerts.Error, DisplayAlerts.NullRepsError, DisplayAlerts.Ok).ConfigureAwait(false);
                return;
            }

            if (Record.Weight <= 0)
            {
                await _pageService.DisplayAlert(DisplayAlerts.Error, DisplayAlerts.NullWeightError, DisplayAlerts.Ok).ConfigureAwait(false);
                return;
            }

            _recordDal.SaveRecord(Record);
            MessagingCenter.Send(this, Events.RecordSaved, Record);
            await _pageService.PopAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
