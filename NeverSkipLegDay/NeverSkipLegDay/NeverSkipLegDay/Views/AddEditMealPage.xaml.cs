using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditMealPage : ContentPage
    {
        public AddEditMealPageViewModel ViewModel
        {
            get { return BindingContext as AddEditMealPageViewModel; }
            set { BindingContext = value; }
        }
        public AddEditMealPage(MealViewModel mealViewModel)
        {
            var mealDal = new MealDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new AddEditMealPageViewModel(mealViewModel, mealDal, pageService);
            InitializeComponent();
        }
    }
}