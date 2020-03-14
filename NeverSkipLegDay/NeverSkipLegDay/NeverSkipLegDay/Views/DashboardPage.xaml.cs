using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            await title.FadeTo(1, 250).ConfigureAwait(true);

            uint scaleBig = 100;
            uint scaleBack = 100;

            await workouts.ScaleTo(1.1, scaleBig).ConfigureAwait(true);
            workouts.ScaleTo(1, scaleBack).ConfigureAwait(true).GetAwaiter();
            await nutrition.ScaleTo(1.1, scaleBig).ConfigureAwait(true);
            nutrition.ScaleTo(1, scaleBack).ConfigureAwait(true).GetAwaiter();
            await records.ScaleTo(1.1, scaleBig).ConfigureAwait(true);
            records.ScaleTo(1, scaleBack).ConfigureAwait(true).GetAwaiter();

            base.OnAppearing();
        }
    }
}