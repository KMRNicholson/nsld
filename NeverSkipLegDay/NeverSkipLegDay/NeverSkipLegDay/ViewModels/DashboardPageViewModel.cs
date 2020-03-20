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
        private IPageService _pageService;
        public string PageName { get => "NSLD"; }
        public ICommand SelectWorkoutsCommand { get; set; }
        public ICommand SelectNutritionCommand { get; set; }
        public ICommand SelectRecordsCommand { get; set; }

        public DashboardPageViewModel(IPageService pageService)
        {
            _pageService = pageService;

            SelectWorkoutsCommand = new Command(async () => await SelectWorkouts());
            SelectNutritionCommand = new Command(async () => await SelectNutrition());
            SelectRecordsCommand = new Command(async () => await SelectRecords());
        }

        private async Task SelectWorkouts()
        {
            await _pageService.PushAsync(new WorkoutsPage());
        }

        private async Task SelectNutrition()
        {
            await _pageService.PushAsync(new MealsPage());
        }

        private async Task SelectRecords()
        {
            await _pageService.PushAsync(new RecordExercisesPage(new WorkoutViewModel(new Workout())));
        }
    }
}
