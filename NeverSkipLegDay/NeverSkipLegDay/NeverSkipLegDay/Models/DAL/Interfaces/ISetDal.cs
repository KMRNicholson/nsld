using System.Collections.Generic;

namespace NeverSkipLegDay.Models.DAL
{
    public interface ISetDal
    {
        List<Set> GetSets();
        Set GetSet(int id);
        List<Set> GetSetsByExerciseId(int exerciseId);
        int SaveSet(Set model);
        int DeleteSet(Set model);
    }
}
