using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class AddEditWorkoutViewModel : BaseViewModel
    {
        private WorkoutDal _workoutDal;
        private IPageService _pageService;

        public Workout Workout { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public AddEditWorkoutViewModel(WorkoutViewModel workout, WorkoutDal workoutDal, IPageService pageService)
        {
            if (workout == null)
                throw new ArgumentNullException(nameof(workout));

            _workoutDal = workoutDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());

            Workout = new Workout()
            {
                Id = workout.Id,
                Name = workout.Name
            };
        }

        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Workout.Name))
            {
                await _pageService.DisplayAlert("Error", "Please enter a name.", "OK");
            }

            await _workoutDal.SaveWorkoutAsync(Workout);
            await _pageService.PopAsync();
        }
    }
}
