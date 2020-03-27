using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.ViewModels
{
    public class AddEditMealPageViewModel : BaseViewModel
    {
        #region private properties
        private readonly IMealDal _mealDal;
        private readonly IPageService _pageService;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public Meal Meal { get; private set; }
        #endregion

        #region commands
        public ICommand SaveCommand { get; private set; }
        #endregion

        #region constructor
        public AddEditMealPageViewModel(MealViewModel meal, IMealDal mealDal, IPageService pageService)
        {
            if (meal == null)
                throw new ArgumentNullException(nameof(meal));

            PageTitle = "MEAL";

            _mealDal = mealDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());

            Meal = new Meal()
            {
                Id = meal.Id,
                Name = meal.Name
            };
        }
        #endregion

        #region public methods
        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Meal.Name))
            {
                await _pageService.DisplayAlert(DisplayAlerts.Error, DisplayAlerts.NullNameError, DisplayAlerts.Ok).ConfigureAwait(false);
                return;
            }

            _mealDal.SaveMeal(Meal);
            MessagingCenter.Send(this, Events.MealSaved, Meal);
            await _pageService.PopAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
