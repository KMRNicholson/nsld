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
    public class WorkoutsPageViewModelTest
    {
        private WorkoutDatabase mockDatabase;
        private MockPageService pageService;
        private WorkoutsPageViewModel viewModel;
        private List<Workout> workouts;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new WorkoutDatabase();
            pageService = new MockPageService();
            viewModel = new WorkoutsPageViewModel(mockDatabase, pageService);
            workouts = mockDatabase.GetWorkouts();
            viewModel.LoadDataCommand.Execute(null);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(viewModel, null);
            Assert.AreEqual(viewModel.PageTitle, "WORKOUTS");
            Assert.AreEqual(viewModel.ButtonText, "Add Workout");
            Assert.AreEqual(viewModel.Workouts.Count, workouts.Count);
            Assert.AreEqual(viewModel.SelectedWorkout, null);
        }

        [Test]
        public void LoadDataTest()
        {
            Assert.AreNotEqual(viewModel.Workouts.Count, 0);
            Assert.AreEqual(workouts.Count, viewModel.Workouts.Count);
        }

        [Test]
        public async Task DeleteCommandTest()
        {
            WorkoutViewModel workoutViewModel = viewModel.Workouts.FirstOrDefault();
            Workout workout = workouts.FirstOrDefault();

            Assert.AreEqual(viewModel.Workouts.Count, workouts.Count);
            Assert.AreNotEqual(workoutViewModel, null);
            Assert.AreNotEqual(workout, null);

            await viewModel.DeleteWorkout(workoutViewModel);

            WorkoutViewModel deletedWorkoutFromViewModel = viewModel.Workouts.Where(w => w.Id == workoutViewModel.Id).ToList().FirstOrDefault();
            Workout deletedWorkoutFromViewDb = mockDatabase.GetWorkout(workoutViewModel.Id);

            Assert.AreEqual(viewModel.Workouts.Count, workouts.Count);
            Assert.AreEqual(deletedWorkoutFromViewModel, null);
            Assert.AreEqual(deletedWorkoutFromViewDb, null);
        }

        [Test]
        public async Task DeleteNullCommandTest()
        {
            int numInList = viewModel.Workouts.Count;
            int numInDb = workouts.Count;

            Assert.AreEqual(viewModel.Workouts.Count, workouts.Count);
            Assert.AreEqual(viewModel.Workouts.Count, numInList);
            Assert.AreEqual(workouts.Count, numInDb);

            await viewModel.DeleteWorkout(null);

            Assert.AreEqual(viewModel.Workouts.Count, workouts.Count);
            Assert.AreEqual(viewModel.Workouts.Count, numInList);
            Assert.AreEqual(workouts.Count, numInDb);
        }
    }
}
