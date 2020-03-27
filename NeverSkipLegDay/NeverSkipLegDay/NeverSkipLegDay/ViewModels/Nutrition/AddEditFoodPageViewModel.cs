using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * Class which defines the ViewModel for the AddEditFoodPage.xaml.cs.
     */
    public class AddEditFoodPageViewModel : BaseViewModel
    {
        #region private properties
        private readonly IFoodDal _foodDal;
        private readonly IPageService _pageService;
        #endregion

        #region public properties
        public string PageTitle { get; private set; }
        public Food Food { get; private set; }
        #endregion

        #region commands
        public ICommand SaveCommand { get; private set; }
        #endregion

        #region constructor
        public AddEditFoodPageViewModel(FoodViewModel food, IFoodDal foodDal, IPageService pageService)
        {
            if (food == null)
                throw new ArgumentNullException(nameof(food));

            PageTitle = "FOOD";

            _foodDal = foodDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save().ConfigureAwait(false));

            Food = new Food()
            {
                Id = food.Id,
                MealId = food.MealId,
                Name = food.Name,
                Fat = food.Fat,
                Prot = food.Prot,
                Carb = food.Carb,
                Cal = food.Cal
            };
        }
        #endregion

        #region public methods
        // Method which saves food and sends the event using the MessagingCenter.
        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Food.Name))
            {
                await _pageService.DisplayAlert(DisplayAlerts.Error, DisplayAlerts.NullNameError, DisplayAlerts.Ok).ConfigureAwait(false);
                return;
            }

            _foodDal.SaveFood(Food);
            MessagingCenter.Send(this, Events.FoodSaved, Food);
            await _pageService.PopAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
