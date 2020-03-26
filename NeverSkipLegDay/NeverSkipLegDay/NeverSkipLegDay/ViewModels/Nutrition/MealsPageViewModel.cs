using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Globalization;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.Views;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * The ViewModel for the MealsPage.xaml.cs.
     * Contains all methods for retrieving information from the model
     * and displaying this information to the view.
     */
    public class MealsPageViewModel : BaseViewModel
    {
        #region private properties
        private readonly IMealDal _mealDal;
        private readonly IPageService _pageService;
        private MealViewModel _selectedMeal;
        private bool _showHelpLabel;
        private int _fatTotal;
        private int _protTotal;
        private int _carbTotal;
        private int _calTotal;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public string ButtonText { get; private set; }
        public ObservableCollection<MealViewModel> Meals { get; private set; }
            = new ObservableCollection<MealViewModel>();
        public int FatTotal
        {
            get { return _fatTotal; }
            set
            {
                SetValue(ref _fatTotal, value);
                OnPropertyChanged(nameof(_fatTotal));
            }
        }
        public int ProtTotal
        {
            get { return _protTotal; }
            set
            {
                SetValue(ref _protTotal, value);
                OnPropertyChanged(nameof(_protTotal));
            }
        }
        public int CarbTotal
        {
            get { return _carbTotal; }
            set
            {
                SetValue(ref _carbTotal, value);
                OnPropertyChanged(nameof(_carbTotal));
            }
        }
        public int CalTotal
        {
            get { return _calTotal; }
            set
            {
                SetValue(ref _calTotal, value);
                OnPropertyChanged(nameof(_calTotal));
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
        public MealViewModel SelectedMeal
        {
            get { return _selectedMeal; }
            set { SetValue(ref _selectedMeal, value); }
        }
        #endregion

        #region commands
        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        #endregion

        #region constructors
        public MealsPageViewModel(IMealDal mealDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditMealPageViewModel, Meal>
                (this, Events.MealSaved, OnMealSaved);

            PageTitle = "MEALS";
            ButtonText = "Add Meal";

            _mealDal = mealDal;
            _pageService = pageService;

            SetTotals();

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddMeal().ConfigureAwait(false));
            EditCommand = new Command<MealViewModel>(async meal => await EditMeal(meal).ConfigureAwait(false));
            DeleteCommand = new Command<MealViewModel>(async meal => await DeleteMeal(meal).ConfigureAwait(false));
            SelectCommand = new Command<MealViewModel>(async meal => await SelectMeal(meal).ConfigureAwait(false));
        }
        #endregion

        #region public methods
        // Asynchronous task for deleting a meal from the database and removing it from the list.
        // params: MealViewModel - required for removing the viewmodel from the list, and to delete the record in the database.
        public async Task DeleteMeal(MealViewModel meal)
        {
            if (meal == null) return;

            string warningMessage = string.Format(new CultureInfo("en-US"), DisplayAlerts.DeleteWarning, meal.Name);

            if (await _pageService.DisplayAlert(DisplayAlerts.Warning, warningMessage, DisplayAlerts.Yes, DisplayAlerts.No).ConfigureAwait(false))
            {
                var mealModel = _mealDal.GetMeal(meal.Id);
                Meals.Remove(meal);
                _mealDal.DeleteMeal(mealModel);
            }

            ShowHelpLabel = IsMealsEmpty();

            SetTotals();
        }
        #endregion

        #region private methods
        // Method which returns a list of meals.
        private void LoadData()
        {
            Meals.Clear();
            List<Meal> meals = _mealDal.GetMeals();
            foreach (var meal in meals)
            {
                Meals.Add(new MealViewModel(meal));
            }

            ShowHelpLabel = IsMealsEmpty();

            SetTotals();
        }

        // Method which is triggered when the MealSaved event occurs. It simply takes the saved meal and updates/adds it to the list.
        // params: AddEditMealPageViewModel - Passed in by the MessagingCenter.subscribe(). This is the source which triggered the event
        //         Meal - The model which is being saved.
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

        // Method which sends the user to the page to add a new meal.
        private async Task AddMeal()
        {
            await _pageService.PushAsync(new AddEditMealPage(new MealViewModel())).ConfigureAwait(false);
        }

        // Method which sends the user to the page to edit a meal.
        // params: MealViewModel - The bounded viewmodel in the list which is being chosen for edit.
        private async Task EditMeal(MealViewModel meal)
        {
            if (meal == null) return;

            await _pageService.PushAsync(new AddEditMealPage(meal)).ConfigureAwait(false);
        }

        // Method which is triggered when a user selects a meal from the list.
        // params: MealViewModel - The selected item from the MealViewModel list.
        private async Task SelectMeal(MealViewModel meal)
        {
            if (meal == null) return;

            SelectedMeal = null;
            await _pageService.PushAsync(new FoodsPage(meal)).ConfigureAwait(false);
        }

        // Method which checks to see if the meal list is empty, in order to display the help label in the view.
        private bool IsMealsEmpty()
        {
            return Meals.Count == 0 ? true : false;
        }

        // Method which sets the totals for the macronutrients and calories.
        private void SetTotals()
        {
            FatTotal = Meals.Select(x => x.FatTotal).Sum();
            ProtTotal = Meals.Select(x => x.ProtTotal).Sum();
            CarbTotal = Meals.Select(x => x.CarbTotal).Sum();
            CalTotal = Meals.Select(x => x.CalTotal).Sum();
        }
        #endregion
    }
}
