using System.Linq;
using NUnit.Framework;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.NUnitTestProject.Database;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class ExerciseViewModelTest
    {
        private ExerciseDatabase mockDatabase;
        private ExerciseViewModel viewModel;
        private Exercise Exercise;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new ExerciseDatabase();
            Exercise = mockDatabase.GetExercises().FirstOrDefault();
            viewModel = new ExerciseViewModel(Exercise);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(viewModel.Id, Exercise.Id);
            Assert.AreEqual(viewModel.WorkoutId, Exercise.WorkoutId);
            Assert.AreEqual(viewModel.Name, Exercise.Name);
        }

        [Test]
        public void OnPropertyChangedTest()
        {
            string newName = "New Exercise";

            viewModel.Name = newName;

            Assert.AreNotEqual(viewModel.Name, Exercise.Name);
            Assert.AreEqual(viewModel.Name, newName);
        }
    }
}
