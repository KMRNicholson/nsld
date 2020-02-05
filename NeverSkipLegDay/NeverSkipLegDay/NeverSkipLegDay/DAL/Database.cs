using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.DAL
{
    class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Workout>().Wait();
        }
    }
}
