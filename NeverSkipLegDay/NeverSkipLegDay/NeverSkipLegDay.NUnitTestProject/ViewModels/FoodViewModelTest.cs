using System.Linq;
using NUnit.Framework;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.NUnitTestProject.Database;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class FoodViewModelTest
    {
        private FoodDatabase mockDatabase;
        private FoodViewModel viewModel;
        private Food Food;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new FoodDatabase();
            Food = mockDatabase.GetFoods().FirstOrDefault();
            viewModel = new FoodViewModel(Food);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(viewModel.Id, Food.Id);
            Assert.AreEqual(viewModel.MealId, Food.MealId);
            Assert.AreEqual(viewModel.Name, Food.Name);
            Assert.AreEqual(viewModel.Fat, Food.Fat);
            Assert.AreEqual(viewModel.Prot, Food.Prot);
            Assert.AreEqual(viewModel.Carb, Food.Carb);
            Assert.AreEqual(viewModel.Cal, Food.Cal);
        }

        [Test]
        public void OnPropertyChangedTest()
        {
            string newName = "New Food";

            viewModel.Name = newName;

            Assert.AreNotEqual(viewModel.Name, Food.Name);
            Assert.AreEqual(viewModel.Name, newName);
        }
    }
}
