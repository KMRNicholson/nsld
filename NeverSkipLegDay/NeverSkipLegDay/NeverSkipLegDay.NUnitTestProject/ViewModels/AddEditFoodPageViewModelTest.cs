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
    public class AddEditFoodPageViewModelTest
    {
        private FoodDatabase mockDatabase;
        private MockPageService pageService;
        private AddEditFoodPageViewModel editViewModel;
        private AddEditFoodPageViewModel addViewModel;
        private FoodViewModel foodViewModel;
        private List<Food> foodsInDb;
        private Food food;
        private Meal newMeal;
        private Meal meal;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new FoodDatabase();
            pageService = new MockPageService();
            meal = new MealDatabase().GetMeals().FirstOrDefault();
            newMeal = new MealDatabase().GetMeals().Last();

            addViewModel = new AddEditFoodPageViewModel(new FoodViewModel() { MealId = newMeal.Id }, mockDatabase, pageService);

            foodsInDb = mockDatabase.GetFoodsByMealId(meal.Id);
            food = foodsInDb.FirstOrDefault();
            foodViewModel = new FoodViewModel(food);
            editViewModel = new AddEditFoodPageViewModel(foodViewModel, mockDatabase, pageService);
        }

        [Test]
        public void EditFoodConstructorTest()
        {
            Assert.AreNotEqual(editViewModel, null);
            Assert.AreEqual(editViewModel.Food.Id, food.Id);
            Assert.AreEqual(editViewModel.Food.MealId, food.MealId);
            Assert.AreEqual(editViewModel.Food.Name, food.Name);
        }

        [Test]
        public void AddFoodConstructorTest()
        {
            Assert.AreNotEqual(addViewModel, null);
            Assert.AreEqual(addViewModel.Food.Id, 0);
            Assert.AreEqual(addViewModel.Food.MealId, newMeal.Id);
            Assert.AreEqual(addViewModel.Food.Name, null);
        }

        [Test]
        public async Task SaveNewFoodTest()
        {
            Assert.AreEqual(addViewModel.Food.Id, 0);
            Assert.AreEqual(addViewModel.Food.MealId, newMeal.Id);
            Assert.AreEqual(addViewModel.Food.Name, null);

            addViewModel.Food.Name = "New Food";

            await addViewModel.Save();

            Food foodInDb = mockDatabase.GetFood(addViewModel.Food.Id);

            Assert.AreNotEqual(addViewModel.Food.Id, 0, "Testing automatic assignment to Id when food is saved to the database.");
            Assert.AreNotEqual(foodInDb, null, "Testing the food that was saved is found in the database.");
            Assert.AreEqual(addViewModel.Food.Name, "New Food", "Testing the food name is set properly on the viewmodel.");
            Assert.AreEqual(addViewModel.Food.Name, foodInDb.Name, "Testing the food name on the viewmodel is the same as the model in the database.");
        }

        [Test]
        public async Task SaveExistingFoodTest()
        {
            Assert.AreEqual(editViewModel.Food.Id, food.Id);
            Assert.AreEqual(editViewModel.Food.MealId, food.MealId);
            Assert.AreEqual(editViewModel.Food.Name, food.Name);

            editViewModel.Food.Name = "Edited Food Name";

            await editViewModel.Save();

            Food editedFoodInDb = mockDatabase.GetFood(editViewModel.Food.Id);

            Assert.AreEqual(editViewModel.Food.Name, editedFoodInDb.Name, "Testing the viewmodel food name is the same as the one in the database.");
        }

        [Test]
        public async Task SaveNewFoodWithEmtpyNameTest()
        {
            Assert.AreEqual(addViewModel.Food.Id, 0);
            Assert.AreEqual(addViewModel.Food.Name, null);

            addViewModel.Food.Name = "";

            await addViewModel.Save();

            Food foodInDb = mockDatabase.GetFood(addViewModel.Food.Id);

            Assert.AreEqual(addViewModel.Food.Id, 0, "Testing that database did not save the food.");
            Assert.AreEqual(foodInDb, null, "Testing that the food that was not saved in the database.");
        }

        [Test]
        public async Task SaveExistingFoodWithEmtpyNameTest()
        {
            Assert.AreEqual(editViewModel.Food.Id, food.Id);
            Assert.AreEqual(editViewModel.Food.Name, food.Name);

            editViewModel.Food.Name = "";

            await editViewModel.Save();

            Food editedFoodInDb = mockDatabase.GetFood(editViewModel.Food.Id);

            Assert.AreNotEqual(editViewModel.Food.Name, editedFoodInDb.Name, "Testing the viewmodel food name is not the same as the one in the database.");
        }
    }
}