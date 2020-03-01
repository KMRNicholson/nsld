using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;

namespace NeverSkipLegDay.Models.DAL
{
    public interface IWorkoutDal
    { 
        List<Workout> GetWorkouts();
        Workout GetWorkout(int id);
        int SaveWorkout(Workout model);
        int DeleteWorkout(Workout model);
    }
}
