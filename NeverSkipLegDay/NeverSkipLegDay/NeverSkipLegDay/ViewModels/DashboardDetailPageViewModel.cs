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

        public ICommand SelectWorkoutsCommand { get; private set; }
        public ICommand SelectNutritionCommand { get; set; }
        public ICommand SelectRecordsCommand { get; set; }

        public DashboardDetailPageViewModel(IPageService pageService)
        {
            _pageService = pageService;

            SelectWorkoutsCommand = new Command(async workoutsPage => await SelectWorkouts(workoutsPage));
        }

        private async Task SelectWorkouts(object workoutsPage)
        {

            await _pageService.PushAsync((WorkoutsPage)workoutsPage);
        }
    }
}
