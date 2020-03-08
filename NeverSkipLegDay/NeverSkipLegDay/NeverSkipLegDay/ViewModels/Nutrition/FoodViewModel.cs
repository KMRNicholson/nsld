using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class FoodViewModel : BaseViewModel
    {
        public int Id { get; set; }
        private int _mealId;
        public int MealId
        {
            get { return _mealId; }
            set
            {
                SetValue(ref _mealId, value);
                OnPropertyChanged(nameof(MealId));
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }

        public FoodViewModel() { }

        public FoodViewModel(Food food)
        {
            Id = food.Id;
            MealId = food.MealId;
            Name = food.Name;
        }
    }
}
