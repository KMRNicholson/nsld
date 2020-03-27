using NUnit.Framework;
using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models;
using System.Collections.Generic;
using NeverSkipLegDay.NUnitTestProject.Database;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class AddEditRecordPageViewModelTest
    {
        private RecordDatabase mockDatabase;
        private MockPageService pageService;
        private AddEditRecordPageViewModel editViewModel;
        private AddEditRecordPageViewModel addViewModel;
        private RecordViewModel recordViewModel;
        private List<Record> recordsInDb;
        private Record record;
        private Exercise newExercise;
        private Exercise exercise;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new RecordDatabase();
            pageService = new MockPageService();
            exercise = new ExerciseDatabase().GetExercises().FirstOrDefault();
            newExercise = new ExerciseDatabase().GetExercises().Last();

            addViewModel = new AddEditRecordPageViewModel(new RecordViewModel() { ExerciseId = newExercise.Id }, mockDatabase, pageService);

            recordsInDb = mockDatabase.GetRecordsByExerciseId(exercise.Id);
            record = recordsInDb.FirstOrDefault();
            recordViewModel = new RecordViewModel(record);
            editViewModel = new AddEditRecordPageViewModel(recordViewModel, mockDatabase, pageService);
        }

        [Test]
        public void EditRecordConstructorTest()
        {
            Assert.AreNotEqual(editViewModel, null);
            Assert.AreEqual(editViewModel.Record.Id, record.Id);
            Assert.AreEqual(editViewModel.Record.ExerciseId, record.ExerciseId);
            Assert.AreEqual(editViewModel.Record.Reps, record.Reps);
            Assert.AreEqual(editViewModel.Record.Weight, record.Weight);
            Assert.AreEqual(editViewModel.PageTitle, "RECORD");
        }

        [Test]
        public void AddRecordConstructorTest()
        {
            Assert.AreNotEqual(addViewModel, null);
            Assert.AreEqual(addViewModel.Record.Id, 0);
            Assert.AreEqual(addViewModel.Record.ExerciseId, newExercise.Id);
            Assert.AreEqual(addViewModel.Record.Reps, 0);
            Assert.AreEqual(addViewModel.Record.Weight, 0);
            Assert.AreEqual(editViewModel.PageTitle, "RECORD");
        }

        [Test]
        public async Task SaveNewRecordTest()
        {
            Assert.AreEqual(addViewModel.Record.Id, 0);
            Assert.AreEqual(addViewModel.Record.ExerciseId, newExercise.Id);
            Assert.AreEqual(addViewModel.Record.Reps, 0);
            Assert.AreEqual(addViewModel.Record.Weight, 0);

            addViewModel.Record.Reps = 1;
            addViewModel.Record.Weight = 100;

            await addViewModel.Save();

            Record recordInDb = mockDatabase.GetRecord(addViewModel.Record.Id);

            Assert.AreNotEqual(addViewModel.Record.Id, 0);
            Assert.AreNotEqual(recordInDb, null);
            Assert.AreEqual(addViewModel.Record.Reps, 1);
            Assert.AreEqual(addViewModel.Record.Reps, recordInDb.Reps);
            Assert.AreEqual(addViewModel.Record.Weight, 100);
            Assert.AreEqual(addViewModel.Record.Weight, recordInDb.Weight);
        }

        [Test]
        public async Task SaveExistingRecordTest()
        {
            Assert.AreEqual(editViewModel.Record.Id, record.Id);
            Assert.AreEqual(editViewModel.Record.ExerciseId, record.ExerciseId);
            Assert.AreEqual(editViewModel.Record.Reps, record.Reps);
            Assert.AreEqual(editViewModel.Record.Weight, record.Weight);

            editViewModel.Record.Reps = 1;
            editViewModel.Record.Weight = 100;

            await editViewModel.Save();

            Record editedRecordInDb = mockDatabase.GetRecord(editViewModel.Record.Id);

            Assert.AreEqual(editViewModel.Record.Reps, 1);
            Assert.AreEqual(editViewModel.Record.Reps, editedRecordInDb.Reps);
            Assert.AreEqual(editViewModel.Record.Weight, 100);
            Assert.AreEqual(editViewModel.Record.Weight, editedRecordInDb.Weight);
        }

        [Test]
        public async Task SaveNewRecordWithEmtpyValuesTest()
        {
            Assert.AreEqual(addViewModel.Record.Id, 0);
            Assert.AreEqual(addViewModel.Record.Reps, 0);
            Assert.AreEqual(addViewModel.Record.Weight, 0);

            await addViewModel.Save();

            Record recordInDb = mockDatabase.GetRecord(addViewModel.Record.Id);

            Assert.AreEqual(addViewModel.Record.Id, 0);
            Assert.AreEqual(recordInDb, null);
        }

        [Test]
        public async Task SaveExistingRecordWithEmtpyValuesTest()
        {
            Assert.AreEqual(editViewModel.Record.Id, record.Id);
            Assert.AreEqual(editViewModel.Record.Reps, record.Reps);
            Assert.AreEqual(editViewModel.Record.Weight, record.Weight);

            editViewModel.Record.Reps = 0;
            editViewModel.Record.Weight = 0;

            await editViewModel.Save();

            Record editedRecordInDb = mockDatabase.GetRecord(editViewModel.Record.Id);

            Assert.AreNotEqual(editViewModel.Record.Reps, editedRecordInDb.Reps);
            Assert.AreNotEqual(editViewModel.Record.Reps, editedRecordInDb.Reps);
        }
    }
}