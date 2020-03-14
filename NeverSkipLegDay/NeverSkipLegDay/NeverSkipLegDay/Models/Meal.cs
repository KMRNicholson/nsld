using NeverSkipLegDay.Models.DAL;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace NeverSkipLegDay.Models
{
    public class Meal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public Dictionary<string, int> GetMealTotals(IFoodDal foodDal)
        {
            Dictionary<string, int> totals = new Dictionary<string, int>();

            List<Food> foods = foodDal.GetFoodsByMealId(this.Id);

            totals.Add("Fat", foods.Select(x => x.Fat).Sum().Value);
            totals.Add("Prot", foods.Select(x => x.Prot).Sum().Value);
            totals.Add("Carb", foods.Select(x => x.Carb).Sum().Value);
            totals.Add("Cal", foods.Select(x => x.Cal).Sum().Value);

            return totals;
        }

        public Dictionary<string, int> GetAllMealsTotals(IFoodDal foodDal, IMealDal mealDal)
        {
            List<Meal> meals = new List<Meal>();
            Dictionary<string, int> totals = new Dictionary<string, int>();

            totals.Add("Fat", 0);
            totals.Add("Prot", 0);
            totals.Add("Carb", 0);
            totals.Add("Cal", 0);

            meals = mealDal.GetMeals();

            foreach(Meal meal in meals)
            {
                totals["Fat"] += meal.GetMealTotals(foodDal)["Fat"];
                totals["Prot"] += meal.GetMealTotals(foodDal)["Prot"];
                totals["Carb"] += meal.GetMealTotals(foodDal)["Carb"];
                totals["Cal"] += meal.GetMealTotals(foodDal)["Cal"];
            }

            return totals;
        }
    }
}
