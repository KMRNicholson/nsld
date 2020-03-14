using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordsPage : ContentPage
    {
        public RecordsPageViewModel ViewModel
        {
            get { return BindingContext as RecordsPageViewModel; }
            set { BindingContext = value; }
        }
        public RecordsPage(ExerciseViewModel exercise)
        {
            var recordDal = new RecordDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new RecordsPageViewModel(exercise, recordDal, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
            helpLabel.IsVisible = ViewModel.IsRecordsEmpty();
        }
    }
}