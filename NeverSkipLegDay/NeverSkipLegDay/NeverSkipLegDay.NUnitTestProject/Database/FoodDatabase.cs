using System.Collections.Generic;
using System.Linq;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.NUnitTestProject.Database
{
    public class FoodDatabase : IFoodDal
    {
        public List<Food> Foods { get; set; }
        public FoodDatabase()
        {
            Foods = new List<Food>
            {
                new Food(){ Id = 1, MealId = 1, Name = "2 Eggs", Fat = 20, Prot = 12, Carb = 0, Cal = 140},
                new Food(){ Id = 2, MealId = 1, Name = "1 Banana", Fat = 0, Prot = 2, Carb = 27, Cal = 90},
                new Food(){ Id = 3, MealId = 1, Name = "2 Toast", Fat = 0, Prot = 5, Carb = 34, Cal = 150},
                new Food(){ Id = 4, MealId = 2, Name = "Soup"},
                new Food(){ Id = 5, MealId = 2, Name = "Bread"},
                new Food(){ Id = 6, MealId = 2, Name = "1 Glass of Milk"},
                new Food(){ Id = 7, MealId = 3, Name = "100g Beef", Fat = 15, Prot = 26, Carb = 0, Cal = 250},
                new Food(){ Id = 8, MealId = 3, Name = "2 Potatoes", Fat = 0, Prot = 4, Carb = 37, Cal = 160},
                new Food(){ Id = 9, MealId = 3, Name = "500ml 1% Milk", Fat = 5, Prot = 17, Carb = 26, Cal = 220},
            };
        }

        public List<Food> GetFoods()
        {
            return Foods;
        }

        public Food GetFood(int id)
        {
            return Foods.Where(w => w.Id == id).ToList().FirstOrDefault();
        }

        public List<Food> GetFoodsByMealId(int mealId)
        {
            return Foods.Where(w => w.MealId == mealId).ToList();
        }

        public int SaveFood(Food model)
        {
            if (model.Id != 0)
            {
                Foods.ForEach(w => { if (w.Id == model.Id) w.Name = model.Name; });
                return 1;
            }
            else
            {
                model.Id = Foods.Last().Id + 1;
                Foods.Add(model);
                return Foods.Find(w => w == model) != null ? 1 : 0;
            }
        }

        public int DeleteFood(Food model)
        {
            Foods.Remove(model);
            return Foods.Find(w => w == model) == null ? 1 : 0;
        }
    }
}
