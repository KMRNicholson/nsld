using System.Collections.Generic;
using System.Linq;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.UnitTests.Database
{
    public class WorkoutDatabase
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
            return this.workouts;
        }

        public Workout GetWorkoutById(int id)
        {
            return this.workouts.Where(w => w.Id == id).ToList().FirstOrDefault();
        }

        public int SaveWorkout(Workout workout)
        {
            if(workout.Id != 0)
            {
                Workout workoutInDb = workouts.Where(w => w.Id == workout.Id).ToList().FirstOrDefault();
                workoutInDb.Name = workout.Name;
                return 1;
            }
            else
            {
                workouts.Add(workout);
                return workouts.Find(w => w == workout) != null ? 1 : 0;
            }
        }

        public int DeleteWorkout(Workout workout)
        {
            workouts.Remove(workout);
            return workouts.Find(w => w == workout) == null ? 1 : 0;
        }
    }
}
