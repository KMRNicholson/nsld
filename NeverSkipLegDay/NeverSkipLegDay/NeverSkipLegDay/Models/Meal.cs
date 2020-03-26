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
        public Dictionary<string, int> GetMealTotals(IFoodDal foodDal)
        {
            if (foodDal == null)
                throw new ArgumentNullException(nameof(foodDal));

            Dictionary<string, int> totals = new Dictionary<string, int>();

            List<Food> foods = foodDal.GetFoodsByMealId(this.Id);

            totals.Add("Fat", foods.Select(x => x.Fat).Sum().Value);
            totals.Add("Prot", foods.Select(x => x.Prot).Sum().Value);
            totals.Add("Carb", foods.Select(x => x.Carb).Sum().Value);
            totals.Add("Cal", foods.Select(x => x.Cal).Sum().Value);

            return totals;
        }
        #endregion
    }
}
