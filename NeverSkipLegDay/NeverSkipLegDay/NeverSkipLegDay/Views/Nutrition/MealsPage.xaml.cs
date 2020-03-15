using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealsPage : ContentPage
    {
        public MealsPageViewModel ViewModel
        {
            get { return BindingContext as MealsPageViewModel; }
            set { BindingContext = value; }
        }
        public MealsPage()
        {
            var mealDal = new MealDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new MealsPageViewModel(mealDal, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.SetTotals();
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
            helpLabel.IsVisible = ViewModel.IsMealsEmpty();
        }

        void OnMealSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectCommand.Execute(e.SelectedItem);
        }
    }
}