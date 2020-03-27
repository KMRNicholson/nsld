using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;

using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Models
{
    /*
     * Class which defines the behavior and properties of the Meal model, and entity in the database.
     */
    public class Meal
    {
        #region attributes
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion

        #region public methods
        // Method for collecting the total macronutrients and calories for the meal using the foodDal and the meal id.
        public Dictionary<string, decimal> GetMealTotals(IFoodDal foodDal)
        {
            if (foodDal == null)
                throw new ArgumentNullException(nameof(foodDal));

            Dictionary<string, decimal> totals = new Dictionary<string, decimal>();

            List<Food> foods = foodDal.GetFoodsByMealId(this.Id);

            totals.Add("Fat", foods.Select(x => x.Fat).Sum());
            totals.Add("Prot", foods.Select(x => x.Prot).Sum());
            totals.Add("Carb", foods.Select(x => x.Carb).Sum());
            totals.Add("Cal", foods.Select(x => x.Cal).Sum());

            return totals;
        }
        #endregion
    }
}
