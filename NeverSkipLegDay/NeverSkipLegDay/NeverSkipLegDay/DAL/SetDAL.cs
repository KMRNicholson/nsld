using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.DAL
{
    public class SetDAL
    {
        readonly SQLiteAsyncConnection _database;

        public SetDAL(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Set>().Wait();
        }

        public Task<List<Set>> GetSetsAsync()
        {
            return _database.Table<Set>().ToListAsync();
        }
        public Task<List<Set>> GetSetsByExerciseIdAsync(int exerciseId)
        {
            return _database.Table<Set>()
                .Where(i => i.ExerciseID == exerciseId)
                .ToListAsync();
        }
        public Task<Set> GetSetAsync(int id)
        {
            return _database.Table<Set>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSetAsync(Set model)
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

        public Task<int> DeleteSetAsync(Set model)
        {
            return _database.DeleteAsync(model);
        }
    }
}
