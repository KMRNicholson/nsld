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
    }
}
