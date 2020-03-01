using System.Collections.Generic;
using System.Linq;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.NUnitTestProject.Database
{
    public class WorkoutDatabase : IWorkoutDal
    {
        public List<Workout> workouts { get; set; }
        public WorkoutDatabase()
        {
            workouts = new List<Workout>
            {
                new Workout(){ Id = 1, Name = "Pull Day 1"},
                new Workout(){ Id = 2, Name = "Push Day 1"},
                new Workout(){ Id = 3, Name = "Leg Day 1"},
                new Workout(){ Id = 4, Name = "Pull Day 2"},
                new Workout(){ Id = 5, Name = "Push Day 2"},
                new Workout(){ Id = 6, Name = "Leg Day 2"}
            };
            
        }

        public List<Workout> GetWorkouts()
        {
            return workouts;
        }

        public Workout GetWorkout(int id)
        {
            return workouts.Where(w => w.Id == id).ToList().FirstOrDefault();
        }

        public int SaveWorkout(Workout model)
        {
            if (model.Id != 0)
            {
                Workout workoutInDb = workouts.Where(w => w.Id == model.Id).ToList().FirstOrDefault();
                workoutInDb.Name = model.Name;
                return 1;
            }
            else
            {
                model.Id = workouts.Last().Id + 1;
                workouts.Add(model);
                return workouts.Find(w => w == model) != null ? 1 : 0;
            }
        }

        public int DeleteWorkout(Workout model)
        {
            workouts.Remove(model);
            return workouts.Find(w => w == model) == null ? 1 : 0;
        }
    }
}
