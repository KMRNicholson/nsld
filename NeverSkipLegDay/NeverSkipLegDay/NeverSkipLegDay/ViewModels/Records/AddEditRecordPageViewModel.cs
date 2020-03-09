using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class AddEditRecordPageViewModel : BaseViewModel
    {
        private IRecordDal _recordDal;
        private IPageService _pageService;

        public Record Record { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public AddEditRecordPageViewModel(RecordViewModel record, IRecordDal recordDal, IPageService pageService)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            _recordDal = recordDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());

            Record = new Record()
            {
                Id = record.Id,
                Name = record.Name,
                Reps = record.Reps,
                Weight = record.Weight
            };
        }

        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Record.Name))
            {
                await _pageService.DisplayAlert("Error", "Please enter a name.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Record.Reps.ToString()))
            {
                await _pageService.DisplayAlert("Error", "Please enter reps.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Record.Weight.ToString()))
            {
                await _pageService.DisplayAlert("Error", "Please enter a weight.", "OK");
                return;
            }

            _recordDal.SaveRecord(Record);
            MessagingCenter.Send(this, Events.RecordSaved, Record);
            await _pageService.PopAsync();
        }
    }
}
