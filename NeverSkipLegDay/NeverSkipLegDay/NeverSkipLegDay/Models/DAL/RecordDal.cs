using System;
using System.Collections.Generic;

using SQLite;

namespace NeverSkipLegDay.Models.DAL
{
    public class RecordDal : IRecordDal
    {
        readonly SQLiteConnection _database;
        public RecordDal(SQLiteDB db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _database = db.GetConnection();
            _database.CreateTable<Record>();
        }

        public List<Record> GetRecords()
        {
            return _database.Table<Record>().ToList();
        }
        public Record GetRecord(int id)
        {
            return _database.Table<Record>()
                            .Where(i => i.Id == id)
                            .FirstOrDefault();
        }
        public List<Record> GetRecordsByExerciseId(int exerciseId)
        {
            return _database.Table<Record>()
                .Where(i => i.ExerciseId == exerciseId)
                .ToList();
        }

        public int SaveRecord(Record model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.Id != 0)
            {
                return _database.Update(model);
            }
            else
            {
                return _database.Insert(model);
            }
        }

        public int DeleteRecord(Record model)
        {
            return _database.Delete(model);
        }
    }
}
