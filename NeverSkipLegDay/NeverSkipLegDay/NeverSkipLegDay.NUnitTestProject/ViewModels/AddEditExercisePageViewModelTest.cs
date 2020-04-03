using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using NUnit.Framework;

using NeverSkipLegDay.NUnitTestProject.Database;
using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class AddEditExercisePageViewModelTest
    {
        private ExerciseDatabase mockDatabase;
        private MockPageService pageService;
        private AddEditExercisePageViewModel editViewModel;
        private AddEditExercisePageViewModel addViewModel;
        private ExerciseViewModel exerciseViewModel;
        private List<Exercise> exercisesInDb;
        private Exercise exercise;
        private Workout newWorkout;
        private Workout workout;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new ExerciseDatabase();
            pageService = new MockPageService();
            workout = new WorkoutDatabase().GetWorkouts().FirstOrDefault();
            newWorkout = new WorkoutDatabase().GetWorkouts().Last();

            addViewModel = new AddEditExercisePageViewModel(new ExerciseViewModel() { WorkoutId = newWorkout.Id }, mockDatabase, pageService);

            exercisesInDb = mockDatabase.GetExercisesByWorkoutId(workout.Id);
            exercise = exercisesInDb.FirstOrDefault();
            exerciseViewModel = new ExerciseViewModel(exercise);
            editViewModel = new AddEditExercisePageViewModel(exerciseViewModel, mockDatabase, pageService);
        }

        [Test]
        public void EditExerciseConstructorTest()
        {
            Assert.AreNotEqual(editViewModel, null);
            Assert.AreEqual(editViewModel.Exercise.Id, exercise.Id);
            Assert.AreEqual(editViewModel.Exercise.WorkoutId, exercise.WorkoutId);
            Assert.AreEqual(editViewModel.Exercise.Name, exercise.Name);
            Assert.AreEqual(editViewModel.PageTitle, "EXERCISE");
        }

        [Test]
        public void AddExerciseConstructorTest()
        {
            Assert.AreNotEqual(addViewModel, null);
            Assert.AreEqual(addViewModel.Exercise.Id, 0);
            Assert.AreEqual(addViewModel.Exercise.WorkoutId, newWorkout.Id);
            Assert.AreEqual(addViewModel.Exercise.Name, null);
            Assert.AreEqual(editViewModel.PageTitle, "EXERCISE");
        }

        [Test]
        public async Task SaveNewExerciseTest()
        {
            Assert.AreEqual(addViewModel.Exercise.Id, 0);
            Assert.AreEqual(addViewModel.Exercise.WorkoutId, newWorkout.Id);
            Assert.AreEqual(addViewModel.Exercise.Name, null);

            addViewModel.Exercise.Name = "New Exercise";

            await addViewModel.Save();

            Exercise exerciseInDb = mockDatabase.GetExercise(addViewModel.Exercise.Id);

            Assert.AreNotEqual(addViewModel.Exercise.Id, 0, "Testing automatic assignment to Id when exercise is saved to the database.");
            Assert.AreNotEqual(exerciseInDb, null, "Testing the exercise that was saved is found in the database.");
            Assert.AreEqual(addViewModel.Exercise.Name, "New Exercise", "Testing the exercise name is set properly on the viewmodel.");
            Assert.AreEqual(addViewModel.Exercise.Name, exerciseInDb.Name, "Testing the exercise name on the viewmodel is the same as the model in the database.");
        }

        [Test]
        public async Task SaveExistingExerciseTest()
        {
            Assert.AreEqual(editViewModel.Exercise.Id, exercise.Id);
            Assert.AreEqual(editViewModel.Exercise.WorkoutId, exercise.WorkoutId);
            Assert.AreEqual(editViewModel.Exercise.Name, exercise.Name);

            editViewModel.Exercise.Name = "Edited Exercise Name";

            await editViewModel.Save();

            Exercise editedExerciseInDb = mockDatabase.GetExercise(editViewModel.Exercise.Id);

            Assert.AreEqual(editViewModel.Exercise.Name, editedExerciseInDb.Name, "Testing the viewmodel exercise name is the same as the one in the database.");
        }

        [Test]
        public async Task SaveNewExerciseWithEmtpyNameTest()
        {
            Assert.AreEqual(addViewModel.Exercise.Id, 0);
            Assert.AreEqual(addViewModel.Exercise.Name, null);

            addViewModel.Exercise.Name = "";

            await addViewModel.Save();

            Exercise exerciseInDb = mockDatabase.GetExercise(addViewModel.Exercise.Id);

            Assert.AreEqual(addViewModel.Exercise.Id, 0, "Testing that database did not save the exercise.");
            Assert.AreEqual(exerciseInDb, null, "Testing that the exercise that was not saved in the database.");
        }

        [Test]
        public async Task SaveExistingExerciseWithEmtpyNameTest()
        {
            Assert.AreEqual(editViewModel.Exercise.Id, exercise.Id);
            Assert.AreEqual(editViewModel.Exercise.Name, exercise.Name);

            editViewModel.Exercise.Name = "";

            await editViewModel.Save();

            Exercise editedExerciseInDb = mockDatabase.GetExercise(editViewModel.Exercise.Id);

            Assert.AreNotEqual(editViewModel.Exercise.Name, editedExerciseInDb.Name, "Testing the viewmodel exercise name is not the same as the one in the database.");
        }
    }
}