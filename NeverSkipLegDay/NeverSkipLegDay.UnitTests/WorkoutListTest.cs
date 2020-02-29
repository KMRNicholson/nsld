using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.ViewModels;
using NeverSkipLegDay.UnitTests.Database;

namespace NeverSkipLegDay.UnitTests
{
    [TestClass]
    public class WorkoutListTest
    {
        WorkoutDatabase _database = new WorkoutDatabase();

        [TestMethod]
        public void ConstructorTest()
        {
            
        }

        [TestMethod]
        public void IsEmptyTest()
        {
        }
    }
}
