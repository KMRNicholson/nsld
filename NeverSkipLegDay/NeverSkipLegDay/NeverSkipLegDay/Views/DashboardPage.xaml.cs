using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPageViewModel ViewModel
        {
            get { return BindingContext as DashboardPageViewModel; }
            set { BindingContext = value; }
        }
        public DashboardPage()
        {
            PageService pageService = new PageService();
            ViewModel = new DashboardPageViewModel(pageService);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await ButtonPop(workouts).ConfigureAwait(false);
            await ButtonPop(nutrition).ConfigureAwait(false);
            await ButtonPop(records).ConfigureAwait(false);

            base.OnAppearing();
        }

        private async Task ButtonPop(Button button)
        {
            uint scaleBig = 100;
            uint scaleBack = 100;

            await button.ScaleTo(1.1, scaleBig).ConfigureAwait(true);
            button.ScaleTo(1, scaleBack).ConfigureAwait(true).GetAwaiter();
        }
    }
}