using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.UnitTests.ViewModels
{
    [TestClass]
    public class WorkoutsPageViewModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Mock<WorkoutDal> mockDB = new Mock<WorkoutDal>();
            PageService pageService = new PageService();
            WorkoutsPageViewModel viewModel = new WorkoutsPageViewModel(mockDB.Object, pageService);

            Assert.AreNotEqual(viewModel, null);
        }
    }
}
