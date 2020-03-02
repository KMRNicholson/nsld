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
            Assert.AreEqual(viewModel.AddButtonText, "Add Set");
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

            viewModel.AddSet();

            sets = mockDatabase.GetSetsByExerciseId(viewModel.Exercise.Id);

            Assert.AreNotEqual(viewModel.Sets.Count, beforeAddViewModel);
            Assert.AreNotEqual(sets.Count, beforeAddDb);
            Assert.AreEqual(viewModel.Sets.Count, beforeAddViewModel + 1);
            Assert.AreEqual(sets.Count, beforeAddDb + 1);
        }

        [Test]
        public void EditCommand()
        {
            SetViewModel setViewModel = viewModel.Sets.FirstOrDefault();

            Set setFromDb = mockDatabase.GetSet(setViewModel.Id);

            Assert.AreEqual(setViewModel.Id, setFromDb.Id);
            Assert.AreEqual(setViewModel.Reps, setFromDb.Reps);
            Assert.AreEqual(setViewModel.Weight, setFromDb.Weight);

            setViewModel.Reps -= 1;
            setViewModel.Weight += 5;

            viewModel.EditSet(setViewModel);

            Set newSetFromDb = mockDatabase.GetSet(setViewModel.Id);

            Assert.AreEqual(setViewModel.Id, setFromDb.Id);
            Assert.AreEqual(setViewModel.Reps, newSetFromDb.Reps);
            Assert.AreEqual(setViewModel.Weight, newSetFromDb.Weight);
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
