using System;
using System.Collections.Generic;
using System.Text;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class Workouts
    {
        public List<Workout> WorkoutList { get; set; }
        public Workouts(List<Workout> workouts)
        {
            WorkoutList = workouts;
        }

        public bool IsEmpty()
        {
            return WorkoutList.Count == 0 ? true : false;
        }
    }
}
