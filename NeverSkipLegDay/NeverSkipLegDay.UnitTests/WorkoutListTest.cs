﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.UnitTests.Database;

namespace NeverSkipLegDay.UnitTests
{
    [TestClass]
    public class WorkoutListTest
    {
        WorkoutDatabase _database = new WorkoutDatabase();

        [TestMethod]
        public void ConstructorTest()
        {
            List<Workout> workouts = _database.GetWorkouts();

            ViewModels.WorkoutsViewModel workoutList = new ViewModels.WorkoutsViewModel(workouts);

            Assert.AreEqual(workouts, workoutList.WorkoutList);
        }

        [TestMethod]
        public void IsEmptyTest()
        {
            List<Workout> workouts = _database.GetWorkouts();

            ViewModels.WorkoutsViewModel workoutList = new ViewModels.WorkoutsViewModel(workouts);
            Assert.IsTrue(workoutList.IsEmpty());

            ViewModels.WorkoutsViewModel emptyWorkoutList = new ViewModels.WorkoutsViewModel(new List<Workout>());
            Assert.IsTrue(emptyWorkoutList.IsEmpty());
        }
    }
}
