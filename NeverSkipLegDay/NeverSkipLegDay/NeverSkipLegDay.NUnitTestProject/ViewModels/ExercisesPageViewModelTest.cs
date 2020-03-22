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
    public class ExercisesPageViewModelTest
    {
        private ExerciseDatabase mockDatabase;
        private MockPageService pageService;
        private ExercisesPageViewModel viewModel;
        private List<Exercise> exercises;
        private List<Exercise> allExercises;
        private WorkoutViewModel workoutViewModel;
        private Workout workout;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new ExerciseDatabase();
            pageService = new MockPageService();
            workout = new WorkoutDatabase().GetWorkouts().FirstOrDefault();
            workoutViewModel = new WorkoutViewModel(workout);
            viewModel = new ExercisesPageViewModel(workoutViewModel, mockDatabase, pageService);
            exercises = mockDatabase.GetExercisesByWorkoutId(workout.Id);
            allExercises = mockDatabase.GetExercises();
            viewModel.LoadDataCommand.Execute(null);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(viewModel, null);
            Assert.AreEqual(viewModel.Workout, workoutViewModel);
            Assert.AreEqual(viewModel.PageTitle, "EXERCISES");
            Assert.AreEqual(viewModel.ButtonText, "Add Exercise");
            Assert.AreEqual(viewModel.Exercises.Count, exercises.Count);
            Assert.AreEqual(viewModel.SelectedExercise, null);
        }

        [Test]
        public void LoadDataTest()
        {
            Assert.AreNotEqual(viewModel.Exercises.Count, 0);
            Assert.AreNotEqual(viewModel.Exercises.Count, allExercises.Count);
            Assert.AreEqual(exercises.Count, viewModel.Exercises.Count);
        }

        [Test]
        public async Task DeleteCommand()
        {
            ExerciseViewModel ExerciseViewModel = viewModel.Exercises.FirstOrDefault();
            Exercise Exercise = exercises.FirstOrDefault();

            Assert.AreEqual(viewModel.Exercises.Count, exercises.Count);
            Assert.AreNotEqual(ExerciseViewModel, null);
            Assert.AreNotEqual(Exercise, null);

            await viewModel.DeleteExercise(ExerciseViewModel);

            ExerciseViewModel deletedExerciseFromViewModel = viewModel.Exercises.Where(w => w.Id == ExerciseViewModel.Id).ToList().FirstOrDefault();
            Exercise deletedExerciseFromViewDb = mockDatabase.GetExercise(ExerciseViewModel.Id);

            Assert.AreEqual(deletedExerciseFromViewModel, null);
            Assert.AreEqual(deletedExerciseFromViewDb, null);
        }

        [Test]
        public async Task DeleteNullCommand()
        {
            int numInList = viewModel.Exercises.Count;
            int numInDb = exercises.Count;

            Assert.AreEqual(viewModel.Exercises.Count, exercises.Count);
            Assert.AreEqual(viewModel.Exercises.Count, numInList);
            Assert.AreEqual(exercises.Count, numInDb);

            await viewModel.DeleteExercise(null);

            Assert.AreEqual(viewModel.Exercises.Count, exercises.Count);
            Assert.AreEqual(viewModel.Exercises.Count, numInList);
            Assert.AreEqual(exercises.Count, numInDb);
        }
    }
}
