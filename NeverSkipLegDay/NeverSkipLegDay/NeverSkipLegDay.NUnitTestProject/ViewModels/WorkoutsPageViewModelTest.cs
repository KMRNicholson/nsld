using System;
using Moq;
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
    public class WorkoutsPageViewModelTest
    {
        WorkoutDatabase mockDatabase = new WorkoutDatabase();
        MockPageService pageService = new MockPageService();
        WorkoutsPageViewModel viewModel;
        List<Workout> workouts;

        [SetUp]
        public void Constructor()
        {
            viewModel = new WorkoutsPageViewModel(mockDatabase, pageService);
            workouts = mockDatabase.GetWorkouts();
            viewModel.LoadDataCommand.Execute(null);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(viewModel, null);
        }

        [Test]
        public void PropertyGettersTest()
        {
            Assert.AreEqual(viewModel.AddButtonText, "Add Workout");
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
        public async Task DeleteCommand()
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
        public async Task DeleteNullCommand()
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

        [Test]
        public async Task DeselectWorkoutTest()
        {
            Assert.AreEqual(viewModel.SelectedWorkout, null);

            WorkoutViewModel selectedWorkout = new WorkoutViewModel(workouts.FirstOrDefault());
            viewModel.SelectedWorkout = selectedWorkout;

            Assert.AreEqual(viewModel.SelectedWorkout, selectedWorkout);

            await viewModel.SelectWorkout(selectedWorkout);

            Assert.AreEqual(viewModel.SelectedWorkout, null);
        }

        [Test]
        public void IsWorkoutsEmptyTest()
        {
            Assert.IsFalse(viewModel.IsWorkoutsEmpty());

            viewModel.Workouts.Clear();

            Assert.IsTrue(viewModel.IsWorkoutsEmpty());
        }
    }
}
