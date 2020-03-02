using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.Views;
using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class DashboardDetailPageViewModel : BaseViewModel
    {
        private IPageService _pageService;
        public ICommand SelectWorkoutsCommand { get; set; }
        public ICommand SelectNutritionCommand { get; set; }
        public ICommand SelectRecordsCommand { get; set; }

        public DashboardDetailPageViewModel(IPageService pageService)
        {
            _pageService = pageService;

            SelectWorkoutsCommand = new Command(async () => await SelectWorkouts());
        }

        private async Task SelectWorkouts()
        {
            await _pageService.PushAsync(new WorkoutsPage());
        }
    }
}
