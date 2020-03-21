using System.Collections.Generic;

namespace NeverSkipLegDay.Models.DAL
{
    public interface IFoodDal
    {
        List<Food> GetFoods();
        Food GetFood(int id);
        List<Food> GetFoodsByMealId(int mealId);
        int SaveFood(Food model);
        int DeleteFood(Food model);
    }
}
