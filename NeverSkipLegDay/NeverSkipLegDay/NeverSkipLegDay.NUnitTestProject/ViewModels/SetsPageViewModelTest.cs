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
    public class SetsPageViewModelTest
    {
        private SetDatabase mockDatabase;
        private MockPageService pageService;
        private SetsPageViewModel viewModel;
        private List<Set> sets;
        private List<Set> allSets;
        private ExerciseViewModel exerciseViewModel;
        private Exercise exercise;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new SetDatabase();
            pageService = new MockPageService();
            exercise = new ExerciseDatabase().GetExercises().FirstOrDefault();
            exerciseViewModel = new ExerciseViewModel(exercise);
            viewModel = new SetsPageViewModel(exerciseViewModel, mockDatabase, pageService);
            sets = mockDatabase.GetSetsByExerciseId(exercise.Id);
            allSets = mockDatabase.GetSets();
            viewModel.LoadDataCommand.Execute(null);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(viewModel, null);
            Assert.AreEqual(viewModel.Exercise, exerciseViewModel);
            Assert.AreEqual(viewModel.PageTitle, "SETS");
            Assert.AreEqual(viewModel.ButtonText, "Add Set");
            Assert.AreEqual(viewModel.Sets.Count, sets.Count);
        }

        [Test]
        public void LoadDataTest()
        {
            Assert.AreNotEqual(viewModel.Sets.Count, 0);
            Assert.AreNotEqual(viewModel.Sets.Count, allSets.Count);
            Assert.AreEqual(sets.Count, viewModel.Sets.Count);
        }

        [Test]
        public void AddCommand()
        {
            int beforeAddViewModel = viewModel.Sets.Count;
            int beforeAddDb = sets.Count;

            Assert.AreEqual(viewModel.Sets.Count, sets.Count);

            viewModel.AddCommand.Execute(null);

            sets = mockDatabase.GetSetsByExerciseId(viewModel.Exercise.Id);

            Assert.AreNotEqual(viewModel.Sets.Count, beforeAddViewModel);
            Assert.AreNotEqual(sets.Count, beforeAddDb);
            Assert.AreEqual(viewModel.Sets.Count, beforeAddViewModel + 1);
            Assert.AreEqual(sets.Count, beforeAddDb + 1);
        }

        [Test]
        public async Task BatchSave()
        {
            SetViewModel firstSetViewModel = viewModel.Sets.FirstOrDefault();
            SetViewModel secondSetViewModel = viewModel.Sets.Last();

            Set firstSetFromDb = mockDatabase.GetSet(firstSetViewModel.Id);
            Set secondSetFromDb = mockDatabase.GetSet(secondSetViewModel.Id);

            Assert.AreEqual(firstSetViewModel.Id, firstSetFromDb.Id);
            Assert.AreEqual(firstSetViewModel.Reps, firstSetFromDb.Reps);
            Assert.AreEqual(firstSetViewModel.Weight, firstSetFromDb.Weight);

            Assert.AreEqual(secondSetViewModel.Id, secondSetFromDb.Id);
            Assert.AreEqual(secondSetViewModel.Reps, secondSetFromDb.Reps);
            Assert.AreEqual(secondSetViewModel.Weight, secondSetFromDb.Weight);

            firstSetViewModel.Reps -= 1;
            firstSetViewModel.Weight += 5;
            secondSetViewModel.Reps -= 1;
            secondSetViewModel.Weight += 5;

            viewModel.BatchSaveCommand.Execute(null);

            Set newSetFromDb1 = mockDatabase.GetSet(firstSetViewModel.Id);
            Set newSetFromDb2 = mockDatabase.GetSet(secondSetViewModel.Id);

            Assert.AreEqual(firstSetViewModel.Id, newSetFromDb1.Id);
            Assert.AreEqual(firstSetViewModel.Reps, newSetFromDb1.Reps);
            Assert.AreEqual(firstSetViewModel.Weight, newSetFromDb1.Weight);

            Assert.AreEqual(secondSetViewModel.Id, newSetFromDb2.Id);
            Assert.AreEqual(secondSetViewModel.Reps, newSetFromDb2.Reps);
            Assert.AreEqual(secondSetViewModel.Weight, newSetFromDb2.Weight);
        }

        [Test]
        public async Task DeleteCommand()
        {
            SetViewModel SetViewModel = viewModel.Sets.FirstOrDefault();
            Set Set = sets.FirstOrDefault();

            Assert.AreEqual(viewModel.Sets.Count, sets.Count);
            Assert.AreNotEqual(SetViewModel, null);
            Assert.AreNotEqual(Set, null);

            await viewModel.DeleteSet(SetViewModel);

            SetViewModel deletedSetFromViewModel = viewModel.Sets.Where(w => w.Id == SetViewModel.Id).ToList().FirstOrDefault();
            Set deletedSetFromViewDb = mockDatabase.GetSet(SetViewModel.Id);

            Assert.AreEqual(deletedSetFromViewModel, null);
            Assert.AreEqual(deletedSetFromViewDb, null);
        }

        [Test]
        public async Task DeleteNullCommand()
        {
            int numInList = viewModel.Sets.Count;
            int numInDb = sets.Count;

            Assert.AreEqual(viewModel.Sets.Count, sets.Count);
            Assert.AreEqual(viewModel.Sets.Count, numInList);
            Assert.AreEqual(sets.Count, numInDb);

            await viewModel.DeleteSet(null);

            Assert.AreEqual(viewModel.Sets.Count, sets.Count);
            Assert.AreEqual(viewModel.Sets.Count, numInList);
            Assert.AreEqual(sets.Count, numInDb);
        }
    }
}
