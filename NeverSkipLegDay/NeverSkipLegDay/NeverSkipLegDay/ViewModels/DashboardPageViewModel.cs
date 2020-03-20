using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Views;

namespace NeverSkipLegDay.ViewModels
{
    public class DashboardPageViewModel : BaseViewModel
    {
        public string PageTitle { get; private set; }
        private readonly IPageService _pageService;
        public ICommand SelectWorkoutsCommand { get; set; }
        public ICommand SelectNutritionCommand { get; set; }
        public ICommand SelectRecordsCommand { get; set; }

        public DashboardPageViewModel(IPageService pageService)
        {
            PageTitle = "NSLD";
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
