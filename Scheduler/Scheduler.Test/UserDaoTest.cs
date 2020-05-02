using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.Data.Models;
using Scheduler.Data;
using System;

namespace Scheduler.Test
{
    [TestClass]
    public class UserDaoTest
    {
        [TestMethod]
        public void GetUserIdByNameTest()
        {
            string username = "test";
            int id = UserDao.GetUserIdByName(username);

            Assert.IsTrue(id >= 0);
        }
    }
}
