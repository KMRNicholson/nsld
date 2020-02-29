using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.Models.DAL
{
    public class SetDal
    {
        readonly SQLiteAsyncConnection _database;

        public SetDal(SQLiteDB db)
        {
            _database = db.GetConnection();
            _database.CreateTableAsync<Set>().Wait();
        }

        public Task<List<Set>> GetSetsAsync()
        {
            return _database.Table<Set>().ToListAsync();
        }
        public Task<List<Set>> GetSetsByExerciseIdAsync(int exerciseId)
        {
            return _database.Table<Set>()
                .Where(i => i.ExerciseId == exerciseId)
                .ToListAsync();
        }
        public Task<Set> GetSetAsync(int id)
        {
            return _database.Table<Set>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSetAsync(Set model)
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

        public Task<int> DeleteSetAsync(Set model)
        {
            return _database.DeleteAsync(model);
        }
    }
}
