using System.Collections.Generic;
using SQLite;

namespace NeverSkipLegDay.Models.DAL
{
    public class SetDal : ISetDal
    {
        readonly SQLiteConnection _database;

        public SetDal(SQLiteDB db)
        {
            _database = db.GetConnection();
            _database.CreateTable<Set>();
        }

        public List<Set> GetSets()
        {
            return _database.Table<Set>().ToList();
        }
        public List<Set> GetSetsByExerciseId(int exerciseId)
        {
            return _database.Table<Set>()
                .Where(i => i.ExerciseId == exerciseId)
                .ToList();
        }
        public Set GetSet(int id)
        {
            return _database.Table<Set>()
                            .Where(i => i.Id == id)
                            .FirstOrDefault();
        }

        public int SaveSet(Set model)
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

        public int DeleteSet(Set model)
        {
            return _database.Delete(model);
        }
    }
}
