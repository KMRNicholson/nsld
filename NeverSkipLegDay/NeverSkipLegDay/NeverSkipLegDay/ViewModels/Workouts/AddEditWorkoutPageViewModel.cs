using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class AddEditWorkoutPageViewModel : BaseViewModel
    {
        #region private properties
        private readonly IWorkoutDal _workoutDal;
        private readonly IPageService _pageService;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public Workout Workout { get; private set; }
        public ICommand SaveCommand { get; private set; }
        #endregion

        public AddEditWorkoutPageViewModel(WorkoutViewModel workout, IWorkoutDal workoutDal, IPageService pageService)
        {
            if (workout == null)
                throw new ArgumentNullException(nameof(workout));

            _workoutDal = workoutDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save().ConfigureAwait(false));

            Workout = new Workout()
            {
                Id = workout.Id,
                Name = workout.Name
            };
        }

        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Workout.Name))
            {
                await _pageService.DisplayAlert(DisplayAlerts.Error, DisplayAlerts.NullNameWarning, DisplayAlerts.Ok).ConfigureAwait(false);
                return;
            }

            _workoutDal.SaveWorkout(Workout);
            MessagingCenter.Send(this, Events.WorkoutSaved, Workout);
            await _pageService.PopAsync().ConfigureAwait(false);
        }
    }
}
