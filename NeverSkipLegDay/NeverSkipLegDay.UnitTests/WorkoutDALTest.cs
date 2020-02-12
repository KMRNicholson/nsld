using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeverSkipLegDay.DAL;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.UnitTests
{
    [TestClass]
    public class WorkoutDALTest
    {        
        [TestMethod]
        public void InsertNoteAsyncTest()
        {
            WorkoutDAL workoutDAL = new WorkoutDAL(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workouts.db3"));
            Workout workout = new Workout();
            workout.ID = 1;
            workout.Name = "Push Day";
            workout.Date = new DateTime().ToUniversalTime();
            Task<int> saveTask = workoutDAL.SaveNoteAsync(workout);
            saveTask.Wait();

            Assert.AreEqual(saveTask.Result, 0);
        }

        [TestMethod]
        public void UpdateNoteAsyncTest()
        {
            WorkoutDAL workoutDAL = new WorkoutDAL(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workouts.db3"));
            Workout workout = new Workout();
            workout.ID = 1;
            workout.Name = "Pull Day";
            workout.Date = new DateTime().ToUniversalTime();
            Task<int> saveTask = workoutDAL.SaveNoteAsync(workout);
            saveTask.Wait();

            Assert.AreEqual(saveTask.Result, 0);

            Task<Workout> readTask = workoutDAL.GetNoteAsync(workout.ID);
            readTask.Wait();

            Workout updatedWorkout = readTask.Result;

            Assert.AreEqual(updatedWorkout.Name, workout.Name);
        }
    }
}
