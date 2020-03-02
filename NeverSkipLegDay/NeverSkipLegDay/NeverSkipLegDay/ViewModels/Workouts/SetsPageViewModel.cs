﻿using System.Collections.ObjectModel;
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
        private ISetDal _setDal;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public string AddButtonText
        {
            get { return "Add Set"; }
        }

        public ExerciseViewModel Exercise { get; private set; }

        public ObservableCollection<SetViewModel> Sets { get; private set; }
            = new ObservableCollection<SetViewModel>();

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand BatchSaveCommand { get; set; }

        public SetsPageViewModel(ExerciseViewModel exercise, ISetDal setDal, IPageService pageService)
        {
            _setDal = setDal;
            _pageService = pageService;

            Exercise = exercise;

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(() => AddSet());
            EditCommand = new Command<SetViewModel>(set => EditSet(set));
            DeleteCommand = new Command<SetViewModel>(async set => await DeleteSet(set));
        }

        public void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var sets = _setDal.GetSetsByExerciseId(Exercise.Id);
            foreach (var set in sets)
            {
                Sets.Add(new SetViewModel(set));
            }
        }

        public void AddSet()
        {
            Set set = new Set() { ExerciseId = Exercise.Id };
            _setDal.SaveSet(set);
            Sets.Add(new SetViewModel(set));   
        }

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

        public async Task DeleteSet(SetViewModel set)
        {
            if (set == null) return;
            
            if(await _pageService.DisplayAlert("Warning", "Are you sure you want to delete the set?", "Yes", "No"))
            {
                var setModel = _setDal.GetSet(set.Id);
                Sets.Remove(set);
                _setDal.DeleteSet(setModel);
            }
        }

        public void BatchSave()
        {
            foreach(SetViewModel set in Sets)
            {
                EditSet(set);
            }
        }
    }
}
