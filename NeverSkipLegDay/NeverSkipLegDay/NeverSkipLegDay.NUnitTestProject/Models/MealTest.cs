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
    public class MealTest
    {
        private MealDatabase mockDatabase;
        private Meal meal;

        [SetUp]
        public void Constructor()
        {
            mockDatabase = new MealDatabase();
            meal = mockDatabase.GetMeals().FirstOrDefault();
        }

        [Test]
        public void MealConstructorTest()
        {
            Assert.AreNotEqual(meal.Id, null);
            Assert.AreNotEqual(meal.Name, null);
        }

        [Test]
        public void GetMealTotalsTest()
        {
            FoodDatabase foodDatabase = new FoodDatabase();
            List<Food> foods = foodDatabase.GetFoodsByMealId(meal.Id);

            decimal fatTotal = foods.Select(x => x.Fat).Sum();
            decimal protTotal = foods.Select(x => x.Prot).Sum();
            decimal carbTotal = foods.Select(x => x.Carb).Sum();
            decimal calTotal = foods.Select(x => x.Cal).Sum();

            Dictionary<string, decimal> mealTotals = meal.GetMealTotals(foodDatabase);

            Assert.AreEqual(mealTotals.GetValueOrDefault("Fat"), fatTotal);
            Assert.AreEqual(mealTotals.GetValueOrDefault("Prot"), protTotal);
            Assert.AreEqual(mealTotals.GetValueOrDefault("Carb"), carbTotal);
            Assert.AreEqual(mealTotals.GetValueOrDefault("Cal"), calTotal);
        }
    }
}