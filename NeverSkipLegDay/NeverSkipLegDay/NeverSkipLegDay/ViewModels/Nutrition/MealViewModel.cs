using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using System;
using System.Collections.Generic;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * Class which defines the ViewModel for the Meal model. This is used for displaying workouts,
     * as well as mapping values from the view to the model/database, through the binded ViewModel.
     */
    public class MealViewModel : BaseViewModel
    {
        #region private properties
        private string _name;
        #endregion

        #region public properties
        public int Id { get; set; }
        public decimal FatTotal { get; private set; }
        public decimal ProtTotal { get; private set; }
        public decimal CarbTotal { get; private set; }
        public decimal CalTotal { get; private set; }
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }
        #endregion

        #region constructors
        public MealViewModel() { }

        public MealViewModel(Meal meal)
        {
            if (meal == null)
                throw new ArgumentNullException(nameof(meal));

            Id = meal.Id;
            Name = meal.Name;
            
            Dictionary<string, decimal> totals = meal.GetMealTotals(new FoodDal(new SQLiteDB()));

            FatTotal = totals["Fat"];
            ProtTotal = totals["Prot"];
            CarbTotal = totals["Carb"];
            CalTotal = totals["Cal"];
        }
        #endregion
    }
}
