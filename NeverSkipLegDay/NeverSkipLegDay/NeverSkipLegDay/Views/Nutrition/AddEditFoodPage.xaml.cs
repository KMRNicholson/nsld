using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditFoodPage : ContentPage
    {
        public AddEditFoodPageViewModel ViewModel
        {
            get { return BindingContext as AddEditFoodPageViewModel; }
            set { BindingContext = value; }
        }
        public AddEditFoodPage(FoodViewModel foodViewModel)
        {
            var foodDal = new FoodDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new AddEditFoodPageViewModel(foodViewModel, foodDal, pageService);
            InitializeComponent();
        }
    }
}