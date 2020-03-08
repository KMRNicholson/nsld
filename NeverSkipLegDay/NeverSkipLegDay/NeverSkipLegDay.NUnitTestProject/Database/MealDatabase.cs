using System.Collections.Generic;
using System.Linq;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.NUnitTestProject.Database
{
    public class MealDatabase : IMealDal
    {
        public List<Meal> meals { get; set; }
        public MealDatabase()
        {
            meals = new List<Meal>
            {
                new Meal(){ Id = 1, Name = "Breakfast"},
                new Meal(){ Id = 2, Name = "Lunch"},
                new Meal(){ Id = 3, Name = "Dinner"},
                new Meal(){ Id = 4, Name = "Snack"}
            };
        }

        public List<Meal> GetMeals()
        {
            return meals;
        }

        public Meal GetMeal(int id)
        {
            return meals.Where(m => m.Id == id).ToList().FirstOrDefault();
        }

        public int SaveMeal(Meal model)
        {
            if (model.Id != 0)
            {
                meals.ForEach(m => { if (m.Id == model.Id) m.Name = model.Name; });
                return 1;
            }
            else
            {
                model.Id = meals.Last().Id + 1;
                meals.Add(model);
                return meals.Find(m => m == model) != null ? 1 : 0;
            }
        }

        public int DeleteMeal(Meal model)
        {
            meals.Remove(model);
            return meals.Find(m => m == model) == null ? 1 : 0;
        }
    }
}
