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
    }
}
