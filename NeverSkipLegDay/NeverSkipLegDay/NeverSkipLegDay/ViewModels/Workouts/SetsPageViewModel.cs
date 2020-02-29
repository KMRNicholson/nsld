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
    public class SetsPageViewModel : BaseViewModel
    {
        private SetDal _setDal;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public ExerciseViewModel Exercise { get; private set; }

        public ObservableCollection<SetViewModel> Sets { get; private set; }
            = new ObservableCollection<SetViewModel>();

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddSetCommand { get; set; }
        public ICommand EditSetCommand { get; set; }
        public ICommand DeleteSetCommand { get; set; }

        public ICommand BatchSaveCommand { get; set; }

        public SetsPageViewModel(ExerciseViewModel exercise, SetDal setDal, IPageService pageService)
        {
            _setDal = setDal;
            _pageService = pageService;

            Exercise = exercise;

            LoadDataCommand = new Command(async () => await LoadData());
            AddSetCommand = new Command(async () => await AddSet());
            EditSetCommand = new Command<SetViewModel>(async set => await EditSet(set));
            DeleteSetCommand = new Command<SetViewModel>(async set => await DeleteSet(set));
        }

        private async Task LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var sets = await _setDal.GetSetsByExerciseIdAsync(Exercise.Id);
            foreach (var set in sets)
            {
                Sets.Add(new SetViewModel(set));
            }
        }

        private async Task AddSet()
        {
            Set set = new Set() { ExerciseId = Exercise.Id };
            await _setDal.SaveSetAsync(set);
            Sets.Add(new SetViewModel(set));   
        }

        private async Task EditSet(SetViewModel set)
        {
            if (set == null) return;

            Set existingSet = await _setDal.GetSetAsync(set.Id);
            existingSet.Reps = set.Reps;
            existingSet.Weight = set.Weight;
            await _setDal.SaveSetAsync(existingSet);

            var setInList = Sets.Single(s => s.Id == set.Id);
            setInList.Reps = set.Reps;
            setInList.Weight = set.Weight;
        }

        private async Task DeleteSet(SetViewModel set)
        {
            if (set == null) return;
            
            if(await _pageService.DisplayAlert("Warning", "Are you sure you want to delete the set?", "Yes", "No"))
            {
                var setModel = await _setDal.GetSetAsync(set.Id);
                Sets.Remove(set);
                await _setDal.DeleteSetAsync(setModel);
            }
        }
    }
}
