using System.Collections.Generic;
using SQLite;

namespace NeverSkipLegDay.Models.DAL
{
    public class ExerciseDal : IExerciseDal
    {
        readonly SQLiteConnection _database;

        public ExerciseDal(SQLiteDB db)
        {
            _database = db.GetConnection();
            _database.CreateTable<Exercise>();
        }

        public List<Exercise> GetExercises()
        {
            return _database.Table<Exercise>().ToList();
        }
        public List<Exercise> GetExercisesByWorkoutId(int workoutId)
        {
            return _database.Table<Exercise>()
                .Where(i => i.WorkoutId == workoutId)
                .ToList();
        }
        public Exercise GetExercise(int id)
        {
            return _database.Table<Exercise>()
                            .Where(i => i.Id == id)
                            .FirstOrDefault();
        }

        public int SaveExercise(Exercise model)
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

        public int DeleteExercise(Exercise model)
        {
            return _database.Delete(model);
        }
    }
}
