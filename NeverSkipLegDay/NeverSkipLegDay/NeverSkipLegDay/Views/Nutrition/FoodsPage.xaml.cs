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
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
            helpLabel.IsVisible = ViewModel.IsFoodsEmpty();
        }

        void OnFoodSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectCommand.Execute(e.SelectedItem);
        }
    }
}