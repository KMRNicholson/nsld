using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditRecordPage : ContentPage
    {
        public AddEditRecordPageViewModel ViewModel
        {
            get { return BindingContext as AddEditRecordPageViewModel; }
            set { BindingContext = value; }
        }
        public AddEditRecordPage(RecordViewModel recordViewModel)
        {
            var recordDal = new RecordDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new AddEditRecordPageViewModel(recordViewModel, recordDal, pageService);
            InitializeComponent();
        }
    }
}