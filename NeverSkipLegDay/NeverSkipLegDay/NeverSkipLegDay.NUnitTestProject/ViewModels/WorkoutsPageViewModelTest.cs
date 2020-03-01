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

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class WorkoutsPageViewModelTest
    {
        WorkoutDatabase mockDatabase = new WorkoutDatabase();
        PageService pageService = new PageService();
        WorkoutsPageViewModel viewModel;
        List<Workout> workouts;
        Workout workout;

        [SetUp]
        public void Constructor()
        {
            viewModel = new WorkoutsPageViewModel(mockDatabase, pageService);
            workouts = mockDatabase.GetWorkouts();
            workout = workouts.FirstOrDefault();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(viewModel, null);
        }

        [Test]
        public void LoadDataTest()
        {
            viewModel.LoadDataCommand.Execute(null);

            Assert.AreNotEqual(viewModel.Workouts.Count, 0);
            Assert.AreEqual(workouts.Count, viewModel.Workouts.Count);
        }

        [Test]
        public void DeleteCommand()
        {
            viewModel.LoadDataCommand.Execute(null);
            viewModel.DeleteCommand.Execute(workout);

            Assert.AreNotEqual(viewModel.Workouts.Count, workouts.Count);
        }
    }
}
