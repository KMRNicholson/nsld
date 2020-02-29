using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;

namespace NeverSkipLegDay.Models.DAL
{
    public class WorkoutDal
    {
        readonly SQLiteAsyncConnection _database;
        public WorkoutDal(SQLiteDB db)
        {
            _database = db.GetConnection();
            _database.CreateTableAsync<Workout>().Wait();
        }

        public Task<List<Workout>> GetWorkoutsAsync()
        {
            return _database.Table<Workout>().ToListAsync();
        }
        public Task<Workout> GetWorkoutAsync(int id)
        {
            return _database.Table<Workout>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveWorkoutAsync(Workout model)
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

        public Task<int> DeleteWorkoutAsync(Workout model)
        {
            return _database.DeleteAsync(model);
        }
    }
}
