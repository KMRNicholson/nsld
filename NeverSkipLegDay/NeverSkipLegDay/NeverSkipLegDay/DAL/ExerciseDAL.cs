using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.DAL
{
    public class ExerciseDAL
    {
        readonly SQLiteAsyncConnection _database;

        public ExerciseDAL(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Exercise>().Wait();
        }

        public Task<List<Exercise>> GetExercisesAsync()
        {
            return _database.Table<Exercise>().ToListAsync();
        }
        public Task<List<Exercise>> GetExercisesByWorkoutIdAsync(int workoutId)
        {
            return _database.Table<Exercise>()
                .Where(i => i.WorkoutID == workoutId)
                .ToListAsync();
        }
        public Task<Exercise> GetExerciseAsync(int id)
        {
            return _database.Table<Exercise>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveExerciseAsync(Exercise model)
        {
            if (model.ID != 0)
            {
                return _database.UpdateAsync(model);
            }
            else
            {
                return _database.InsertAsync(model);
            }
        }

        public Task<int> DeleteExerciseAsync(Exercise model)
        {
            return _database.DeleteAsync(model);
        }
    }
}
