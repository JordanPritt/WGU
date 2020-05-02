using System;
using System.Linq;
using Scheduler.Data.Models;
using System.Collections.Generic;

namespace Scheduler.Data
{
    public static class CustomerDao
    {
        public static Customer CreateCustomer(string name, int addressId, string createdBy)
        {
            try
            {
                Customer customer = new Customer
                {
                    CustomerName = name,
                    AddressId = addressId,
                    Active = 1,
                    CreateDate = DateTime.Now,
                    CreatedBy = createdBy,
                    LastUpdate = DateTime.Now,
                    LastUpdateBy = createdBy
                };

                return customer;
            }
            catch (Exception err)
            {
                Console.WriteLine("Couldn't create a user." + err);
                throw new Exception("Couldn't create a user." + err);
            }
        }
        public static bool SaveNewCustomer(Customer newCustomer)
        {
            using (Context context = new Context())
            {
                try
                {
                    context.Customer.Add(newCustomer);
                    context.SaveChanges();

                    return true;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Couldn't save user." + err);
                    return false;
                }
            }
        }

        public static bool DeleteCustomer(Customer customer)
        {
            using (Context context = new Context())
            {
                try
                {
                    context.Customer.Remove(customer);
                    context.SaveChanges();

                    return true;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Couldn't delete user." + err);
                    return false;
                }
            }
        }

        public static Customer GetCustomerById(int id)
        {
            try
            {
                Customer customer = new Customer();

                using (Context db = new Context())
                {
                    customer = db.Customer.Single(c => c.CustomerId == id);
                }

                return customer;
            }
            catch (Exception err)
            {
                Console.WriteLine("error: " + err);
                throw new Exception($"couldn't find the customer with CustomerId: {id}");
            }
        }

        public static Customer ApplyCustomerChanges(Customer customer,
                                                             string name,
                                                             int addressId,
                                                             sbyte active,
                                                             DateTime lastUpdated,
                                                             string updatedBy)
        {
            try
            {
                customer.CustomerName = name;
                customer.AddressId = addressId;
                customer.Active = active;
                customer.LastUpdate = lastUpdated;
                customer.LastUpdateBy = updatedBy;

                return customer;
            }
            catch (Exception Ex)
            {
                throw new Exception($"Couldn't apply changes to customer object because: {Ex}");
            }
        }

        public static bool UpdateCustomer(Customer customer)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Customer.Update(customer);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception err)
            {
                throw new Exception("couldn't update the customer because: " + err);
            }
        }

        public static Dictionary<string, int> GetAllCustomers()
        {
            Dictionary<string, int> customers = new Dictionary<string, int>();

            try
            {
                using (Context db = new Context())
                {
                    customers = db.Customer.Select(c => new { c.CustomerName, c.CustomerId }).ToDictionary(c => c.CustomerName, c => c.CustomerId);
                }

                return customers;
            }
            catch (Exception err)
            {
                throw new Exception($"Couldn't retrieve all ucstomers because: {err}");
            }
        }
    }
}
