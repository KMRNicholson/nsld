using System;
using System.Collections.Generic;
using System.Text;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class WorkoutsViewModel
    {
        public List<Workout> WorkoutList { get; set; }
        public WorkoutsViewModel(List<Workout> workouts)
        {
            WorkoutList = workouts;
        }

        public bool IsEmpty()
        {
            return WorkoutList.Count == 0 ? true : false;
        }
    }
}
