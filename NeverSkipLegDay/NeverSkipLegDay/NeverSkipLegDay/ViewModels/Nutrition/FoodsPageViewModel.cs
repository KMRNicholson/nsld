using System.Globalization;
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
     * The ViewModel for the FoodsPage.xaml.cs.
     * Contains all methods for retrieving information from the model
     * and displaying this information to the view.
     */
    public class FoodsPageViewModel : BaseViewModel
    {
        #region private properties
        private MealViewModel _meal;
        private readonly IFoodDal _foodDal;
        private readonly IPageService _pageService;
        private bool _isDataLoaded;
        private bool _showHelpLabel;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public string ButtonText { get; private set; }
        public ObservableCollection<FoodViewModel> Foods { get; private set; }
            = new ObservableCollection<FoodViewModel>();
        public MealViewModel Meal
        {
            get { return _meal; }
            set
            {
                SetValue(ref _meal, value);
                OnPropertyChanged(nameof(Meal));
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

        #region constructor
        public FoodsPageViewModel(MealViewModel meal, IFoodDal foodDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditFoodPageViewModel, Food>
                (this, Events.FoodSaved, OnFoodSaved);

            PageTitle = "FOODS";
            ButtonText = "Add Food";

            _foodDal = foodDal;
            _pageService = pageService;

            Meal = meal;

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddFood().ConfigureAwait(false));
            EditCommand = new Command<FoodViewModel>(async food => await EditFood(food).ConfigureAwait(false));
            DeleteCommand = new Command<FoodViewModel>(async food => await DeleteFood(food).ConfigureAwait(false));
        }
        #endregion

        #region public methods
        // Asynchronous task for deleting food from the database and removing it from the list.
        // params: FoodViewModel - required for removing the viewmodel from the list, and to delete the record in the database.
        public async Task DeleteFood(FoodViewModel food)
        {
            if (food == null) return;

            string warningMessage = string.Format(new CultureInfo("en-US"), DisplayAlerts.DeleteWarning, food.Name);

            if (await _pageService.DisplayAlert(DisplayAlerts.Warning, warningMessage, DisplayAlerts.Yes, DisplayAlerts.No).ConfigureAwait(false))
            {
                var foodModel = _foodDal.GetFood(food.Id);
                Foods.Remove(food);
                _foodDal.DeleteFood(foodModel);
            }

            ShowHelpLabel = IsFoodsEmpty();

            SetTotals();
        }
        #endregion

        #region private methods
        // Method which returns a list of foods associated to the selected meal by using the exercise id.
        private void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var foods = _foodDal.GetFoodsByMealId(Meal.Id);
            foreach (var food in foods)
            {
                Foods.Add(new FoodViewModel(food));
            }

            ShowHelpLabel = IsFoodsEmpty();

            SetTotals();
        }

        // Method which saves a food to the database. Triggered by the MessageCenter event.
        private void OnFoodSaved(AddEditFoodPageViewModel source, Food food)
        {
            FoodViewModel foodInList = Foods.Where(e => e.Id == food.Id).ToList().FirstOrDefault();

            SetTotals();

            if (foodInList == null)
            {
                Foods.Add(new FoodViewModel(food));
            }
            else
            {
                foodInList.Name = food.Name;
                foodInList.Fat = food.Fat;
                foodInList.Prot = food.Prot;
                foodInList.Carb = food.Carb;
                foodInList.Cal = food.Cal;
            }
        }

        // Method which adds and saves a new food to the list and database.
        private async Task AddFood()
        {
            await _pageService.PushAsync(new AddEditFoodPage(new FoodViewModel() { MealId = Meal.Id })).ConfigureAwait(false);
        }

        // Method which edits and saves food to the list and database.
        private async Task EditFood(FoodViewModel food)
        {
            if (food == null) return;

            await _pageService.PushAsync(new AddEditFoodPage(food)).ConfigureAwait(false);
        }

        // Method which checks to see if the food list is empty, in order to display the help label in the view.
        public bool IsFoodsEmpty()
        {
            return Foods.Count == 0 ? true : false;
        }

        // Method which gets a new meal model to update the macros and cals to display in the view.
        public void SetTotals()
        {
            //THIS SHOULD NOT ACTUALLY CALL THE DAL. UPDATE THE CURRENT MODEL ON THE FLY
            var mealDal = new MealDal(new SQLiteDB());
            var meal = mealDal.GetMeal(Meal.Id);

            Meal = meal != null ? new MealViewModel(meal) : Meal;
        }
        #endregion
    }
}
