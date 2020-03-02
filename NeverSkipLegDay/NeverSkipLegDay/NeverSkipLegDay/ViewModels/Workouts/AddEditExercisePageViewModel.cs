using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class AddEditExercisePageViewModel : BaseViewModel
    {
        private IExerciseDal _exerciseDal;
        private IPageService _pageService;

        public Exercise Exercise { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public AddEditExercisePageViewModel(ExerciseViewModel exercise, IExerciseDal exerciseDal, IPageService pageService)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            _exerciseDal = exerciseDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());

            Exercise = new Exercise()
            {
                Id = exercise.Id,
                WorkoutId = exercise.WorkoutId,
                Name = exercise.Name
            };
        }

        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Exercise.Name))
            {
                await _pageService.DisplayAlert("Error", "Please enter a name.", "OK");
                return;
            }

            _exerciseDal.SaveExercise(Exercise);
            MessagingCenter.Send(this, Events.ExerciseSaved, Exercise);
            await _pageService.PopAsync();
        }
    }
}
