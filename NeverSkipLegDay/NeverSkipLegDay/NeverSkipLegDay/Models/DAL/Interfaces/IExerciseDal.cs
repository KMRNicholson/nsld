using System.Collections.Generic;

namespace NeverSkipLegDay.Models.DAL
{
    public interface IExerciseDal
    {
        List<Exercise> GetExercises();
        Exercise GetExercise(int id);
        List<Exercise> GetExercisesByWorkoutId(int workoutId);
        int SaveExercise(Exercise model);
        int DeleteExercise(Exercise model);
    }
}
