using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class AddEditFoodPageViewModel : BaseViewModel
    {
        private IFoodDal _foodDal;
        private IPageService _pageService;

        public Food Food { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public AddEditFoodPageViewModel(FoodViewModel food, IFoodDal foodDal, IPageService pageService)
        {
            if (food == null)
                throw new ArgumentNullException(nameof(food));

            _foodDal = foodDal;
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());

            Food = new Food()
            {
                Id = food.Id,
                MealId = food.MealId,
                Name = food.Name
            };
        }

        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Food.Name))
            {
                await _pageService.DisplayAlert("Error", "Please enter a name.", "OK");
                return;
            }

            _foodDal.SaveFood(Food);
            MessagingCenter.Send(this, Events.FoodSaved, Food);
            await _pageService.PopAsync();
        }
    }
}
