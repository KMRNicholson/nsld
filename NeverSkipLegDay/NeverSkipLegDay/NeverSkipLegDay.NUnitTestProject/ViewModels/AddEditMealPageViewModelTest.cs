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
    public class AddEditMealPageViewModelTest
    {
        private MealDatabase mockDatabase;
        private MockPageService pageService;
        private AddEditMealPageViewModel editViewModel;
        private AddEditMealPageViewModel addViewModel;
        private MealViewModel mealViewModel;
        private List<Meal> mealsInDb;
        private Meal meal;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new MealDatabase();
            pageService = new MockPageService();

            addViewModel = new AddEditMealPageViewModel(new MealViewModel(), mockDatabase, pageService);

            mealsInDb = mockDatabase.GetMeals();
            meal = mealsInDb.FirstOrDefault();
            mealViewModel = new MealViewModel(meal);
            editViewModel = new AddEditMealPageViewModel(mealViewModel, mockDatabase, pageService);
        }

        [Test]
        public void EditMealConstructorTest()
        {
            Assert.AreNotEqual(editViewModel, null);
            Assert.AreEqual(editViewModel.Meal.Id, meal.Id);
            Assert.AreEqual(editViewModel.Meal.Name, meal.Name);
        }

        [Test]
        public void AddMealConstructorTest()
        {
            Assert.AreNotEqual(addViewModel, null);
            Assert.AreEqual(addViewModel.Meal.Id, 0);
            Assert.AreEqual(addViewModel.Meal.Name, null);
        }

        [Test]
        public async Task SaveNewMealTest()
        {
            Assert.AreEqual(addViewModel.Meal.Id, 0);
            Assert.AreEqual(addViewModel.Meal.Name, null);

            addViewModel.Meal.Name = "New Meal";

            await addViewModel.Save();

            Meal mealInDb = mockDatabase.GetMeal(addViewModel.Meal.Id);

            Assert.AreNotEqual(addViewModel.Meal.Id, 0, "Testing automatic assignment to Id when meal is saved to the database.");
            Assert.AreNotEqual(mealInDb, null, "Testing the meal that was saved is found in the database.");
            Assert.AreEqual(addViewModel.Meal.Name, "New Meal", "Testing the meal name is set properly on the viewmodel.");
            Assert.AreEqual(addViewModel.Meal.Name, mealInDb.Name, "Testing the meal name on the viewmodel is the same as the model in the database.");
            Assert.IsTrue(mealsInDb.Contains(addViewModel.Meal), "Testing the meal on the viewmodel is found in the database.");
        }

        [Test]
        public async Task SaveExistingMealTest()
        {
            Assert.AreEqual(editViewModel.Meal.Id, meal.Id);
            Assert.AreEqual(editViewModel.Meal.Name, meal.Name);

            editViewModel.Meal.Name = "Edited Meal Name";

            await editViewModel.Save();

            Meal editedMealInDb = mockDatabase.GetMeal(editViewModel.Meal.Id);

            Assert.AreEqual(editViewModel.Meal.Name, editedMealInDb.Name, "Testing the viewmodel meal name is the same as the one in the database.");
        }

        [Test]
        public async Task SaveNewMealWithEmtpyNameTest()
        {
            Assert.AreEqual(addViewModel.Meal.Id, 0);
            Assert.AreEqual(addViewModel.Meal.Name, null);

            addViewModel.Meal.Name = "";

            await addViewModel.Save();

            Meal mealInDb = mockDatabase.GetMeal(addViewModel.Meal.Id);

            Assert.AreEqual(addViewModel.Meal.Id, 0, "Testing that database did not save the meal.");
            Assert.AreEqual(mealInDb, null, "Testing that the meal that was not saved in the database.");
            Assert.IsFalse(mealsInDb.Contains(addViewModel.Meal), "Testing the meal on the viewmodel is found in the database.");
        }

        [Test]
        public async Task SaveExistingMealWithEmtpyNameTest()
        {
            Assert.AreEqual(editViewModel.Meal.Id, meal.Id);
            Assert.AreEqual(editViewModel.Meal.Name, meal.Name);

            editViewModel.Meal.Name = "";

            await editViewModel.Save();

            Meal editedMealInDb = mockDatabase.GetMeal(editViewModel.Meal.Id);

            Assert.AreNotEqual(editViewModel.Meal.Name, editedMealInDb.Name, "Testing the viewmodel meal name is not the same as the one in the database.");
        }
    }
}
