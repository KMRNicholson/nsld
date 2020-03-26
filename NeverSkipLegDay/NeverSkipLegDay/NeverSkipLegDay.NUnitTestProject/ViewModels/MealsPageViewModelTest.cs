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
    public class MealsPageViewModelTest
    {
        private MealDatabase mockDatabase;
        private MockPageService pageService;
        private MealsPageViewModel viewModel;
        private List<Meal> meals;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new MealDatabase();
            pageService = new MockPageService();
            viewModel = new MealsPageViewModel(mockDatabase, pageService);
            meals = mockDatabase.GetMeals();
            viewModel.LoadDataCommand.Execute(null);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(viewModel, null);
            Assert.AreEqual(viewModel.ButtonText, "Add Meal");
            Assert.AreEqual(viewModel.PageTitle, "MEALS");
            Assert.AreEqual(viewModel.Meals.Count, meals.Count);
            Assert.AreEqual(viewModel.SelectedMeal, null);
        }

        [Test]
        public void LoadDataTest()
        {
            Assert.AreNotEqual(viewModel.Meals.Count, 0);
            Assert.AreEqual(meals.Count, viewModel.Meals.Count);
        }

        [Test]
        public async Task DeleteCommand()
        {
            MealViewModel mealViewModel = viewModel.Meals.FirstOrDefault();
            Meal meal = meals.FirstOrDefault();

            Assert.AreEqual(viewModel.Meals.Count, meals.Count);
            Assert.AreNotEqual(mealViewModel, null);
            Assert.AreNotEqual(meal, null);

            await viewModel.DeleteMeal(mealViewModel);

            MealViewModel deletedMealFromViewModel = viewModel.Meals.Where(w => w.Id == mealViewModel.Id).ToList().FirstOrDefault();
            Meal deletedMealFromViewDb = mockDatabase.GetMeal(mealViewModel.Id);

            Assert.AreEqual(viewModel.Meals.Count, meals.Count);
            Assert.AreEqual(deletedMealFromViewModel, null);
            Assert.AreEqual(deletedMealFromViewDb, null);
        }

        [Test]
        public async Task DeleteNullCommand()
        {
            int numInList = viewModel.Meals.Count;
            int numInDb = meals.Count;

            Assert.AreEqual(viewModel.Meals.Count, meals.Count);
            Assert.AreEqual(viewModel.Meals.Count, numInList);
            Assert.AreEqual(meals.Count, numInDb);

            await viewModel.DeleteMeal(null);

            Assert.AreEqual(viewModel.Meals.Count, meals.Count);
            Assert.AreEqual(viewModel.Meals.Count, numInList);
            Assert.AreEqual(meals.Count, numInDb);
        }
    }
}
