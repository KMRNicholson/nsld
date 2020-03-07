using System.Collections.Generic;

namespace NeverSkipLegDay.Models.DAL
{
    public interface IMealDal
    {
        List<Meal> GetMeals();
        Meal GetMeal(int id);
        int SaveMeal(Meal model);
        int DeleteMeal(Meal model);
    }
}
