using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class MealViewModel : BaseViewModel
    {
        public int Id { get; set; }
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

        public MealViewModel() { }

        public MealViewModel(Meal meal)
        {
            Id = meal.Id;
            Name = meal.Name;
        }
    }
}
