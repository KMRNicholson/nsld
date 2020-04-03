using System.Collections.Generic;
using System.Linq;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.NUnitTestProject.Database
{
    public class RecordDatabase : IRecordDal
    {
        public List<Record> Records { get; set; }
        public RecordDatabase()
        {
            Records = new List<Record>
            {
                new Record(){ Id = 1, ExerciseId = 2, Reps = 8, Weight = 110 },
                new Record(){ Id = 2, ExerciseId = 2, Reps = 5, Weight = 120 },
                new Record(){ Id = 3, ExerciseId = 3, Reps = 12, Weight = 90 },
                new Record(){ Id = 4, ExerciseId = 1, Reps = 3, Weight = 150 },
                new Record(){ Id = 5, ExerciseId = 1, Reps = 1, Weight = 160 },
                new Record(){ Id = 6, ExerciseId = 3, Reps = 15, Weight = 20 }
            };
        }

        public List<Record> GetRecords()
        {
            return Records;
        }

        public Record GetRecord(int id)
        {
            return Records.Where(s => s.Id == id).ToList().FirstOrDefault();
        }

        public List<Record> GetRecordsByExerciseId(int exerciseId)
        {
            return Records.Where(s => s.ExerciseId == exerciseId).ToList();
        }

        public int SaveRecord(Record model)
        {
            if (model.Id != 0)
            {
                Records.ForEach(s => {
                    if (s.Id == model.Id)
                    {
                        s.Reps = model.Reps;
                        s.Weight = model.Weight;
                    }
                });
                return 1;
            }
            else
            {
                model.Id = Records.Last().Id + 1;
                Records.Add(model);
                return Records.Find(s => s == model) != null ? 1 : 0;
            }
        }

        public int DeleteRecord(Record model)
        {
            Records.Remove(model);
            return Records.Find(s => s == model) == null ? 1 : 0;
        }
    }
}
