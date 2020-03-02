using System.Linq;
using NUnit.Framework;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.NUnitTestProject.Database;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class SetViewModelTest
    {
        private SetDatabase mockDatabase;
        private SetViewModel viewModel;
        private Set Set;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new SetDatabase();
            Set = mockDatabase.GetSets().FirstOrDefault();
            viewModel = new SetViewModel(Set);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(viewModel.Id, Set.Id);
            Assert.AreEqual(viewModel.ExerciseId, Set.ExerciseId);
            Assert.AreEqual(viewModel.Reps, Set.Reps);
            Assert.AreEqual(viewModel.Weight, Set.Weight);
        }

        [Test]
        public void OnPropertyChangedTest()
        {
            viewModel.Reps -= 1;
            viewModel.Weight += 5;

            Assert.AreEqual(viewModel.Reps, Set.Reps - 1);
            Assert.AreEqual(viewModel.Weight, Set.Weight + 5);
        }
    }
}