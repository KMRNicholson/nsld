using System.Collections.Generic;
using SQLite;

namespace NeverSkipLegDay.Models.DAL
{
    public class RecordDal : IRecordDal
    {
        readonly SQLiteConnection _database;
        public RecordDal(SQLiteDB db)
        {
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

        public int SaveRecord(Record model)
        {
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
