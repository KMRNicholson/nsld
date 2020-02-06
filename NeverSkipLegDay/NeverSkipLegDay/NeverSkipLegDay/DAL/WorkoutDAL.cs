using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.DAL
{
    public class WorkoutDAL
    {
        readonly SQLiteAsyncConnection _database;

        public WorkoutDAL(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Workout>().Wait();
        }

        public Task<List<Workout>> GetAllAsync<T>()
        {
            return _database.Table<Workout>().ToListAsync();
        }
        public Task<Workout> GetNoteAsync(int id)
        {
            return _database.Table<Workout>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Workout model)
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

        public Task<int> DeleteNoteAsync(Workout model)
        {
            return _database.DeleteAsync(model);
        }
    }
}
