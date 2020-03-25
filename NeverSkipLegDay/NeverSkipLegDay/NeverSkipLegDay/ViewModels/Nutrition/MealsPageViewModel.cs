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
    public class MealsPageViewModel : BaseViewModel
    {
        private MealViewModel _selectedMeal;
        private IMealDal _mealDal;
        private IPageService _pageService;

        private int _fatTotal;
        public int FatTotal
        {
            get { return _fatTotal; }
            set
            {
                SetValue(ref _fatTotal, value);
                OnPropertyChanged(nameof(_fatTotal));
            }
        }

        private int _protTotal;
        public int ProtTotal
        {
            get { return _protTotal; }
            set
            {
                SetValue(ref _protTotal, value);
                OnPropertyChanged(nameof(_protTotal));
            }
        }

        private int _carbTotal;
        public int CarbTotal
        {
            get { return _carbTotal; }
            set
            {
                SetValue(ref _carbTotal, value);
                OnPropertyChanged(nameof(_carbTotal));
            }
        }

        private int _calTotal;
        public int CalTotal
        {
            get { return _calTotal; }
            set
            {
                SetValue(ref _calTotal, value);
                OnPropertyChanged(nameof(_calTotal));
            }
        }

        public string AddButtonText
        {
            get { return "Add Meal"; }
        }

        public ObservableCollection<MealViewModel> Meals { get; private set; }
            = new ObservableCollection<MealViewModel>();

        public MealViewModel SelectedMeal
        {
            get { return _selectedMeal; }
            set { SetValue(ref _selectedMeal, value); }
        }

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        public MealsPageViewModel(IMealDal mealDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditMealPageViewModel, Meal>
                (this, Events.MealSaved, OnMealSaved);

            _mealDal = mealDal;
            _pageService = pageService;

            SetTotals();

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddMeal().ConfigureAwait(false));
            EditCommand = new Command<MealViewModel>(async meal => await EditMeal(meal).ConfigureAwait(false));
            DeleteCommand = new Command<MealViewModel>(async meal => await DeleteMeal(meal).ConfigureAwait(false));
            SelectCommand = new Command<MealViewModel>(async meal => await SelectMeal(meal).ConfigureAwait(false));
        }

        private void OnMealSaved(AddEditMealPageViewModel source, Meal meal)
        {
            MealViewModel mealInList = Meals.Where(w => w.Id == meal.Id).ToList().FirstOrDefault();

            SetTotals();

            if (mealInList == null)
            {
                Meals.Add(new MealViewModel(meal));
            }
            else
            {
                mealInList.Id = meal.Id;
                mealInList.Name = meal.Name;
            }
        }

        public void LoadData()
        {
            Meals.Clear();
            List<Meal> meals = _mealDal.GetMeals();
            foreach (var meal in meals)
            {
                Meals.Add(new MealViewModel(meal));
            }

            SetTotals();
        }

        private async Task AddMeal()
        {
            await _pageService.PushAsync(new AddEditMealPage(new MealViewModel())).ConfigureAwait(false);
        }

        private async Task EditMeal(MealViewModel meal)
        {
            if (meal == null) return;

            await _pageService.PushAsync(new AddEditMealPage(meal)).ConfigureAwait(false);
        }

        public async Task DeleteMeal(MealViewModel meal)
        {
            if (meal == null) return;

            if (await _pageService.DisplayAlert(DisplayAlerts.Warning, $"Are you sure you want to delete {meal.Name}?", DisplayAlerts.Yes, DisplayAlerts.No).ConfigureAwait(false))
            {
                var mealModel = _mealDal.GetMeal(meal.Id);
                Meals.Remove(meal);
                _mealDal.DeleteMeal(mealModel);
            }

            SetTotals();
        }

        public async Task SelectMeal(MealViewModel meal)
        {
            if (meal == null) return;

            SelectedMeal = null;
            await _pageService.PushAsync(new FoodsPage(meal)).ConfigureAwait(false);
        }

        public bool IsMealsEmpty()
        {
            return Meals.Count == 0 ? true : false;
        }

        public void SetTotals()
        {
            FatTotal = Meals.Select(x => x.FatTotal).Sum();
            ProtTotal = Meals.Select(x => x.ProtTotal).Sum();
            CarbTotal = Meals.Select(x => x.CarbTotal).Sum();
            CalTotal = Meals.Select(x => x.CalTotal).Sum();
        }
    }
}
