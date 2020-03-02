using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using NeverSkipLegDay.Models;
using System.IO;

namespace NeverSkipLegDay.Models.DAL
{
    public class ExerciseDal
    {
        readonly SQLiteAsyncConnection _database;
        


        public ExerciseDal(SQLiteDB db)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(appData, "SQLiteNSLD.db3");
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
                .Where(i => i.WorkoutId == workoutId)
                .ToListAsync();
        }
        public Task<Exercise> GetExerciseAsync(int id)
        {
            return _database.Table<Exercise>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveExerciseAsync(Exercise model)
        {
            if (model.Id != 0)
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
