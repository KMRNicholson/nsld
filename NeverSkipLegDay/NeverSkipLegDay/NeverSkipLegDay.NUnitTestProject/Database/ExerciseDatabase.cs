using System.Collections.Generic;
using System.Linq;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.NUnitTestProject.Database
{
    public class ExerciseDatabase : IExerciseDal
    {
        public List<Exercise> Exercises { get; set; }
        public ExerciseDatabase()
        {
            Exercises = new List<Exercise>
            {
                new Exercise(){ Id = 1, WorkoutId = 2, Name = "Bench Press"},
                new Exercise(){ Id = 2, WorkoutId = 2, Name = "Overhead Press"},
                new Exercise(){ Id = 3, WorkoutId = 3, Name = "Lunges"},
                new Exercise(){ Id = 4, WorkoutId = 1, Name = "Pullups"},
                new Exercise(){ Id = 5, WorkoutId = 1, Name = "Deadlift"},
                new Exercise(){ Id = 6, WorkoutId = 3, Name = "Squat"}
            };
        }

        public List<Exercise> GetExercises()
        {
            return Exercises;
        }

        public Exercise GetExercise(int id)
        {
            return Exercises.Where(w => w.Id == id).ToList().FirstOrDefault();
        }

        public List<Exercise> GetExercisesByWorkoutId(int workoutId)
        {
            return Exercises.Where(w => w.WorkoutId == workoutId).ToList();
        }

        public int SaveExercise(Exercise model)
        {
            if (model.Id != 0)
            {
                Exercises.ForEach(w => { if (w.Id == model.Id) w.Name = model.Name; });
                return 1;
            }
            else
            {
                model.Id = Exercises.Last().Id + 1;
                Exercises.Add(model);
                return Exercises.Find(w => w == model) != null ? 1 : 0;
            }
        }

        public int DeleteExercise(Exercise model)
        {
            Exercises.Remove(model);
            return Exercises.Find(w => w == model) == null ? 1 : 0;
        }
    }
}
