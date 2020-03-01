using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.UnitTests.Database;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.UnitTests.ViewModels
{
    [TestClass]
    public class WorkoutViewModelTest
    {
        private WorkoutDatabase _db = new WorkoutDatabase();
        [TestMethod]
        public void ConstructorTest()
        {
            Workout workout = _db.GetWorkouts().FirstOrDefault();
            WorkoutViewModel workoutViewModel = new WorkoutViewModel(workout);

            Assert.AreEqual(workoutViewModel.Id, workout.Id);
            Assert.AreEqual(workoutViewModel.Name, workout.Name);
        }

        [TestMethod]
        public void OnPropertyChangedTest()
        {
            Workout workout = _db.GetWorkouts().FirstOrDefault();
            WorkoutViewModel workoutViewModel = new WorkoutViewModel(workout);

            string newName = "New Workout";

            workoutViewModel.Name = newName;

            Assert.AreNotEqual(workoutViewModel.Name, workout.Name);
            Assert.AreEqual(workoutViewModel.Name, newName);
        }
    }
}
