using NUnit.Framework;
using NeverSkipLegDay.Models;
using System.Collections.Generic;
using NeverSkipLegDay.NUnitTestProject.Database;
using System.Linq;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class ExerciseTest
    {
        private ExerciseDatabase mockDatabase;
        private Exercise exercise;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new ExerciseDatabase();
            exercise = mockDatabase.GetExercises().FirstOrDefault();
        }

        [Test]
        public void ExerciseConstructorTest()
        {
            Assert.AreNotEqual(exercise.Id, null);
            Assert.AreNotEqual(exercise.Name, null);
        }

        [Test]
        public void GetRepsTotalTest()
        {
            SetDatabase setDatabase = new SetDatabase();
            List<Set> sets = setDatabase.GetSetsByExerciseId(exercise.Id);

            int? totals = sets.Select(x => x.Reps).Sum();

            int? totalsFromExercise = exercise.GetRepsTotal(setDatabase);

            Assert.AreEqual(totalsFromExercise, totals);
        }

        [Test]
        public void GetSetsTotalTest()
        {
            SetDatabase setDatabase = new SetDatabase();
            int totalSetsFromDb = setDatabase.GetSetsByExerciseId(exercise.Id).Count;

            int? totalSetsFromModel = exercise.GetSetsTotal(setDatabase);

            Assert.AreEqual(totalSetsFromModel, totalSetsFromDb);
        }
    }
}