using System.Collections.Generic;

namespace NeverSkipLegDay.Models.DAL
{
    public interface IRecordDal
    {
        List<Record> GetRecords();
        Record GetRecord(int id);
        List<Record> GetRecordsByExerciseId(int exerciseId);
        int SaveRecord(Record model);
        int DeleteRecord(Record model);
    }
}
