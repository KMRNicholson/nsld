using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using System.Collections.Generic;

namespace NeverSkipLegDay.ViewModels
{
    public class MealViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int FatTotal { get; private set; }
        public int ProtTotal { get; private set; }
        public int CarbTotal { get; private set; }
        public int CalTotal { get; private set; }

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
            
            Dictionary<string, int> totals = meal.GetMealTotals(new FoodDal(new SQLiteDB()));

            FatTotal = totals["Fat"];
            ProtTotal = totals["Prot"];
            CarbTotal = totals["Carb"];
            CalTotal = totals["Cal"];
        }
    }
}
