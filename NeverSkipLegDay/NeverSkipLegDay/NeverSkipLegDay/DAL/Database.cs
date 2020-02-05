using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace NeverSkipLegDay.DAL
{
    class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Models.Workout>().Wait();
        }

        public Task<List<Models.Model>> GetAllAsync()
        {
            return _database.Table<Models.Model>().ToListAsync();
        }
        public Task<Models.Model> GetNoteAsync(int id)
        {
            return _database.Table<Models.Model>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Models.Model model)
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

        public Task<int> DeleteNoteAsync(Models.Model model)
        {
            return _database.DeleteAsync(model);
        }
    }
}
