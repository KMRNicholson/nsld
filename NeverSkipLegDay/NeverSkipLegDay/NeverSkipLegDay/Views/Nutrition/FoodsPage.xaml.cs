using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodsPage : ContentPage
    {
        public FoodsPageViewModel ViewModel
        {
            get { return BindingContext as FoodsPageViewModel; }
            set { BindingContext = value; }
        }
        public FoodsPage(MealViewModel meal)
        {
            var foodDal = new FoodDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new FoodsPageViewModel(meal, foodDal, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var meal = new MealViewModel(new MealDal(new SQLiteDB()).GetMeal(ViewModel.Meal.Id));
            var foodDal = new FoodDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new FoodsPageViewModel(meal, foodDal, pageService);
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
        }
    }
}