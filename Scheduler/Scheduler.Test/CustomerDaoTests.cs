using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.Data.Models;
using Scheduler.Data;
using System;

namespace Scheduler.Test
{
    [TestClass]
    public class CustomerTests
    {
        Customer cust = new Customer();

        [TestMethod]
        public void TestCreateCustomer()
        {
            string name = "Billy Bob";
            int addressId = 0;
            string createdBy = "system";

            cust = CustomerDao.CreateCustomer(name, addressId, createdBy);

            Assert.IsInstanceOfType(cust, typeof(Customer));
            Assert.IsTrue(cust.CustomerName == name);
            Assert.IsTrue(cust.AddressId == addressId);
            Assert.IsTrue(cust.CreatedBy == createdBy);
        }

        [TestMethod]
        public void TestSaveAndDeleteCustomer()
        {
            bool save, delete;
            cust = CustomerDao.CreateCustomer("Billy Bob", 1, "system");

            save = CustomerDao.SaveNewCustomer(cust);
            delete = CustomerDao.DeleteCustomer(cust);

            Assert.IsTrue(save == true && delete == true);
        }

        [TestMethod]
        public void TestGetCustomerById()
        {
            int id = 1;
            cust = CustomerDao.GetCustomerById(id);

            Assert.IsInstanceOfType(cust, typeof(Customer));
        }

        [TestMethod]
        public void TestApplyCustomerChanges()
        {
            int id = 1;
            cust = CustomerDao.GetCustomerById(id);

            Customer updatedCustomer = CustomerDao.ApplyCustomerChanges(cust, "Jane Douglas", 2, 1, DateTime.Now, "System Admin");
            Assert.IsInstanceOfType(updatedCustomer, typeof(Customer));
        }

        [TestMethod]
        public void TestUpdateCustomer()
        {
            int id = 1;
            cust = CustomerDao.GetCustomerById(id);

            Customer updatedCustomer = CustomerDao.ApplyCustomerChanges(cust, "Jane Douglas", 2, 1, DateTime.Now, "System Admin");
            bool result = CustomerDao.UpdateCustomer(updatedCustomer);

            Assert.IsTrue(result == true);

            updatedCustomer = CustomerDao.ApplyCustomerChanges(cust, "Jane Doe", 1, 1, DateTime.Now, "system");
            result = CustomerDao.UpdateCustomer(updatedCustomer);

            Assert.IsTrue(result == true);
        }

        [TestMethod]
        public void TestGetAllCustomer()
        {
            System.Collections.Generic.Dictionary<string, int> custs = CustomerDao.GetAllCustomers();
            Assert.IsTrue(custs.Count >= 1);
        }
    }
}
