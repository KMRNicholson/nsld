using System.Linq;
using NUnit.Framework;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.NUnitTestProject.Database;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class WorkoutViewModelTest
    {
        private WorkoutDatabase mockDatabase;
        private WorkoutViewModel viewModel;
        private Workout workout;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new WorkoutDatabase();
            workout = mockDatabase.GetWorkouts().FirstOrDefault();
            viewModel = new WorkoutViewModel(workout);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(viewModel.Id, workout.Id);
            Assert.AreEqual(viewModel.Name, workout.Name);
        }

        [Test]
        public void OnPropertyChangedTest()
        {
            string newName = "New Workout";

            viewModel.Name = newName;

            Assert.AreNotEqual(viewModel.Name, workout.Name);
            Assert.AreEqual(viewModel.Name, newName);
        }
    }
}
