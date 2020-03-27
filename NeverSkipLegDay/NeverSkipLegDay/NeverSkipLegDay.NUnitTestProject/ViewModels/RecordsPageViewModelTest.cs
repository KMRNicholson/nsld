using NUnit.Framework;
using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models;
using System.Collections.Generic;
using NeverSkipLegDay.NUnitTestProject.Database;
using System.Threading.Tasks;
using System.Linq;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class RecordsPageViewModelTest
    {
        private RecordDatabase mockDatabase;
        private MockPageService pageService;
        private RecordsPageViewModel viewModel;
        private List<Record> records;
        private List<Record> allRecords;
        private ExerciseViewModel exerciseViewModel;
        private Exercise exercise;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new RecordDatabase();
            pageService = new MockPageService();
            exercise = new ExerciseDatabase().GetExercises().FirstOrDefault();
            exerciseViewModel = new ExerciseViewModel(exercise);
            viewModel = new RecordsPageViewModel(exerciseViewModel, mockDatabase, pageService);
            records = mockDatabase.GetRecordsByExerciseId(exercise.Id);
            allRecords = mockDatabase.GetRecords();
            viewModel.LoadDataCommand.Execute(null);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(viewModel, null);
            Assert.AreEqual(viewModel.Exercise, exerciseViewModel);
            Assert.AreEqual(viewModel.ButtonText, "Add Record");
            Assert.AreEqual(viewModel.PageTitle, "RECORDS");
            Assert.AreEqual(viewModel.Records.Count, records.Count);
        }

        [Test]
        public void LoadDataTest()
        {
            Assert.AreNotEqual(viewModel.Records.Count, 0);
            Assert.AreNotEqual(viewModel.Records.Count, allRecords.Count);
            Assert.AreEqual(records.Count, viewModel.Records.Count);
        }

        [Test]
        public async Task DeleteCommand()
        {
            RecordViewModel RecordViewModel = viewModel.Records.FirstOrDefault();
            Record Record = records.FirstOrDefault();

            Assert.AreEqual(viewModel.Records.Count, records.Count);
            Assert.AreNotEqual(RecordViewModel, null);
            Assert.AreNotEqual(Record, null);

            await viewModel.DeleteRecord(RecordViewModel);

            RecordViewModel deletedRecordFromViewModel = viewModel.Records.Where(w => w.Id == RecordViewModel.Id).ToList().FirstOrDefault();
            Record deletedRecordFromViewDb = mockDatabase.GetRecord(RecordViewModel.Id);

            Assert.AreEqual(deletedRecordFromViewModel, null);
            Assert.AreEqual(deletedRecordFromViewDb, null);
        }

        [Test]
        public async Task DeleteNullCommand()
        {
            int numInList = viewModel.Records.Count;
            int numInDb = records.Count;

            Assert.AreEqual(viewModel.Records.Count, records.Count);
            Assert.AreEqual(viewModel.Records.Count, numInList);
            Assert.AreEqual(records.Count, numInDb);

            await viewModel.DeleteRecord(null);

            Assert.AreEqual(viewModel.Records.Count, records.Count);
            Assert.AreEqual(viewModel.Records.Count, numInList);
            Assert.AreEqual(records.Count, numInDb);
        }
    }
}
