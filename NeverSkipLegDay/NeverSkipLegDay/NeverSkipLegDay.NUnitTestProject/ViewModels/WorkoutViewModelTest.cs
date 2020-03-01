using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.UnitTests.Database;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.UnitTests.ViewModels
{
    [TestFixture]
    public class WorkoutViewModelTest
    {
        private WorkoutDatabase _db = new WorkoutDatabase();
        
        [Test]
        public void ConstructorTest()
        {
            Workout workout = _db.GetWorkouts().FirstOrDefault();
            WorkoutViewModel workoutViewModel = new WorkoutViewModel(workout);

            Assert.AreEqual(workoutViewModel.Id, workout.Id);
            Assert.AreEqual(workoutViewModel.Name, workout.Name);
        }

        [Test]
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
