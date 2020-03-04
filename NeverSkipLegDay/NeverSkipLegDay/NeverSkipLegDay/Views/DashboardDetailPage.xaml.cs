using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardDetailPage : ContentPage
    {
        public DashboardDetailPageViewModel ViewModel
        {
            get { return BindingContext as DashboardDetailPageViewModel; }
            set { BindingContext = value; }
        }
        public DashboardDetailPage()
        {
            PageService pageService = new PageService();
            ViewModel = new DashboardDetailPageViewModel(pageService);
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