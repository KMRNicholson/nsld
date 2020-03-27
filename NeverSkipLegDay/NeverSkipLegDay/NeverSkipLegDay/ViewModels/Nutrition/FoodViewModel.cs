using System;

using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * Class which defines the ViewModel for the Food model. This is used for displaying exercises,
     * as well as mapping values from the view to the model/database, through the binded ViewModel.
     */
    public class FoodViewModel : BaseViewModel
    {
        #region private properties
        private string _name;
        private decimal _fat;
        private decimal _prot;
        private decimal _carb;
        private decimal _cal;
        #endregion

        #region public properties
        public int Id { get; set; }
        public int MealId { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }
        public decimal Fat
        {
            get { return _fat; }
            set
            {
                SetValue(ref _fat, value);
                OnPropertyChanged(nameof(Fat));
            }
        }
        public decimal Prot
        {
            get { return _prot; }
            set
            {
                SetValue(ref _prot, value);
                OnPropertyChanged(nameof(Prot));
            }
        }
        public decimal Carb
        {
            get { return _carb; }
            set
            {
                SetValue(ref _carb, value);
                OnPropertyChanged(nameof(Carb));
            }
        }
        public decimal Cal
        {
            get { return _cal; }
            set
            {
                SetValue(ref _cal, value);
                OnPropertyChanged(nameof(Cal));
            }
        }
        #endregion

        #region constructors
        public FoodViewModel() { }

        public FoodViewModel(Food food)
        {
            if (food == null)
                throw new ArgumentNullException(nameof(food));
            
            Id = food.Id;
            MealId = food.MealId;
            Name = food.Name;
            Fat = food.Fat;
            Prot = food.Prot;
            Carb = food.Carb;
            Cal = food.Cal;
        }
        #endregion
    }
}
