using System.Collections.Generic;

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
