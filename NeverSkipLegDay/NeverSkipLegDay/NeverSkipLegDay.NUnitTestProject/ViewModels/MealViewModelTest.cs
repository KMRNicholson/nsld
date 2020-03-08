using System.Linq;
using NUnit.Framework;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.NUnitTestProject.Database;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class MealViewModelTest
    {
        private MealDatabase mockDatabase;
        private MealViewModel viewModel;
        private Meal meal;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new MealDatabase();
            meal = mockDatabase.GetMeals().FirstOrDefault();
            viewModel = new MealViewModel(meal);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(viewModel.Id, meal.Id);
            Assert.AreEqual(viewModel.Name, meal.Name);
        }

        [Test]
        public void OnPropertyChangedTest()
        {
            string newName = "New Meal";

            viewModel.Name = newName;

            Assert.AreNotEqual(viewModel.Name, meal.Name);
            Assert.AreEqual(viewModel.Name, newName);
        }
    }
}
