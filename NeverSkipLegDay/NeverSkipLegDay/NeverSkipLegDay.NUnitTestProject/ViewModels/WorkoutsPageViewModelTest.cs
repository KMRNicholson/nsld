using System;
using Moq;
using NUnit.Framework;
using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    [TestFixture]
    public class WorkoutsPageViewModelTest
    {
        [Test]
        public void ConstructorTest()
        {
            Mock<WorkoutDal> mockDB = new Mock<WorkoutDal>();
            PageService pageService = new PageService();
            WorkoutsPageViewModel viewModel = new WorkoutsPageViewModel(mockDB.Object, pageService);

            Assert.AreNotEqual(viewModel, null);
        }
    }
}
