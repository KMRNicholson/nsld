using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * Class which defines the ViewModel for the AddEditExercisePage.xaml.cs.
     */
    public class AddEditExercisePageViewModel : BaseViewModel
    {
        #region private properties
        private IExerciseDal _exerciseDal;
        private IPageService _pageService;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public Exercise Exercise { get; private set; }
        #endregion

        #region commands
        public ICommand SaveCommand { get; private set; }
        #endregion

        #region constructors
        public AddEditExercisePageViewModel(ExerciseViewModel exercise, IExerciseDal exerciseDal, IPageService pageService)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            PageTitle = "EXERCISE";

            _exerciseDal = exerciseDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save().ConfigureAwait(false));

            Exercise = new Exercise()
            {
                Id = exercise.Id,
                WorkoutId = exercise.WorkoutId,
                Name = exercise.Name
            };
        }
        #endregion

        #region public methods
        //Method which saves the exercise, and sends an event to the MessagingCenter.
        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Exercise.Name))
            {
                await _pageService.DisplayAlert(DisplayAlerts.Error, DisplayAlerts.NullNameError, DisplayAlerts.Ok).ConfigureAwait(false);
                return;
            }

            _exerciseDal.SaveExercise(Exercise);
            MessagingCenter.Send(this, Events.ExerciseSaved, Exercise);
            await _pageService.PopAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
