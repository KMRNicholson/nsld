using System;
using System.Collections.Generic;
using System.Text;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class WorkoutList
    {
        public List<Workout> Workouts { get; set; }
        public WorkoutList(List<Workout> workouts)
        {
            Workouts = workouts;
        }

        public bool IsEmpty()
        {
            return Workouts.Count == 0 ? true : false;
        }
    }
}
