using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.Data.Models;
using Scheduler.Data;
using System;

namespace Scheduler.Test
{
    [TestClass]
    public class AddressDaoTests
    {
        [TestMethod]
        public void GetAllCitiesTest()
        {
            System.Collections.Generic.Dictionary<string, int> cities = AddressDao.GetAllCities();
            Assert.IsTrue(cities.Count >= 1);
        }
        [TestMethod]
        public void CreateAddressTest()
        {
            var address = AddressDao.CreateAddress("123 Test Address", "456 Seccond Address", 1, "12345-9876", "123-456-7890", DateTime.Now, DateTime.Now, "system", "system");
            Assert.IsInstanceOfType(address, typeof(Address));
        }

        [TestMethod]
        public void GetAddressByValuesTest()
        {
            var address = AddressDao.GetAddressByValues("123 Main", 1, "11111");
            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.Address1 == "123 Main");
            Assert.IsTrue(address.CityId == 1);
            Assert.IsTrue(address.PostalCode == "11111");
        }
    }
}
