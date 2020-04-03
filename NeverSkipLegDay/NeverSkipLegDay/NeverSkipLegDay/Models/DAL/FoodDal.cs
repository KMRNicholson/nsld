using System;
using System.Collections.Generic;

using SQLite;

namespace NeverSkipLegDay.Models.DAL
{
    public class FoodDal : IFoodDal
    {
        readonly SQLiteConnection _database;

        public FoodDal(SQLiteDB db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _database = db.GetConnection();
            _database.CreateTable<Food>();
        }

        public List<Food> GetFoods()
        {
            return _database.Table<Food>().ToList();
        }
        public List<Food> GetFoodsByMealId(int mealId)
        {
            return _database.Table<Food>()
                .Where(i => i.MealId == mealId)
                .ToList();
        }
        public Food GetFood(int id)
        {
            return _database.Table<Food>()
                            .Where(i => i.Id == id)
                            .FirstOrDefault();
        }

        public int SaveFood(Food model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.Id != 0)
            {
                return _database.Update(model);
            }
            else
            {
                return _database.Insert(model);
            }
        }

        public int DeleteFood(Food model)
        {
            return _database.Delete(model);
        }
    }
}
