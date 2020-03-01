using System.Collections.Generic;
using SQLite;

namespace NeverSkipLegDay.Models.DAL
{
    public class WorkoutDal : IWorkoutDal
    {
        readonly SQLiteConnection _database;
        public WorkoutDal(SQLiteDB db)
        {
            _database = db.GetConnection();
            _database.CreateTable<Workout>();
        }

        public List<Workout> GetWorkouts()
        {
            return _database.Table<Workout>().ToList();
        }
        public Workout GetWorkout(int id)
        {
            return _database.Table<Workout>()
                            .Where(i => i.Id == id)
                            .FirstOrDefault();
        }

        public int SaveWorkout(Workout model)
        {
            if (model.Id != 0)
            {
                return _database.Update(model);
            }
            else
            {
                return _database.Insert(model);
            }
        }

        public int DeleteWorkout(Workout model)
        {
            return _database.Delete(model);
        }
    }
}
