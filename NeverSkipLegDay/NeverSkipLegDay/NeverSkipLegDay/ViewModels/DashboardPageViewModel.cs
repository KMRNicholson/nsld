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
    public class DashboardPageViewModel : BaseViewModel
    {
        public static string PageName => "NSLD";

        private IPageService _pageService;
        public ICommand SelectWorkoutsCommand { get; set; }
        public ICommand SelectNutritionCommand { get; set; }
        public ICommand SelectRecordsCommand { get; set; }

        public DashboardPageViewModel(IPageService pageService)
        {
            _pageService = pageService;

            SelectWorkoutsCommand = new Command(async () => await SelectWorkouts().ConfigureAwait(false));
            SelectNutritionCommand = new Command(async () => await SelectNutrition().ConfigureAwait(false));
            SelectRecordsCommand = new Command(async () => await SelectRecords().ConfigureAwait(false));
        }

        private async Task SelectWorkouts()
        {
            await _pageService.PushAsync(new WorkoutsPage()).ConfigureAwait(false);
        }

        private async Task SelectNutrition()
        {
            await _pageService.PushAsync(new MealsPage()).ConfigureAwait(false);
        }

        private async Task SelectRecords()
        {
            await _pageService.PushAsync(new RecordExercisesPage(new WorkoutViewModel(new Workout()))).ConfigureAwait(false);
        }
    }
}
