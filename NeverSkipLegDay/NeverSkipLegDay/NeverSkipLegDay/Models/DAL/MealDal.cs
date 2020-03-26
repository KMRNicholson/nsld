using System;
using System.Collections.Generic;

using SQLite;

namespace NeverSkipLegDay.Models.DAL
{
    public class MealDal : IMealDal
    {
        readonly SQLiteConnection _database;
        public MealDal(SQLiteDB db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _database = db.GetConnection();
            _database.CreateTable<Meal>();
        }

        public List<Meal> GetMeals()
        {
            return _database.Table<Meal>().ToList();
        }
        public Meal GetMeal(int id)
        {
            return _database.Table<Meal>()
                            .Where(i => i.Id == id)
                            .FirstOrDefault();
        }

        public int SaveMeal(Meal model)
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

        public int DeleteMeal(Meal model)
        {
            return _database.Delete(model);
        }
    }
}
