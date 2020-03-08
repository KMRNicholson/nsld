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

        private int? _fat;
        public int? Fat
        {
            get { return _fat; }
            set
            {
                SetValue(ref _fat, value);
                OnPropertyChanged(nameof(Fat));
            }
        }

        private int? _prot;
        public int? Prot
        {
            get { return _prot; }
            set
            {
                SetValue(ref _prot, value);
                OnPropertyChanged(nameof(Prot));
            }
        }

        private int? _carb;
        public int? Carb
        {
            get { return _carb; }
            set
            {
                SetValue(ref _carb, value);
                OnPropertyChanged(nameof(Carb));
            }
        }

        private int? _cal;
        public int? Cal
        {
            get { return _cal; }
            set
            {
                SetValue(ref _cal, value);
                OnPropertyChanged(nameof(Cal));
            }
        }

        public FoodViewModel() { }

        public FoodViewModel(Food food)
        {
            if (food != null)
            {
                Id = food.Id;
                MealId = food.MealId;
                Name = food.Name;
                Fat = food.Fat;
                Prot = food.Prot;
                Carb = food.Carb;
                Cal = food.Cal;
            } 
        }
    }
}
