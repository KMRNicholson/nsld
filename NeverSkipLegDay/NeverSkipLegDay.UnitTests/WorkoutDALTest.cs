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
        static WorkoutDAL workoutDAL = new WorkoutDAL(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workouts.db3"));
        
        [TestMethod]
        public void SaveNoteAsyncTest()
        {
            Workout workout = new Workout();
            workout.ID = 1;
            workout.Name = "Push Day";
            workout.Date = new DateTime().ToUniversalTime();
            Task<int> result = workoutDAL.SaveNoteAsync(workout);
            result.Wait();

            Assert.AreEqual(result.Result, 0);
        }
    }
}
