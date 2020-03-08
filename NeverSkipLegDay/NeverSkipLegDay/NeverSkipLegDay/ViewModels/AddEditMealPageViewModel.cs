using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class AddEditMealPageViewModel : BaseViewModel
    {
        private IMealDal _mealDal;
        private IPageService _pageService;

        public Meal Meal { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public AddEditMealPageViewModel(MealViewModel meal, IMealDal mealDal, IPageService pageService)
        {
            if (meal == null)
                throw new ArgumentNullException(nameof(meal));

            _mealDal = mealDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());

            Meal = new Meal()
            {
                Id = meal.Id,
                Name = meal.Name
            };
        }

        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Meal.Name))
            {
                await _pageService.DisplayAlert("Error", "Please enter a name.", "OK");
                return;
            }

            _mealDal.SaveMeal(Meal);
            MessagingCenter.Send(this, Events.MealSaved, Meal);
            await _pageService.PopAsync();
        }
    }
}
