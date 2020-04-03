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
    public class FoodsPageViewModelTest
    {
        private FoodDatabase mockDatabase;
        private MockPageService pageService;
        private FoodsPageViewModel viewModel;
        private List<Food> foods;
        private List<Food> allFoods;
        private MealViewModel mealViewModel;
        private Meal meal;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new FoodDatabase();
            pageService = new MockPageService();
            meal = new MealDatabase().GetMeals().FirstOrDefault();
            mealViewModel = new MealViewModel(meal);
            viewModel = new FoodsPageViewModel(mealViewModel, mockDatabase, pageService);
            foods = mockDatabase.GetFoodsByMealId(meal.Id);
            allFoods = mockDatabase.GetFoods();
            viewModel.LoadDataCommand.Execute(null);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreNotEqual(viewModel, null);
            Assert.AreEqual(viewModel.Meal, mealViewModel);
            Assert.AreEqual(viewModel.PageTitle, "FOODS");
            Assert.AreEqual(viewModel.ButtonText, "Add Food");
            Assert.AreEqual(viewModel.Foods.Count, foods.Count);
        }

        [Test]
        public void LoadDataTest()
        {
            Assert.AreNotEqual(viewModel.Foods.Count, 0);
            Assert.AreNotEqual(viewModel.Foods.Count, allFoods.Count);
            Assert.AreEqual(foods.Count, viewModel.Foods.Count);
        }

        [Test]
        public async Task DeleteCommand()
        {
            FoodViewModel FoodViewModel = viewModel.Foods.FirstOrDefault();
            Food Food = foods.FirstOrDefault();

            Assert.AreEqual(viewModel.Foods.Count, foods.Count);
            Assert.AreNotEqual(FoodViewModel, null);
            Assert.AreNotEqual(Food, null);

            await viewModel.DeleteFood(FoodViewModel);

            FoodViewModel deletedFoodFromViewModel = viewModel.Foods.Where(w => w.Id == FoodViewModel.Id).ToList().FirstOrDefault();
            Food deletedFoodFromViewDb = mockDatabase.GetFood(FoodViewModel.Id);

            Assert.AreEqual(deletedFoodFromViewModel, null);
            Assert.AreEqual(deletedFoodFromViewDb, null);
        }

        [Test]
        public async Task DeleteNullCommand()
        {
            int numInList = viewModel.Foods.Count;
            int numInDb = foods.Count;

            Assert.AreEqual(viewModel.Foods.Count, foods.Count);
            Assert.AreEqual(viewModel.Foods.Count, numInList);
            Assert.AreEqual(foods.Count, numInDb);

            await viewModel.DeleteFood(null);

            Assert.AreEqual(viewModel.Foods.Count, foods.Count);
            Assert.AreEqual(viewModel.Foods.Count, numInList);
            Assert.AreEqual(foods.Count, numInDb);
        }

        [Test]
        public void IsFoodsEmptyTest()
        {
            Assert.IsFalse(viewModel.IsFoodsEmpty());

            viewModel.Foods.Clear();

            Assert.IsTrue(viewModel.IsFoodsEmpty());
        }
    }
}
