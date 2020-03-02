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
    public class AddEditWorkoutPageViewModelTest
    {
        private WorkoutDatabase mockDatabase;
        private MockPageService pageService;
        private AddEditWorkoutPageViewModel editViewModel;
        private AddEditWorkoutPageViewModel addViewModel;
        private WorkoutViewModel workoutViewModel;
        private List<Workout> workoutsInDb;
        private Workout workout;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new WorkoutDatabase();
            pageService = new MockPageService();

            addViewModel = new AddEditWorkoutPageViewModel(new WorkoutViewModel(), mockDatabase, pageService);

            workoutsInDb = mockDatabase.GetWorkouts();
            workout = workoutsInDb.FirstOrDefault();
            workoutViewModel = new WorkoutViewModel(workout);
            editViewModel = new AddEditWorkoutPageViewModel(workoutViewModel, mockDatabase, pageService);
        }

        [Test]
        public void EditWorkoutConstructorTest()
        {
            Assert.AreNotEqual(editViewModel, null);
            Assert.AreEqual(editViewModel.Workout.Id, workout.Id);
            Assert.AreEqual(editViewModel.Workout.Name, workout.Name);
        }

        [Test]
        public void AddWorkoutConstructorTest()
        {
            Assert.AreNotEqual(addViewModel, null);
            Assert.AreEqual(addViewModel.Workout.Id, 0);
            Assert.AreEqual(addViewModel.Workout.Name, null);
        }

        [Test]
        public async Task SaveNewWorkoutTest()
        {
            Assert.AreEqual(addViewModel.Workout.Id, 0);
            Assert.AreEqual(addViewModel.Workout.Name, null);

            addViewModel.Workout.Name = "New Workout";

            await addViewModel.Save();

            Workout workoutInDb = mockDatabase.GetWorkout(addViewModel.Workout.Id);

            Assert.AreNotEqual(addViewModel.Workout.Id, 0, "Testing automatic assignment to Id when workout is saved to the database.");
            Assert.AreNotEqual(workoutInDb, null, "Testing the workout that was saved is found in the database.");
            Assert.AreEqual(addViewModel.Workout.Name, "New Workout", "Testing the workout name is set properly on the viewmodel.");
            Assert.AreEqual(addViewModel.Workout.Name, workoutInDb.Name, "Testing the workout name on the viewmodel is the same as the model in the database.");
            Assert.IsTrue(workoutsInDb.Contains(addViewModel.Workout), "Testing the workout on the viewmodel is found in the database.");
        }

        [Test]
        public async Task SaveExistingWorkoutTest()
        {
            Assert.AreEqual(editViewModel.Workout.Id, workout.Id);
            Assert.AreEqual(editViewModel.Workout.Name, workout.Name);

            editViewModel.Workout.Name = "Edited Workout Name";

            await editViewModel.Save();

            Workout editedWorkoutInDb = mockDatabase.GetWorkout(editViewModel.Workout.Id);

            Assert.AreEqual(editViewModel.Workout.Name, editedWorkoutInDb.Name, "Testing the viewmodel workout name is the same as the one in the database.");
        }

        [Test]
        public async Task SaveNewWorkoutWithEmtpyNameTest()
        {
            Assert.AreEqual(addViewModel.Workout.Id, 0);
            Assert.AreEqual(addViewModel.Workout.Name, null);

            addViewModel.Workout.Name = "";

            await addViewModel.Save();

            Workout workoutInDb = mockDatabase.GetWorkout(addViewModel.Workout.Id);

            Assert.AreEqual(addViewModel.Workout.Id, 0, "Testing that database did not save the workout.");
            Assert.AreEqual(workoutInDb, null, "Testing that the workout that was not saved in the database.");
            Assert.IsFalse(workoutsInDb.Contains(addViewModel.Workout), "Testing the workout on the viewmodel is found in the database.");
        }

        [Test]
        public async Task SaveExistingWorkoutWithEmtpyNameTest()
        {
            Assert.AreEqual(editViewModel.Workout.Id, workout.Id);
            Assert.AreEqual(editViewModel.Workout.Name, workout.Name);

            editViewModel.Workout.Name = "";

            await editViewModel.Save();

            Workout editedWorkoutInDb = mockDatabase.GetWorkout(editViewModel.Workout.Id);

            Assert.AreNotEqual(editViewModel.Workout.Name, editedWorkoutInDb.Name, "Testing the viewmodel workout name is not the same as the one in the database.");
        }
    }
}
