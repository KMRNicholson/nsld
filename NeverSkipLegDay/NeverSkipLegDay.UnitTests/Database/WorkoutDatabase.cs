using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new Workout(){ ID = 1, Name = "Pull Day 1" , Date = new DateTime().ToUniversalTime() },
                new Workout(){ ID = 2, Name = "Push Day 1" , Date = new DateTime().ToUniversalTime() },
                new Workout(){ ID = 3, Name = "Leg Day 1" , Date = new DateTime().ToUniversalTime() },
                new Workout(){ ID = 4, Name = "Pull Day 2" , Date = new DateTime().ToUniversalTime() },
                new Workout(){ ID = 5, Name = "Push Day 2" , Date = new DateTime().ToUniversalTime() },
                new Workout(){ ID = 6, Name = "Leg Day 2" , Date = new DateTime().ToUniversalTime() }
            };
        }

        public List<Workout> GetWorkouts()
        {
            return this.workouts;
        }
    }
}
