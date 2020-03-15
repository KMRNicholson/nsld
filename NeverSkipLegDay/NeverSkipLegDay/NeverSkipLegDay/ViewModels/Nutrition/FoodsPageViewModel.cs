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
    public class FoodsPageViewModel : BaseViewModel
    {
        private IFoodDal _foodDal;
        private IPageService _pageService;

        private bool _isDataLoaded;

        private MealViewModel _meal;
        public MealViewModel Meal
        {
            get { return _meal; }
            set
            {
                SetValue(ref _meal, value);
                OnPropertyChanged(nameof(Meal));
            }
        }

        public string AddButtonText
        {
            get { return "Add Food"; }
        }

        public ObservableCollection<FoodViewModel> Foods { get; private set; }
            = new ObservableCollection<FoodViewModel>();

        public ICommand LoadDataCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public FoodsPageViewModel(MealViewModel meal, IFoodDal foodDal, IPageService pageService)
        {
            MessagingCenter.Subscribe<AddEditFoodPageViewModel, Food>
                (this, Events.FoodSaved, OnFoodSaved);

            _foodDal = foodDal;
            _pageService = pageService;

            Meal = meal;

            LoadDataCommand = new Command(() => LoadData());
            AddCommand = new Command(async () => await AddFood());
            EditCommand = new Command<FoodViewModel>(async food => await EditFood(food));
            DeleteCommand = new Command<FoodViewModel>(async food => await DeleteFood(food));
        }

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

        public void LoadData()
        {
            if (_isDataLoaded) return;

            _isDataLoaded = true;
            var foods = _foodDal.GetFoodsByMealId(Meal.Id);
            foreach (var food in foods)
            {
                Foods.Add(new FoodViewModel(food));
            }
        }

        private async Task AddFood()
        {
            await _pageService.PushAsync(new AddEditFoodPage(new FoodViewModel() { MealId = Meal.Id }));
        }

        private async Task EditFood(FoodViewModel food)
        {
            if (food == null) return;

            await _pageService.PushAsync(new AddEditFoodPage(food));
        }

        public async Task DeleteFood(FoodViewModel food)
        {
            if (food == null) return;

            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {food.Name}?", "Yes", "No"))
            {
                var foodModel = _foodDal.GetFood(food.Id);
                Foods.Remove(food);
                _foodDal.DeleteFood(foodModel);
            }

            SetTotals();
        }

        public bool IsFoodsEmpty()
        {
            return Foods.Count == 0 ? true : false;
        }

        public void SetTotals()
        {
            var mealDal = new MealDal(new SQLiteDB());
            var meal = mealDal.GetMeal(Meal.Id);

            Meal = meal != null ? new MealViewModel(meal) : new MealViewModel();
        }
    }
}
