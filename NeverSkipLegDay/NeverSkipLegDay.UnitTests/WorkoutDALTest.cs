using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeverSkipLegDay.DAL;

namespace NeverSkipLegDay.UnitTests
{
    [TestClass]
    public class WorkoutDALTest
    {
        static WorkoutDAL workoutDAL = new WorkoutDAL(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workouts.db3"));
        [TestMethod]
        public void SaveNoteAsyncTest()
        {
            
        }
    }
}
