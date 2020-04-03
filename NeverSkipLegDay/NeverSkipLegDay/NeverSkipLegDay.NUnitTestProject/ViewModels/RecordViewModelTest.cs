using System.Linq;
using NUnit.Framework;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.NUnitTestProject.Database;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class RecordViewModelTest
    {
        private RecordDatabase mockDatabase;
        private RecordViewModel viewModel;
        private Record Record;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new RecordDatabase();
            Record = mockDatabase.GetRecords().FirstOrDefault();
            viewModel = new RecordViewModel(Record);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(viewModel.Id, Record.Id);
            Assert.AreEqual(viewModel.ExerciseId, Record.ExerciseId);
            Assert.AreEqual(viewModel.Reps, Record.Reps);
            Assert.AreEqual(viewModel.Weight, Record.Weight);
        }

        [Test]
        public void OnPropertyChangedTest()
        {
            viewModel.Reps -= 1;
            viewModel.Weight += 5;

            Assert.AreEqual(viewModel.Reps, Record.Reps - 1);
            Assert.AreEqual(viewModel.Weight, Record.Weight + 5);
        }
    }
}