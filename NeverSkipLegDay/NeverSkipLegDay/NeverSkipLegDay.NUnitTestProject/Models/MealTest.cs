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

            int fatTotal = foods.Select(x => x.Fat).Sum().Value;
            int protTotal = foods.Select(x => x.Prot).Sum().Value;
            int carbTotal = foods.Select(x => x.Carb).Sum().Value;
            int calTotal = foods.Select(x => x.Cal).Sum().Value;

            Dictionary<string, int> mealTotals = meal.GetMealTotals(foodDatabase);

            Assert.AreEqual(mealTotals.GetValueOrDefault("Fat"), fatTotal);
            Assert.AreEqual(mealTotals.GetValueOrDefault("Prot"), protTotal);
            Assert.AreEqual(mealTotals.GetValueOrDefault("Carb"), carbTotal);
            Assert.AreEqual(mealTotals.GetValueOrDefault("Cal"), calTotal);
        }
    }
}